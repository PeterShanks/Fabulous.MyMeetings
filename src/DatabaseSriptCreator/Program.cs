using System.CommandLine;
using System.Text.RegularExpressions;
using DatabaseScriptCreator;
using Microsoft.SqlServer.Dac;

var connectionStringOption = new Option<string>(
    "--connection-string",
    "Database connection")
{
    IsRequired = true
};
connectionStringOption.AddAlias("-cs");

var dacpacPathOption = new Option<string>(
    "--dacpac-path",
    "Path to the DACPAC file")
{
    IsRequired = true
};
dacpacPathOption.AddAlias("-dp");

var outputScriptPathOption = new Option<string>(
    "--output-script-path",
    "Path to save the generated script")
{
    IsRequired = true
};
outputScriptPathOption.AddAlias("-o");

var rootCommand = new RootCommand
{
    connectionStringOption,
    dacpacPathOption,
    outputScriptPathOption
};

rootCommand.SetHandler((connectionString, dacpacPath, outputPath) =>
{
    var databaseNameRegex = new Regex("(?<=\\b(?:Database|Initial Catalog)=)[^;]+", RegexOptions.Compiled);
    var match = databaseNameRegex.Match(connectionString);
    if (!match.Success)
    {
        Console.WriteLine("Database name not found in the connection string");
        return;
    }
    var targetDatabaseName = match.Value;

    CreateMigrationScript(connectionString, dacpacPath, outputPath, targetDatabaseName);
}, connectionStringOption, dacpacPathOption, outputScriptPathOption );

await rootCommand.InvokeAsync(args);

static void CreateMigrationScript(string connectionString, string dacpacPath, string outputScriptPath, string targetDatabaseName)
{
    var dacServices = new DacServices(connectionString);

    // Load the DACPAC file
    using var dacpac = DacPackage.Load(dacpacPath);
    var options = new DacDeployOptions
    {
        // You can customize the options as needed
        BlockOnPossibleDataLoss = true,
        DropObjectsNotInSource = false,
    };

    // Generate the deployment script to reach the desired state in the DACPAC
    string deployScript = dacServices.GenerateDeployScript(dacpac, targetDatabaseName, options);

    // Transform the script to remove SQLCMD commands and replace variables
    var scriptManipulator = new SqlScriptManipulator();
    string transformedScript = scriptManipulator.Transform(deployScript, targetDatabaseName);

    // Save the generated script to a file
    File.WriteAllText(outputScriptPath, transformedScript);
    Console.WriteLine($"Script generated successfully at: {outputScriptPath}");
}