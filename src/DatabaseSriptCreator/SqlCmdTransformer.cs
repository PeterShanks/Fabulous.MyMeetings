using System.Text.RegularExpressions;

namespace DatabaseScriptCreator;

public class SqlScriptManipulator()
{
    public string Transform(string sqlScript, string databaseName)
    {
        var variableReplacedScript = ReplaceVariables(sqlScript, ExtractVariables(sqlScript));
        var truncatedScript = TruncateScript(variableReplacedScript, databaseName);

        return truncatedScript;
    }

    private static Dictionary<string, string> ExtractVariables(string script)
    {
        var variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var pattern = new Regex(@"^\s*:setvar\s+(?<Name>\w+)\s+""(?<Value>.+)"".*$", RegexOptions.Compiled | RegexOptions.Multiline);

        foreach (Match match in pattern.Matches(script))
        {
            var name = match.Groups["Name"].Value;
            var value = match.Groups["Value"].Value.Trim('"');
            variables[name] = value;
        }

        return variables;
    }

    private static string ReplaceVariables(string script, Dictionary<string, string> variables)
    {
        var replacedScript = script;
        foreach (var variable in variables)
        {
            var pattern = $@"\$\({variable.Key}\)";
            replacedScript = Regex.Replace(replacedScript, pattern, variable.Value, RegexOptions.IgnoreCase);
        }

        return replacedScript;
    }

    private static string TruncateScript(string script, string databaseName)
    {
        // Remove :setvar and other SQLCMD commands
        var sqlCmdPattern = @"^\s*:(setvar|r|connect|on error).*?$";

        var transformedScript = Regex.Replace(script, sqlCmdPattern, string.Empty, RegexOptions.Multiline).Trim();

        var multilineCommentPattern = new Regex(@"\/\*[\s\S]*?\*\/", RegexOptions.Compiled);

        transformedScript = multilineCommentPattern.Replace(transformedScript, string.Empty);

        var batches = transformedScript.Split("GO", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        batches = batches.SkipWhile(b => !b.StartsWith($"use [{databaseName}]", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        return batches.Length == 2 
            ? string.Empty
            : string.Join($"{Environment.NewLine}GO{Environment.NewLine}{Environment.NewLine}", batches);
    }
}