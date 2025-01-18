using System;
using Microsoft.Extensions.Configuration;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using System.Linq;
using JetBrains.Annotations;

public class Build : NukeBuild
{
    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    static readonly IConfiguration Settings = new ConfigurationBuilder()
        .AddUserSecrets(typeof(Build).Assembly)
        .Build();

    public Project GetProject(string projectName) => Solution.GetAllProjects(projectName).First();

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("Connection string")] 
    readonly string ConnectionString = Settings.GetConnectionString("MyMeetings");

    [Solution] readonly Solution Solution;

    public static int Main() => Execute<Build>(t => t.MigrateDatabase);

    Target Clean => _ => _
        .Executes(() =>
        {
            foreach (var directoryToClean in SolutionRootFolder.GlobDirectories("**/bin"))
            {
                directoryToClean.DeleteDirectory();
            }
        });

    Target Compile => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
            );
        });

    [Parameter("Migration script name", Name = "name")] 
    [CanBeNull] 
    readonly string MigrationScriptName;

    Target AddMigration => _ => _
        .Requires(() => ConnectionString)
        .Requires(() => MigrationScriptName)
        .DependsOn(MigrateDatabase)
        .Executes(() =>
        {
            var scriptCreatorProject = GetProject(Projects.DatabaseScriptCreator);

            var scriptName =
                $"{DateTime.UtcNow:yyyyMMddHHmmss}{(string.IsNullOrWhiteSpace(MigrationScriptName) ? string.Empty : $"_{MigrationScriptName}")}.sql";

            var databaseBuildProject = GetProject(Projects.DatabaseBuildProjectName);

            var dacpacPath = databaseBuildProject.Directory / "bin" / Configuration 
                             / databaseBuildProject.GetTargetFramework()
                             / $"{databaseBuildProject.Name}.dacpac";

            DotNetTasks.DotNetRun(s =>
                    s.SetProjectFile(scriptCreatorProject.Path)
                        .AddApplicationArguments(
                            $"--connection-string",
                            ConnectionString,
                            $"--dacpac-path",
                            dacpacPath,
                            $"--output-script-path",
                            Database.SourceProject.Migrations / scriptName)
            );
        });

    Target MigrateDatabase => _ => _
        .Requires(() => !string.IsNullOrWhiteSpace(ConnectionString))
        .DependsOn(Compile)
        .Executes(() =>
        {
            if (!Database.SourceProject.Migrations.DirectoryExists())
                Database.SourceProject.Migrations.CreateDirectory();

            var dbUpMigratorProject = GetProject(Projects.DatabaseMigrator);

            DotNetTasks.DotNetRun(s =>
                s.SetProjectFile(dbUpMigratorProject.Path)
                    .SetApplicationArguments(ConnectionString, Database.SourceProject.Migrations)
            );
        });
}