using HarmonyLib;
using LevelGeneration;

namespace EOSExt.SecDoor.Patches
{
    /// <summary>
    /// Replaces the interaction text of doors that have a definition but no glitch style.
    /// Runs on every door state change, matching the previous behaviour.
    /// </summary>
    [HarmonyPatch]
    internal class CustomizeSecDoorInteractionText
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(LG_SecurityDoor), nameof(LG_SecurityDoor.Setup))]
        private static void Post_Customize_SecDoor_Interaction_Text(LG_SecurityDoor __instance)
        {
            var locks = __instance.m_locks.Cast<LG_SecurityDoor_Locks>();
            if (locks == null) return;

            if (!SecDoorIntTextOverrideManager.Current.TryGetDefinition(locks, out var def) || def.GlitchMode != Definition.GlitchMode.None)
                return;

            __instance.m_sync.add_OnDoorStateChange(new System.Action<pDoorState, bool>((state, isRecall) =>
            {
                SecDoorIntTextOverrideManager.Current.ReplaceText(locks, def);
            }));
        }
    }
}
