using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
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

    public static int Main () => Execute<Build>(x => x.Compile);

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
            var dbUpMigratorProject = Solution.GetProject("DatabaseMigrator");

            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(dbUpMigratorProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OutputDbUpMigratorBuildDirectory)
            );
        });

    [Parameter("Modular Monolith database connection string")] readonly string DatabaseConnectionString;

    Target MigrateDatabase => _ => _
        .Requires(() => DatabaseConnectionString != null)
        .DependsOn(CompileDbUpMigrator)
        .Executes(() =>
        {
            var migrationsPath = DatabaseDirectory / "Migrations";

            DotNetTasks.DotNet($"{DbUpMigratorPath} {DatabaseConnectionString} {migrationsPath}");
        });

    #endregion
}
