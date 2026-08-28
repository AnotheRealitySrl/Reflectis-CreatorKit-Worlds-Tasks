using Virtuademy.SDK.Core.Editor;
using UnityEditor;

namespace Virtuademy.CreatorKit.Worlds.Tasks.Editor
{
    [InitializeOnLoad]
    public class ScriptDefineSymbols
    {
        public const string TASKS_SCRIPT_DEFINE_SYMBOL = "REFLECTIS_CREATOR_KIT_WORLDS_TASKS";
        static ScriptDefineSymbols()
        {
            ScriptDefineSymbolsUtilities.AddScriptingDefineSymbolToAllBuildTargetGroups(TASKS_SCRIPT_DEFINE_SYMBOL);
        }
    }
}