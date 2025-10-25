using Flashcards.DTOs;
using Flashcards.Models;
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

        public static T PromptMenuSelection<T>(Dictionary<string, T> options)
        {
            var selectedItem = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select an [blue]item[/] from the list:")
                    .AddChoices(options.Keys)
            );

            return options[selectedItem];
        }

        public static Dictionary<string, Stack> GetModelItems(List<Stack> stacks)
        {
            return stacks.ToDictionary(
                stack => StackDTO.ToDisplayString(stack),
                stack => stack
            );
        }
        public static Dictionary<string, Flashcard> GetModelItems(List<Flashcard> flashcards)
        {
            return flashcards.ToDictionary(
                card => $"{card.Question} (Answer: {card.Answer})",
                card => card
            );
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

        public static string PromptInput(string message)
        {
            return AnsiConsole.Ask<string>(message);
        }
    }
}
