using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChromaLite {

    public static class ColourManager {

        public const int RGB_INT_OFFSET = 2000000000;

        public static int ColourToInt(Color color) {
            int r = Mathf.FloorToInt(color.r * 255);
            int g = Mathf.FloorToInt(color.g * 255);
            int b = Mathf.FloorToInt(color.b * 255);
            return RGB_INT_OFFSET + (((r & 0x0ff) << 16) | ((g & 0x0ff) << 8) | (b & 0x0ff));
        }

        public static Color ColourFromInt(int rgb) {
            rgb = rgb - RGB_INT_OFFSET;
            int red = (rgb >> 16) & 0x0ff;
            int green = (rgb >> 8) & 0x0ff;
            int blue = (rgb) & 0x0ff;
            return new Color(red / 255f, green / 255f, blue / 255f, 1);
        }

        /*
         * TECHNICOLOR
         * Mostly just included for compatibility with ChromaToggle lightmaps.
         */

        public enum TechnicolourStyle {
            OFF = 0,
            WARM_COLD = 1,
            ANY_PALETTE = 2,
            PURE_RANDOM = 3
        }

        public static TechnicolourStyle GetTechnicolourStyleFromFloat(float f) {
            if (f == 1) return TechnicolourStyle.WARM_COLD;
            else if (f == 2) return TechnicolourStyle.ANY_PALETTE;
            else if (f == 3) return TechnicolourStyle.PURE_RANDOM;
            else return TechnicolourStyle.OFF;
        }

        public static bool technicolourEnabled = false;

        public static TechnicolourStyle _technicolourLights = TechnicolourStyle.OFF;
        public static TechnicolourStyle _technicolourSabers = TechnicolourStyle.OFF;
        public static TechnicolourStyle _technicolourBlocks = TechnicolourStyle.OFF;
        public static TechnicolourStyle _technicolourWalls = TechnicolourStyle.OFF;

        public static bool TechnicolourLights {
            get { return technicolourEnabled && _technicolourLights != TechnicolourStyle.OFF; }
        }

        public static bool TechnicolourSabers {
            get { return technicolourEnabled && _technicolourSabers != TechnicolourStyle.OFF; }
        }

        public static bool TechnicolourBlocks {
            get { return technicolourEnabled && _technicolourBlocks != TechnicolourStyle.OFF; }
        }

        public static bool TechnicolourBarriers {
            get { return technicolourEnabled && _technicolourWalls != TechnicolourStyle.OFF; }
        }

        public static Color[] TechnicolourWarmPalette { get; set; } = new Color[] { new Color(0f, 0.5f, 1f, 1f), new Color(0, 1, 0, 1), new Color(0, 0, 1, 1) };
        public static Color[] TechnicolourColdPalette { get; set; } = new Color[] { new Color(1, 0, 0, 1), new Color(1, 0, 1, 1) };

        public static Color GetTechnicolour(NoteData noteData, TechnicolourStyle style) {
            return GetTechnicolour(noteData.noteType == NoteType.NoteA, noteData.time, style);
        }

        public static Color GetTechnicolour(bool warm, float time, TechnicolourStyle style) {
            switch (style) {
                case TechnicolourStyle.ANY_PALETTE:
                    return GetEitherTechnicolour(time);
                case TechnicolourStyle.PURE_RANDOM:
                    return UnityEngine.Random.ColorHSV().ColorWithAlpha(1f);
                case TechnicolourStyle.WARM_COLD:
                    return warm ? GetWarmTechnicolour(time) : GetColdTechnicolour(time);
                default: return Color.clear;
            }
        }

        private static Color GetEitherTechnicolour(float time) {
            System.Random rand = new System.Random(Mathf.FloorToInt(8 * time));
            return rand.NextDouble() < 0.5 ? GetWarmTechnicolour(time) : GetColdTechnicolour(time);
        }

        private static Color GetWarmTechnicolour(float time) {
            System.Random rand = new System.Random(Mathf.FloorToInt(8 * time));
            return TechnicolourWarmPalette[rand.Next(0, TechnicolourWarmPalette.Length)];
        }

        private static Color GetColdTechnicolour(float time) {
            System.Random rand = new System.Random(Mathf.FloorToInt(8 * time));
            return TechnicolourColdPalette[rand.Next(0, TechnicolourColdPalette.Length)];
        }

        /*
         * LIGHTS
         */

        public static Color DefaultLightAmbient { get; set; } = new Color(0, 0.3765f, 0.5f, 1); //0, 192, 255

        public static Color DefaultLightA { get; } = new Color(1, 0.016f, 0.016f, 1); //255, 4, 4

        public static Color DefaultLightB { get; } = new Color(0, 0.753f, 1, 1); //0, 192, 255

        public static Color DefaultLightAltA { get; } = new Color(1, 0.032f, 1, 1); //255, 8, 255

        public static Color DefaultLightAltB { get; } = new Color(0.016f, 1, 0.016f, 1); //4, 255, 4

        public static Color DefaultLightWhite { get; } = new Color(1, 1, 1, 1); //Color.white

        public static Color DefaultLightGrey { get; } = new Color(0.6f, 0.6f, 0.6f, 1); //Color.white

        public static Color LightAmbient { get; set; } = new Color(0, 0.3765f, 0.5f, 1); //0, 192, 255

        public static Color LightA { get; set; } = new Color(1, 0, 0, 1);

        public static Color LightB { get; set; } = new Color(0, 0.502f, 1, 1);

        public static Color LightAltA { get; set; } = new Color(1, 0, 1, 1); //Color.magenta

        public static Color LightAltB { get; set; } = new Color(0, 1, 0, 1); //Color.green

        public static Color LightWhite { get; set; } = new Color(1, 1, 1, 1); //Color.white

        public static Color LightGrey { get; set; } = new Color(0.5f, 0.5f, 0.5f, 1); //128, 128, 128

        /*
         * BLOCKS / SABERS
         */

        public static Color DefaultA { get; } = new Color(1, 0, 0, 1);

        public static Color DefaultB { get; } = new Color(0, 0.502f, 1, 1);

        public static Color DefaultAltA { get; } = new Color(1, 0, 1, 1); //Color.magenta

        public static Color DefaultAltB { get; } = new Color(0, 1, 0, 1); //Color.green

        public static Color DefaultDoubleHit { get; } = new Color(0, 0, 0, 1); //Color.black

        public static Color DefaultNonColoured { get; } = new Color(1, 1, 1, 1); //Color.white

        public static Color DefaultSuper { get; set; } = new Color(1, 1, 0, 1);

        public static Color A { get; set; } = new Color(1, 0, 0, 1);

        public static Color B { get; set; } = new Color(0, 0.502f, 1, 1);

        public static Color AltA { get; set; } = new Color(1, 0, 1, 1); //Color.magenta

        public static Color AltB { get; set; } = new Color(0, 1, 0, 1); //Color.green

        public static Color DoubleHit { get; set; } = new Color(0, 0, 0, 1);

        public static Color NonColoured { get; set; } = new Color(1, 1, 1, 1);

        public static Color Super { get; set; } = new Color(1, 1, 0, 1);

        /*
         * OTHER
         */

        public static Color DefaultBarrierColour { get; } = Color.red;

        public static Color BarrierColour { get; set; } = DefaultBarrierColour;

        public static Color LaserPointerColour { get; set; } = B;

        public static Color SignA { get; set; } = LightA;

        public static Color SignB { get; set; } = LightB;



        public static LightSwitchEventEffect[] GetAllLightSwitches() {
            return Resources.FindObjectsOfTypeAll<LightSwitchEventEffect>();
        }

        public static void RecolourAllLights(Color red, Color blue) {
            LightSwitchEventEffect[] lights = GetAllLightSwitches();
            RecolourLights(ref lights, red, blue);
        }

        public static void RecolourLights(ref LightSwitchEventEffect[] lights, Color red, Color blue) {
            for (int i = 0; i < lights.Length; i++) {
                RecolourLight(ref lights[i], red, blue);
            }
        }

        public static void RecolourLight(ref LightSwitchEventEffect obj, Color red, Color blue) {
            if (obj.name.Contains("nightmare")) return;
            string[] sa = new string[] { "_lightColor0", "_highlightColor0", "_lightColor1", "_highlightColor1" };

            for (int i = 0; i < sa.Length; i++) {
                string s = sa[i];

                SimpleColorSO baseSO = SetupNewLightColourSOs(obj, s);

                Color newColour = i < sa.Length / 2 ? blue : red;
                if (newColour == Color.clear) continue;
                
                baseSO.SetColor(newColour);
            }
        }

        public static void RecolourAmbientLights(Color color) {
            List<TubeBloomPrePassLight> bls = UnityEngine.Object.FindObjectsOfType<TubeBloomPrePassLight>().ToList();
            LightSwitchEventEffect[] lights = GetAllLightSwitches();
            foreach (LightSwitchEventEffect light in lights) {
                BloomPrePassLight[] blsInLight = light.GetField<BloomPrePassLight[]>("_lights");
                foreach (BloomPrePassLight b in blsInLight) {
                    if (b is TubeBloomPrePassLight tb) bls.Remove(tb);
                }
            }
            foreach (TubeBloomPrePassLight tb in bls) {
                tb.SetField("_color", color);
                tb.color = color;
                Renderer[] rends = tb.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in rends) {
                    if (color == Color.black) rend.enabled = false;
                    else {
                        if (!rend.gameObject.name.StartsWith("NightmareLight_")) rend.enabled = true;
                        if (rend.materials.Length > 0) {
                            if (rend.material.shader.name == "Custom/ParametricBox" || rend.material.shader.name == "Custom/ParametricBoxOpaque") {
                                rend.material.SetColor("_Color", new Color(color.r * 0.5f, color.g * 0.5f, color.b * 0.5f, 1.0f));
                            }
                        }
                    }
                }
            }
        }

        public static SimpleColorSO SetupNewLightColourSOs(LightSwitchEventEffect light, String s) {
            return SetupNewLightColourSOs(light, s, Color.clear);
        }

        public static SimpleColorSO SetupNewLightColourSOs(LightSwitchEventEffect light, String s, Color overrideMultiplierColour) {
            MultipliedColorSO mColorSO = light.GetField<MultipliedColorSO>(s);
            SimpleColorSO baseSO = mColorSO.GetField<SimpleColorSO>("_baseColor");

            SimpleColorSO newBaseSO = ScriptableObject.CreateInstance<SimpleColorSO>();// new SimpleColorSO();
            newBaseSO.SetColor(baseSO.color);

            MultipliedColorSO newMColorSO = ScriptableObject.CreateInstance<MultipliedColorSO>();
            if (overrideMultiplierColour == Color.clear) {
                newMColorSO.SetField("_multiplierColor", mColorSO.GetField<Color>("_multiplierColor"));
            } else {
                newMColorSO.SetField("_multiplierColor", overrideMultiplierColour);
            }
            newMColorSO.SetField("_baseColor", newBaseSO);

            light.SetField(s, newMColorSO);
            if (!light.name.Contains("chroma")) light.name = light.name + "_chroma";
            return newBaseSO;
        }

        public static void SetupAllNewLightColourSOs() {
            LightSwitchEventEffect[] lights = GetAllLightSwitches();
            string[] sa = new string[] { "_lightColor0", "_highlightColor0", "_lightColor1", "_highlightColor1" };
            foreach (LightSwitchEventEffect light in lights) {
                for (int i = 0; i < sa.Length; i++) {
                    SetupNewLightColourSOs(light, sa[i]);
                }
            }
        }

    }

}
