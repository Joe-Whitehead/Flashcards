using Spectre.Console;
using Flashcards.Enums;
using Flashcards.Helpers;

namespace Flashcards.Views
{
    internal class FlashcardView
    {
        public FlashcardView()
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

                    case MainMenu.ManageFlashcards:
                        var flashcardMenu = Display.GetMenuItems<FlashcardMenu>();
                        var flashcardOption = Display.PromptMenuSelection(flashcardMenu);
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
    }
}
