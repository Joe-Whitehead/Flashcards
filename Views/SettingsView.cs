using Spectre.Console;
using Flashcards.Enums;
using Flashcards.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace Flashcards.Views
{
    internal class SettingsView
    {
        private readonly SettingService _settings;

        public SettingsView()
        {
                _settings = new SettingService();
        }
    }
}
