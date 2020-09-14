using System;
using System.IO;
using SurveyMod.Implementation.App.Adapter.Repository;
using SurveyMod.Implementation.App.Command;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;

namespace SurveyMod.Implementation.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    CreateStorageDirectoryWhenNotExist();
                    if (RunProgram()) break;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error: {0}", exception.Message);
                }
            } while (true);
        }

        private static void CreateStorageDirectoryWhenNotExist()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Storage"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Storage");
            }
        }

        private static bool RunProgram()
        {
            Console.WriteLine("What commands do you want to run ? (/survey create help, /survey list help)");

            var command = new Command(Console.ReadLine());

            if (command.Input == "exit")
            {
                Console.WriteLine("...exiting");
                return true;
            }

            if (string.IsNullOrEmpty(command.Input))
            {
                throw new Exception("You must specify a command or type exit");
            }

            CheckUserCommandInput(command);

            new Runner(new JsonSurveyRepository(Directory.GetCurrentDirectory() + "\\Storage\\surveys.json"), new Facade(), new Player("123456789"), new Adapter.Command.Output.Console())
                .RunCommand(command);

            return false;
        }

        private static void CheckUserCommandInput(Command command)
        {
            var commands = command.SplitInputBySpace();
            
            if (commands.Length < 2)
            {
                throw new Exception("The command is wrong");
            }

            if (commands[0] != "survey")
            {
                throw new Exception("You must start with /survey {sub-command}");
            }
        }
    }
}