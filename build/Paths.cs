using Nuke.Common.IO;
using static Nuke.Common.NukeBuild;

namespace _build
{
    public class Paths
    {
        public static AbsolutePath SolutionRootFolder => RootDirectory / "src";


        public static class Database
        {
            public static AbsolutePath DatabaseFolder => SolutionRootFolder / "Database";

            public static class SourceProject
            {
                public static AbsolutePath Folder => DatabaseFolder / "Fabulous.MyMeetings.Database";
                public static AbsolutePath Scripts => Folder / "Scripts";
                public static AbsolutePath Migrations => Scripts / "Migrations";
            }

            public static class Build
            {
                public static AbsolutePath Folder => DatabaseFolder / "Fabulous.MyMeetings.Database.Build";
                
            }

            public static class Migrator
            {
                public static AbsolutePath Folder => DatabaseFolder / "Fabulous.MyMeetings.Database.Migrator";
            }
            public static AbsolutePath DbUpMigratorExePath => DatabaseFolder / "DatabaseMigrator" ;
        }
    }
}
