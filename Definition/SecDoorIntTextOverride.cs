using AmorLib.Utils.JsonElementConverters;
using ExtraObjectiveSetup.BaseClasses;
using LevelGeneration;

namespace EOSExt.SecDoor.Definition
{
    public enum GlitchMode
    {
        None,
        Style1,
        Style2
    }

    public class SecDoorIntTextOverride : ExtraObjectiveSetup.BaseClasses.GlobalZoneIndex
    {
        public Localization.LocalizedText Prefix { get; set; } = null;

        public Localization.LocalizedText Postfix { get; set; } = null;

        public Localization.LocalizedText TextToReplace { get; set; } = null;

        public eDoorStatus[] ActiveTextOverrideWhitelist { get; set; } = new eDoorStatus[0];

        public GlitchMode GlitchMode { get; set; } = GlitchMode.None;

        public LocaleText Style2Prefix { get; set; } = LocaleText.Empty;

        public LocaleText Style2Postfix { get; set; } = LocaleText.Empty;

        public LocaleText Style2Text { get; set; } = new LocaleText(841);

        public eDoorStatus[] ActiveGlitchStatusWhitelist { get; set; } = new eDoorStatus[0];
    }
}
