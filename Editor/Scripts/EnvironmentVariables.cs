using Reflectis.SDK.Core.Editor;
using UnityEditor;

[InitializeOnLoad]
public class EnvironmentVariables
{
    public const string TASKS_ENV_VARIABLE = "REFLECTIS_CREATOR_KIT_WORLDS_TASKS";
    static EnvironmentVariables()
    {
        ScriptDefineSymbolsUtilities.AddScriptingDefineSymbolToAllBuildTargetGroups(TASKS_ENV_VARIABLE);
    }
}
