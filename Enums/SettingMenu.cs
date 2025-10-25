using System.ComponentModel.DataAnnotations;
namespace Flashcards.Enums
{
    internal enum SettingMenu
    {
        [Display(Name = "Insert Test Data")]
        InsertTestData = 1,
        [Display(Name = "Clear All Data")]
        ClearAllData,
        [Display(Name = "Back to Main Menu")]
        BackToMainMenu
    }
}
