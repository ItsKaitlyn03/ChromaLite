using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ChromaLite {

    class ChromaLiteBehaviour : MonoBehaviour {

        void Start() {
            StartCoroutine(ReadEvents());
        }

        IEnumerator ReadEvents() {
            yield return new WaitForSeconds(0f);

            MainGameSceneSetup mainGameSceneSetup = Resources.FindObjectsOfTypeAll<MainGameSceneSetup>().First();
            BeatmapDataModel dataModel = mainGameSceneSetup.GetField<BeatmapDataModel>("_beatmapDataModel");

            BeatmapData beatmapData = ReadBeatmapEvents(dataModel.beatmapData, ChromaLiteConfig.RGBLightsEnabled, ChromaLiteConfig.SpecialEventsEnabled);

            //ChromaLogger.Log("Events read!");
        }

        public BeatmapData ReadBeatmapEvents(BeatmapData beatmapData, bool rgbEvents, bool specialEvents) {
            //ChromaLogger.Log("Attempting to read lighting events");
            ChromaLiteMapReader.ReadMapData(beatmapData, rgbEvents, specialEvents); //.CreateTransformedData(beatmapData, ref chroma, ref mode, ref gameplayOptions, ref gameplayMode);
            return beatmapData;
        }

    }

}
