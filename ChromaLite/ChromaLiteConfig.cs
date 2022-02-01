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

        }

    }

}
