using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleController
{
    private ConsoleModel console_model = new ConsoleModel();

    void registerCommand(Command command){
        this.console_model.commands[command.name] = command;
    }

    bool checkIfCommandExists(string command){
        if(this.console_model.commands.ContainsKey(command))
            return true;
        this.appendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
        return false;
    }

    public void appendLogLine(string log_line){
        this.console_model.log_string_builder.Append(log_line+"\n");
    }

    void appendToCommandHistory(string command_string){
        this.console_model.command_history.Add(command_string);
    }

    string getLog(){
        return this.console_model.log_string_builder.ToString();
    }

    void runCommandString(string command_string) {
        this.appendLogLine("$ " + command_string);
        
        string[] commandSplit = parseArguments(command_string);
        string[] args = new string[0];

        if (commandSplit.Length <= 1){
            runCommand(command_string, args);
            return;
        }

        int numArgs = commandSplit.Length - 1;
        args = new string[numArgs];
        System.Array.Copy(commandSplit, 1, args, 0, numArgs);
        runCommand(commandSplit[0].ToLower(), args);
    }

    public void runCommand(string command, string[] args) {
        if(!checkIfCommandExists(command))
            return;
        Command.CommandHandler handler = this.console_model.commands[command].executeCommandHandler();
        handler(args);
        this.appendToCommandHistory(command);
    }

    public static string[] parseArguments(string command_string){
        if(command_string == "")
            return new string[0];

        LinkedList<char> parmChars = new LinkedList<char>(command_string.ToCharArray());
        bool inQuote = false;

        var node = parmChars.First;
        while (node != null){
            var next = node.Next;
            if (node.Value == '"') {
                inQuote = !inQuote;
                parmChars.Remove(node);
            }
            if (!inQuote && node.Value == ' ') {
                node.Value = '\n';
            }
            node = next;
        }
        char[] parmCharsArr = new char[parmChars.Count];
        parmChars.CopyTo(parmCharsArr, 0);
        return (new string(parmCharsArr)).Split(new char[] {'\n'} , System.StringSplitOptions.RemoveEmptyEntries);
    }
}
