using Flashcards.Models;
namespace Flashcards.Helpers
{
    internal static class Validation
    {
        public static bool StackExists(string stackName, List<Stack> existingStacks) => !existingStacks.Any(stack =>
                string.Equals(stack.Name, stackName, StringComparison.OrdinalIgnoreCase));
        
        public static bool isValidTextInput(string input) => !string.IsNullOrWhiteSpace(input);
    }
}
