﻿using System.Text.RegularExpressions;
using PWManager.CLI.Enums;

namespace PWManager.CLI.Parser; 

public class CommandParser {

    public IEnumerable<string> ParseCommandWithArguments(string cmd) {
        cmd = Regex.Replace(cmd, @"\s+", " ").Trim();
        var cursor = 0;
        while (cursor < cmd.Length) {
            
            var cursorEnd = cmd[cursor] switch {
                '"' => LookaheadTo(cmd, '"', cursor),
                '\'' => LookaheadTo(cmd, '\'', cursor),
                _ => LookaheadTo(cmd, ' ', cursor)
            };
            yield return cmd[cursor..++cursorEnd].Trim().Replace("\"", "").Replace("'","");
            cursor = cursorEnd;
        }
    }

    private int LookaheadTo(string cmd, char c, int cursorStart) {
        if (cursorStart < cmd.Length && cmd[cursorStart] == c)
            ++cursorStart;
        while (cursorStart < cmd.Length - 1 && cmd[cursorStart] != c) {
            ++cursorStart;
        }

        return cursorStart;
    }

    public AvailableCommands ParseCommand(string cmd) {
        try {
            return Enum.Parse<AvailableCommands>(cmd.ToUpper().Replace("-", "_"));
        }
        catch(Exception) {
            Console.WriteLine(UIstrings.UNKNOWN_COMMAND + cmd);
        }
        return AvailableCommands.HELP;
    }
}