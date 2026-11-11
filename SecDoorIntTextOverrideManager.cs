using EOSExt.SecDoor.Definition;
using ExtraObjectiveSetup.BaseClasses;
using ExtraObjectiveSetup.Utils;
using LevelGeneration;
using System.Linq;
using System.Text;

namespace EOSExt.SecDoor
{
    public class SecDoorIntTextOverrideManager : ZoneDefinitionManager<SecDoorIntTextOverride>
    {
        public static SecDoorIntTextOverrideManager Current { get; } = new();

        protected override string DEFINITION_NAME => "SecDoorIntText";

        private SecDoorIntTextOverrideManager() { }

        static SecDoorIntTextOverrideManager() { }

        public bool TryGetDefinition(LG_SecurityDoor_Locks locks, out SecDoorIntTextOverride def)
        {
            def = null;
            var door = locks?.m_door;
            if (door == null) return false;

            def = GetDefinition(door.Gate.DimensionIndex, door.LinksToLayerType, door.LinkedToZoneData.LocalIndex);
            return def != null;
        }

        public void ReplaceText(LG_SecurityDoor_Locks locks, SecDoorIntTextOverride def = null)
        {
            if (def == null && !TryGetDefinition(locks, out def)) return;

            if (def.ActiveTextOverrideWhitelist != null && def.ActiveTextOverrideWhitelist.Length > 0
                && !def.ActiveTextOverrideWhitelist.Contains(locks.m_lastStatus))
            {
                return;
            }

            locks.m_intCustomMessage.m_message = Replace(locks.m_intCustomMessage.m_message);
            locks.m_intOpenDoor.InteractionMessage = Replace(locks.m_intOpenDoor.InteractionMessage);
            locks.m_intUseKeyItem.m_msgNeedItemHeader = Replace(locks.m_intUseKeyItem.m_msgNeedItemHeader);

            string Replace(string input)
            {
                StringBuilder sb = new();
                if (!string.IsNullOrEmpty(def.Prefix))
                {
                    sb.Append(def.Prefix).AppendLine();
                }

                sb.Append(string.IsNullOrEmpty(def.TextToReplace) ? input : def.TextToReplace);

                if (!string.IsNullOrEmpty(def.Postfix))
                {
                    sb.AppendLine().Append(def.Postfix);
                }
                return sb.ToString();
            }

            EOSLogger.Debug($"SecDoorIntTextOverride: replaced text for {def}");
        }
    }
}
