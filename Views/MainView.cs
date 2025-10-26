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
                    GetStackSubMenu(DisplayStackMenu());
                    break;
            
                case StackMenu.DeleteStack:
                    /*var stacks = _stackController.GetAllStacks();
                    var stackItems = Display.GetModelItems(stacks);
                    var stackToDelete = Display.PromptMenuSelection<Stack>(stackItems);*/
                    var stackToDelete = DisplayStackMenu();
                    if (Display.YesNoPrompt("Are you sure you want to delete this stack? (This will also delete any Flashcards in the stack)"))
                    {
                        _stackController.DeleteStack(stackToDelete.Id);
                        AnsiConsole.MarkupLine("[green]Stack deleted successfully.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Deletion cancelled.[/]");
                    }
                    Display.PressToContinue();
                   /* foreach (var stack in stacks)
                        RefreshStack(stack);*/
                    
                    break;
                case StackMenu.ViewAllStacks:
                    break;
                case StackMenu.ReturnToMainMenu:
                    return;
            }
        }

        private void GetStackSubMenu(Stack stack)
        {
            bool exitSubMenu = false;
            while (!exitSubMenu)
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
                    case EditStackMenu.RenameStack:
                        string newName = Display.PromptInput("Enter the new name for the stack: ");
                        stack.Name = newName;
                        if (_stackController.EditStack(stack))
                        {
                            AnsiConsole.MarkupLine("[green]Stack renamed successfully.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Failed to rename stack.[/]");
                        }
                        Display.PressToContinue();
                        RefreshStack(stack);
                        break;
                    case EditStackMenu.AddFlashcardToStack:
                        var flashcardDto = PromptFlashcardDetails();
                        if (_stackController.AddFlashcardToStack(stack.Id, flashcardDto))
                        {
                            AnsiConsole.MarkupLine("[green]Flashcard added successfully.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Failed to add flashcard.[/]");
                        }
                        Display.PressToContinue();
                        RefreshStack(stack);
                        break;

                    case EditStackMenu.EditFlashcardInStack:
                        flashcards = Display.GetModelItems(stack.Flashcards);
                        selectedFlashcard = Display.PromptMenuSelection<Flashcard>(flashcards);
                        var updatedFlashcardDto = PromptFlashcardDetails(selectedFlashcard.Question, selectedFlashcard.Answer);

                        if (_flashcardController.UpdateFlashcard(selectedFlashcard.Id, updatedFlashcardDto))
                        {
                            AnsiConsole.MarkupLine("[green]Flashcard updated successfully.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Failed to update flashcard.[/]");
                        }
                        Display.PressToContinue();
                        RefreshStack(stack);
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
                        RefreshStack(stack);
                        break;
                    case EditStackMenu.ReturnToMainMenu:
                        exitSubMenu = true;
                        break;
                }                    
            }
        }

        private FlashcardDTO PromptFlashcardDetails(string? existingQuestion = null, string? existingAnswer = null)
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

        private void RefreshStack(Stack stack)
        {
            var updatedStack = _stackController.GetStackById(stack.Id);

            // Mutate the original object
            stack.Flashcards = updatedStack.Flashcards;
        }

        private Stack DisplayStackMenu()
        {
            var stackList = _stackController.GetAllStacks();
            var stackMenuItems = Display.GetModelItems(stackList);
            return Display.PromptMenuSelection<Stack>(stackMenuItems);
        }
    }
}
