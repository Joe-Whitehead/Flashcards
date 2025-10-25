using Spectre.Console;
using Flashcards.Enums;
using Flashcards.Helpers;
using Flashcards.Controllers;
using Flashcards.DTOs;
using Flashcards.Models;

namespace Flashcards.Views
{
    internal class MainView
    {
        public MainView()
        {
            
        }

        public void Run()
        {
            while (true)
            {
                //Clear the Console and set the main titles
                Console.Clear();
                Display.SetPageTitle("Home");
                Display.ShowTitle("Main Menu");

                //Get our Main menu items and prompt user for selection
                var menuItems = Display.GetMenuItems<MainMenu>();
                var selectedOption = Display.PromptMenuSelection(menuItems);

                //Handle user selection
                switch (selectedOption)
                {
                    case MainMenu.StudyFlashcards:
                        break;

                    case MainMenu.ManageStacksCards:
                        ManageStacksAndCards();
                        break;

                    case MainMenu.ViewStatistics:                        
                        break;

                    case MainMenu.Settings:
                        SettingsView.Run();
                        break;

                    case MainMenu.ExitApplication:
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Thank you for using Flashcards! Goodbye![/]");
                        Environment.Exit(0);
                        break;
                }
                        //Display.PressToContinue();
            }
        }

        private void ManageStacksAndCards()
        {
            StackController stackController = new StackController();
            Console.Clear();
            Display.SetPageTitle("Manage");
            Display.ShowTitle("Manage Stacks & Flashcards");

            var menuItems = Display.GetMenuItems<StackMenu>();
            var selectedOption = Display.PromptMenuSelection(menuItems);

            switch (selectedOption)
            {
                case StackMenu.AddStack:
                    string stackName = Display.PromptInput("Enter the name of the new stack: ");
                    List<FlashcardDTO> flashcards = [];

                    if (Display.YesNoPrompt("Would you like to add a flashcard to this stack now?"))
                    {
                        while (true)
                        {
                            string question = Display.PromptInput("Enter the question for the flashcard: ");
                            string answer = Display.PromptInput("Enter the answer for the flashcard: ");
                            flashcards.Add(new FlashcardDTO
                            {
                                Question = question,
                                Answer = answer
                            });
                            if (!Display.YesNoPrompt("Would you like to add another flashcard?"))
                            {
                                break;
                            }
                        }
                    }
                        stackController.AddNewStack(new StackDTO
                    {
                        Name = stackName,                        
                        Flashcards = flashcards
                    });
                    break;
                case StackMenu.EditExistingStack:
                    var stackList = stackController.GetAllStacks();
                    var stackMenuItems = Display.GetModelItems(stackList);
                    var selectedStack = Display.PromptMenuSelection<Stack>(stackMenuItems);

                    stackController.EditStack(selectedStack);

                    break;
                case StackMenu.DeleteStack:
                    break;
                case StackMenu.ViewAllStacks:
                    break;
                case StackMenu.ReturnToMainMenu:
                    return;
            }

        }
    }
}
