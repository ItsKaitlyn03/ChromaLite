using CustomUI.GameplaySettings;
using IllusionPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaLite {

    public static class ChromaLiteConfig {

        private static bool initialized = false;
        public static bool RGBLightsEnabled = true;
        public static bool SpecialEventsEnabled = false;

        public static void InitializeMenu() {
            
            if (!initialized) {
                initialized = true;
                RGBLightsEnabled = ModPrefs.GetBool("ChromaLite", "RGBLights", true);
                SpecialEventsEnabled = ModPrefs.GetBool("ChromaLite", "SpecialEvents", false);
            }

            GameplaySettingsUI.CreateSubmenuOption(GameplaySettingsPanels.PlayerSettingsRight, "ChromaLite", "MainMenu", "CLite", "ChromaLite events options");

            ToggleOption rgbLightsToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.PlayerSettingsRight, "RGB Lights", "CLite", "Enable/Disable RGB lighting events.");
            rgbLightsToggle.GetValue = RGBLightsEnabled;
            rgbLightsToggle.OnToggle += delegate (bool value) {
                RGBLightsEnabled = value;
                ModPrefs.SetBool("ChromaLite", "RGBLights", value);
            };
            rgbLightsToggle.AddConflict("Darth Maul");

            ToggleOption specialEventsToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.PlayerSettingsRight, "Special Events", "CLite", "Enable/Disable Special Events, such as note size changing, player heal/harm events, and rotation events.");
            specialEventsToggle.GetValue = SpecialEventsEnabled;
            specialEventsToggle.OnToggle += delegate (bool value) {
                SpecialEventsEnabled = value;
                ModPrefs.SetBool("ChromaLite", "SpecialEvents", value);
            };
            specialEventsToggle.AddConflict("Darth Maul");

        }

    }

}
