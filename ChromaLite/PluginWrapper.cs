using IllusionPlugin;

namespace ChromaLite {
    public class PluginWrapper : IPlugin {

        public string Name => "ChromaLite";
        public string Version => "1.0.2";

        public Plugin PluginInstance;

        public void OnApplicationStart() {

            CosturaUtility.Initialize();
            InitializePlugin();
        }

        private void InitializePlugin() {
            PluginInstance = new Plugin();
            PluginInstance.OnApplicationStart();
        }

        public void OnApplicationQuit() {
            PluginInstance.OnApplicationQuit();
        }

        public void OnLevelWasLoaded(int level) {

        }

        public void OnLevelWasInitialized(int level) {

        }

        public void OnUpdate() {

        }

        public void OnFixedUpdate() {

        }

    }
}
