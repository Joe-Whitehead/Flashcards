using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums
{
    internal enum StackMenu
    {
        [Display(Name = "Create New Stack")]
        AddStack = 1,
        [Display(Name = "Edit Existing Stacks")]
        EditExistingStack,
        [Display(Name = "Delete Stack")]
        DeleteStack,
        [Display(Name = "View All Stacks")]
        ViewAllStacks,
        [Display(Name = "Return to Main Menu")]
        ReturnToMainMenu
    }
}
