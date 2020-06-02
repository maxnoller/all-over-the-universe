using System.Collections;
using System.Collections.Generic;

public abstract class Command {
    public string name{get;set;}
    string help{get;set;}
    int argCount{get;set;}
    public delegate void CommandHandler(string[] args);
    CommandHandler handler {get; set;}
    ConsoleController controller{get;set;}
    public Command(ConsoleController controller){
        this.controller = controller;
    }

    public abstract void executeFunction(string[] args);
    public void CheckArgumentCountAndExecuteFunction(string[] args){
        if(args.Length > argCount){
            log("Wrong argument count");
            return;
        }
        executeFunction(args);
    }
    public CommandHandler executeCommandHandler(){
        CommandHandler ret = CheckArgumentCountAndExecuteFunction;
        return ret;
    }
    public void log(string log){
        controller.appendLogLine(name+": "+log);
    }
}