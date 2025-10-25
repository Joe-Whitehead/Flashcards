using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums
{
    internal enum MainMenu
    {
        [Display(Name = "Study Flashcards")]
        StudyFlashcards = 1,
        [Display(Name = "Manage Flashcards")]
        ManageFlashcards,
        [Display(Name = "Manage Stacks")]
        ManageStacks,
        [Display(Name = "View Statistics")]
        ViewStatistics,
        [Display(Name = "Settings")]
        Settings,
        [Display(Name = "Exit Application")]
        ExitApplication
    }
}
