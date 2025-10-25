using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace Flashcards.Helpers
{
    internal static class Display
    {
        public static Dictionary<string, TEnum> GetMenuItems<TEnum>()
            where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>()
                .ToDictionary(option=> GetEnumDisplayName(option), option=> option);
        }

        private static string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return attribute?.Name ?? value.ToString();
        }

        public static TEnum PromptMenuSelection<TEnum>(Dictionary<string, TEnum> options)
            where TEnum : struct, Enum
        {
            var selectedItem = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select an [blue]item[/] from the list:")
                    .AddChoices(options.Keys)
            );

            return options[selectedItem];
        }

        public static bool YesNoPrompt(string message)
        {
            return AnsiConsole.Confirm(message);
        }

        public static void SetPageTitle(string title)
        {
            Console.Title = $"Flashcards - {title}";
        }

        public static void ShowTitle(string title)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[underline green]{title}[/]");
        }

        public static void PressToContinue()
        {
            AnsiConsole.Markup("[red]----------------------------[/]");
            AnsiConsole.MarkupLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
