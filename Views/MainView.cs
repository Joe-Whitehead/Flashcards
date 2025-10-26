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
        private readonly StackController _stackController;
        private readonly FlashcardController _flashcardController;

        public MainView()
        {
            _stackController = new StackController();
            _flashcardController = new FlashcardController();
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
                            flashcards.Add(PromptFlashcardDetails());

                            if (!Display.YesNoPrompt("Would you like to add another flashcard?"))
                            {
                                break;
                            }
                        }
                    }
                    _stackController.AddNewStack(new StackDTO
                    {
                        Name = stackName,
                        Flashcards = flashcards
                    });
                    break;

                case StackMenu.EditExistingStack:
                    var stackList = _stackController.GetAllStacks();
                    var stackMenuItems = Display.GetModelItems(stackList);
                    var selectedStack = Display.PromptMenuSelection<Stack>(stackMenuItems);
                    GetStackSubMenu(selectedStack);
                    break;
            
                case StackMenu.DeleteStack:
                    break;
                case StackMenu.ViewAllStacks:
                    break;
                case StackMenu.ReturnToMainMenu:
                    return;
            }
        }

        private void GetStackSubMenu(Stack stack)
        {
            Dictionary<string, Flashcard> flashcards;
            Flashcard selectedFlashcard;
            Console.Clear();
            Display.SetPageTitle("Manage");
            Display.ShowTitle("Stack Management Menu");
            var menuItems = Display.GetMenuItems<EditStackMenu>();
            var selectedOption = Display.PromptMenuSelection(menuItems);
            switch (selectedOption)
            {
                case EditStackMenu.AddFlashcardToStack:
                    var flashcardDto = PromptFlashcardDetails();                    
                    _stackController.AddFlashcardToStack(stack.Id, flashcardDto);
                    break;

                case EditStackMenu.EditFlashcardInStack:
                    flashcards = Display.GetModelItems(stack.Flashcards);
                     selectedFlashcard = Display.PromptMenuSelection<Flashcard>(flashcards);
                    var updatedFlashcardDto = PromptFlashcardDetails(selectedFlashcard.Question, selectedFlashcard.Answer);

                    _flashcardController.UpdateFlashcard(selectedFlashcard.Id, updatedFlashcardDto);
                    break;

                case EditStackMenu.DeleteFlashcardFromStack:
                    flashcards = Display.GetModelItems(stack.Flashcards);
                    selectedFlashcard = Display.PromptMenuSelection<Flashcard>(flashcards);
                    if (Display.YesNoPrompt("Are you sure you want to delete this flashcard?"))
                    {
                        _flashcardController.DeleteFlashcard(selectedFlashcard.Id);
                        AnsiConsole.MarkupLine("[green]Flashcard deleted successfully.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Deletion cancelled.[/]");
                    }
                    Display.PressToContinue();
                    break;
                case EditStackMenu.ReturnToManageStacksMenu:
                    return;
                    
            }
        }

        private FlashcardDTO PromptFlashcardDetails(string existingQuestion = null, string existingAnswer = null)
        {
            if (existingQuestion != null && existingAnswer != null)
            {
                AnsiConsole.MarkupLine($"Current Question: [blue]{existingQuestion}[/]");
                AnsiConsole.MarkupLine($"Current Answer: [blue]{existingAnswer}[/]");
            }
            string question = Display.PromptInput($"Enter the question for the flashcard: ");
            string answer = Display.PromptInput("Enter the answer for the flashcard: ");
            return new FlashcardDTO
            {
                Question = question,
                Answer = answer
            };
        }
    }
}
