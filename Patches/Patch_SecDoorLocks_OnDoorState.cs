using HarmonyLib;
using LevelGeneration;

namespace EOSExt.SecDoor.Patches
{
    /// <summary>
    /// Re-applies the configured interaction text once the door locks itself behind a chained puzzle,
    /// which is when vanilla rewrites the interaction message.
    /// </summary>
    [HarmonyPatch(typeof(LG_SecurityDoor_Locks), nameof(LG_SecurityDoor_Locks.OnDoorState))]
    internal static class Patch_SecDoorLocks_OnDoorState
    {
        [HarmonyPostfix]
        [HarmonyPriority(Priority.Normal)]
        [HarmonyWrapSafe]
        private static void Post_LG_SecurityDoor_Locks_OnDoorState(LG_SecurityDoor_Locks __instance, pDoorState state)
        {
            if (state.status != eDoorStatus.Closed_LockedWithChainedPuzzle && state.status != eDoorStatus.Closed_LockedWithChainedPuzzle_Alarm)
                return;

            SecDoorIntTextOverrideManager.Current.ReplaceText(__instance);
        }
    }
}
