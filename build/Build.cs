using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

public partial class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    static readonly IConfiguration Settings = new ConfigurationBuilder()
        .AddUserSecrets(typeof(Build).Assembly)
        .Build();

    public static int Main () => Execute<Build>(x => x.MigrateDatabase);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    AbsolutePath WorkingDirectory => RootDirectory / ".nuke-working-directory";

    AbsolutePath OutputDirectory => WorkingDirectory / "output";

    AbsolutePath OutputDbUpMigratorBuildDirectory => OutputDirectory / "dbUpMigrator";

    AbsolutePath InputFilesDirectory => WorkingDirectory / "input-files";

    AbsolutePath DatabaseDirectory => RootDirectory / "src" / "Database" / "Fabulous.MyMeetings.Database" / "Scripts";
    AbsolutePath DbUpMigratorPath => OutputDbUpMigratorBuildDirectory / "DatabaseMigrator.dll";


    #region Main
    Target Clean => _ => _
        .Executes(() =>
        {
            WorkingDirectory.CreateOrCleanDirectory();
        });

    Target Compile => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration));
        });

    Target UnitTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetFilter("UnitTests")
                .SetConfiguration(Configuration)
            );
        });

    Target ArchitectureTests => _ => _
        .DependsOn(UnitTests)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetFilter("ArchTests")
                .SetConfiguration(Configuration)
            );
        });

    Target BuildAndUnitTests => _ => _
        .Triggers(ArchitectureTests)
        .Executes(() =>
        {
        });
    #endregion

    #region Database
    Target CompileDbUpMigrator => _ => _
        .Executes(() =>
        {
            var dbUpMigratorProject = Solution.GetAllProjects("DatabaseMigrator").FirstOrDefault();

            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(dbUpMigratorProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OutputDbUpMigratorBuildDirectory)
            );
        });

    [Parameter("Connection string")] readonly string ConnectionString = Settings.GetConnectionString("MyMeetings");

    Target MigrateDatabase => _ => _
        .Requires(() => ConnectionString != null)
        .DependsOn(CompileDbUpMigrator)
        .Executes(() =>
        {
            var migrationsPath = DatabaseDirectory / "Migrations";

            DotNetTasksExtensions.DotNet($@"""{DbUpMigratorPath}"" ""{ConnectionString}"" ""{migrationsPath}""");
        });

    #endregion
}

public static class DotNetTasksExtensions
{
    public static IReadOnlyCollection<Output> DotNet(string command, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Action<IProcess> exitHandler = null)
    {
        using var process = ProcessTasks.StartProcess(DotNetTasks.DotNetPath, command, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger ?? DotNetTasks.DotNetLogger);
        (exitHandler ?? (p => DotNetTasks.DotNetExitHandler.Invoke(null, p))).Invoke(process.AssertWaitForExit());
        return process.Output;
    }
}