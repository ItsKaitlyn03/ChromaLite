using HarmonyLib;
using IllusionInjector;
using IllusionPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChromaLite {
    public class Plugin {

        public static bool CTInstalled = false;

        Harmony harmony = new Harmony("net.binaryelement.chromalite");

        public void OnApplicationStart() {

            if (ChromaToggleInstalled()) {
                ChromaLogger.Log("ChromaToggle Detected, Disabling ChromaLite.");
                ChromaLogger.Log("ChromaToggle contains all features (and many more) of ChromaLite.");
                CTInstalled = true;
                return;
            }

            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
            //ChromaLogger.Log("Harmonized");

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
            if (arg0.name == "Menu" && !CTInstalled) {
                ChromaLiteConfig.InitializeMenu();
            } else if (arg0.name == "GameCore") {
                //ChromaLogger.Log("Transitioning into GameCore");
                new GameObject("ChromaLiteReader").AddComponent<ChromaLiteBehaviour>();
                return;
            }
        }

        public void OnApplicationQuit() {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        public static bool ChromaToggleInstalled() {
            foreach (IPlugin plugin in PluginManager.Plugins) {
                if (plugin.ToString() == "ChromaToggle.Plugin") {
                    return true;
                }
            }
            return false;
        }

    }
}
