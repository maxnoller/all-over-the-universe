using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ConsoleModel{
    public Dictionary<string, Command> commands;
    public List<string> command_history;
    public StringBuilder log_string_builder;
    
    public ConsoleModel(){
        this.command_history = new List<string>();
        this.commands = new Dictionary<string, Command>();
        log_string_builder = new StringBuilder("");
    }

}
