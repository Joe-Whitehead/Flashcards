using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums
{
    internal enum FlashcardMenu
    {
        [Display(Name = "Add New Flashcard")]
        AddNewFlashcard = 1,
        [Display(Name = "Edit Existing Flashcard")]
        EditExistingFlashcard,
        [Display(Name = "Delete Flashcard")]
        DeleteFlashcard,
        [Display(Name = "View All Flashcards")]
        ViewAllFlashcards,
        [Display(Name = "Return to Main Menu")]
        ReturnToMainMenu
    }
}
