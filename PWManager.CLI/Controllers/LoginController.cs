﻿using PWManager.Application.Services.Interfaces;
using PWManager.CLI.Abstractions;
using PWManager.CLI.Attributes;
using PWManager.CLI.Enums;
using PWManager.CLI.Interfaces;
using PWManager.Data.Abstraction;

namespace PWManager.CLI.Controllers {
    
    [NoSession]
    public class LoginController : IController {

        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService) {

            _loginService = loginService;
        }

        
        public ExitCondition Handle(string[] args) {
            (var username, var path) = ParseArgs(args);

            if(ConfigFileHandler.DefaultFileExists()) {
                var lastUser = ConfigFileHandler.ReadDefaultFile().Split('\n');

                if(lastUser.Length == 2) {
                    if (String.IsNullOrWhiteSpace(username)) {
                        username = lastUser[0];
                    }
                    if(String.IsNullOrWhiteSpace(path)) {
                        path = lastUser[1];
                    }
                }
            }
            if(String.IsNullOrWhiteSpace(username)) {
                username = AskForInput(UIstrings.ENTER_USERNAME);
            }
            if(String.IsNullOrWhiteSpace(path)) {
                path = AskForInput(UIstrings.ENTER_PATH);
            }

            var succ = PromptHelper.InputPassword((p) => _loginService.Login(username, p, path));

            if(!succ) {
                return ExitCondition.EXIT;
            }

            ConfigFileHandler.WriteDefaultFile(username, path);
            Console.WriteLine(UIstrings.WelcomeMessage(username));
            return ExitCondition.CONTINUE;
        }

        public (string, string) ParseArgs(string[] args) {
            string username = "";
            string path = "";

            int basepointer = 0;
            while (basepointer < args.Length) {
                if ((args[basepointer].Equals("-u") || args[basepointer].Equals("--username"))) {
                    if ((args.Length - basepointer <= 1) || args[basepointer + 1].StartsWith('-')) {
                        username = AskForInput(UIstrings.ENTER_USERNAME);
                    } else {
                        username = args[basepointer + 1];
                        basepointer++;
                    }
                } else if ((args[basepointer].Equals("-d") || args[basepointer].Equals("--directory"))) {
                    if ((args.Length - basepointer <= 1) || args[basepointer + 1].StartsWith('-')) {
                        path = AskForInput(UIstrings.ENTER_PATH);
                    } else {
                        path = args[basepointer + 1];
                        basepointer++;
                    }
                }
                basepointer++;
            }

            return (username, path);
        }

        public virtual string AskForInput(string prompt) {
            return ConsoleInteraction.Input<string>(prompt);
        }
    }
}
