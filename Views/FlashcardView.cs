using Spectre.Console;
using Flashcards.Enums;
using Flashcards.Helpers;
using Flashcards.Controllers;
using Flashcards.DTOs;

namespace Flashcards.Views
{
    internal class FlashcardView
    {
        private readonly FlashcardController _flashcardController;

        public FlashcardView()
        {
            _flashcardController = new FlashcardController();
        }

        public void Run()
        {
            while (true)
            {
                //Clear the Console and set the main titles
                Console.Clear();
                Display.SetPageTitle("Flashcards");
                Display.ShowTitle("Flashcard Menu");
                ShowFlashcardMenu();
            }
        }

        private void ShowFlashcardMenu()
        {
            //Get our Flashcard menu items and prompt user for selection
            var menuItems = Display.GetMenuItems<FlashcardMenu>();
            var selectedOption = Display.PromptMenuSelection(menuItems);
            //Handle user selection
            switch (selectedOption)
            {
                case FlashcardMenu.AddNewFlashcard:
                    var newFlashcardDto = Flashcard();
                    var created = _flashcardController.CreateFlashcard(newFlashcardDto);
                    if (created)
                    {
                        AnsiConsole.MarkupLine("[green]Flashcard created successfully![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Failed to create flashcard.[/]");
                    }
                    Display.PressToContinue();
                    break;
                case FlashcardMenu.EditExistingFlashcard:
                    break;
                case FlashcardMenu.DeleteFlashcard:
                    break;
                case FlashcardMenu.ViewAllFlashcards:
                    var flashcards = _flashcardController.GetAllFlashcards();
                    foreach (var flashcard in flashcards)
                    {
                        AnsiConsole.MarkupLine("[grey]----------------------------------------[/]");
                        DisplayFlashcard(flashcard);                        
                    }
                    Display.PressToContinue();
                    break;
                case FlashcardMenu.ReturnToMainMenu:
                    return;
            }
        }

        private FlashcardDTO Flashcard()
        {
            var flashcardDto = new FlashcardDTO();
            flashcardDto.Question = AnsiConsole.Ask<string>("Enter the [green]question[/]:");
            flashcardDto.Answer = AnsiConsole.Ask<string>("Enter the [green]answer[/]:");
            return flashcardDto;
        }

        private void DisplayFlashcard(FlashcardDTO flashcard)
        {
            AnsiConsole.MarkupLine($"""
                [bold yellow]Flashcard ID:[/] {flashcard.Id}
                [bold yellow]Question:[/] {flashcard.Question}
                [bold yellow]Answer:[/] {flashcard.Answer}
                [bold yellow]Stack Name:[/] {flashcard.StackName}
                """);
                
        }
    }
}
