﻿using PWManager.Application.Context;
using PWManager.Application.Services.Interfaces;
using PWManager.CLI.Abstractions;
using PWManager.CLI.Enums;
using PWManager.CLI.Interfaces;
using Sharprompt;

namespace PWManager.CLI.Controllers {
    public class LoginController : IController {
        private readonly IApplicationEnvironment _env;
        private readonly ILoginService _loginService;
        public LoginController(IApplicationEnvironment env, ILoginService loginService) {
            _env = env;
            _loginService = loginService;
        }
        public ExitCondition Handle(string[] args) {
            if (_env.RunningSession) {
                throw new Exception("Command not available in a session!");
                // TODO: change to UserFeedbackException
            }

            string username = "";
            string path = "";

            int basepointer = 0;
            while(basepointer < args.Length) {
                if ((args[basepointer].Equals("-u") || args[basepointer].Equals("--username"))) {
                    if ((args.Length - basepointer <= 1) || args[basepointer + 1].StartsWith('-')) {
                        username = Prompt.Input<string>("Please enter your username");
                    } else {
                        username = args[basepointer + 1];
                        basepointer++;
                    }
                }
                else if ((args[basepointer].Equals("-d") || args[basepointer].Equals("--directory"))) {
                    if ((args.Length - basepointer <= 1) || args[basepointer + 1].StartsWith('-')) {
                        path = Prompt.Input<string>("Please enter the location of your databasefile");
                    } else {
                        path = args[basepointer + 1];
                        basepointer++;
                    }
                }
                basepointer++;
            }

            var lastUser = ReadDefaultFile();
            if (String.IsNullOrWhiteSpace(username)) {
                username = lastUser.Split('\n')[0];
            }
            if (String.IsNullOrWhiteSpace(path)) {
                path = lastUser.Split('\n')[1];
            }

            var pass = Prompt.Password("Enter your password");
            _loginService.Login(username, pass, path);

            WriteDefaultFile(username, path);

            Console.WriteLine($"Welcome {username} :)");
            return ExitCondition.CONTINUE;
        }

        private string ReadDefaultFile() {
            var defaultFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            try {
                return File.ReadAllText(Path.Combine(defaultFilePath, "last.txt"));
            } catch (IOException e) {
                throw new Exception("The file could not be read");
                // TODO: throw new UserFeedbackException
            }
        }
        private void WriteDefaultFile(string username, string path) {
            var defaultFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            try {
                File.WriteAllText(Path.Combine(defaultFilePath, "last.txt"), username + '\n' + path);
            } catch (IOException e) {
                throw new Exception("The file could not be written");
                // TODO: change to UserFeedbackException
            }
        }

    }
}
