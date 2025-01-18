using DatabaseMigrator;
using DbUp;
using DbUp.ScriptProviders;
using Serilog;
using Serilog.Formatting.Compact;

var logsPath = "logs\\migration-logs";

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(new CompactJsonFormatter(), logsPath)
    .CreateLogger();

logger.Information("Logger configured. Starting migration...");

if (args.Length != 2)
{
    logger.Error("Invalid arguments. Execution: DatabaseMigrator [connectionString] [pathToScripts]");

    logger.Information("Migration stopped");

    return -1;
}

var connectionString = args[0];

var scriptsPath = args[1];

if (!Directory.Exists(scriptsPath))
{
    logger.Information("Directory {ScriptsPath} does not exist", scriptsPath);

    return -1;
}

var serilogUpgradeLog = new SerilogUpgradeLog(logger);


EnsureDatabase.For.SqlDatabase(connectionString);

var upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsFromFileSystem(scriptsPath, new FileSystemScriptOptions
        {
            IncludeSubDirectories = true
        })
        .LogTo(serilogUpgradeLog)
        .JournalToSqlTable("dbo", "MigrationsJournal")
        .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    logger.Information("Migration failed");

    return -1;
}

logger.Information("Migration successful");

return 0;