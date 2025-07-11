using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyHighlighter
{
    private static string _prefix = "--";
    private static Color _backgroundColor = new(0.9f, 0.1f, 0.1f, 0.5f);
    private static Color _textColor = Color.white;

    static HierarchyHighlighter()
    {
        EditorApplication.hierarchyWindowItemOnGUI += (id, rect) =>
        {
            var go = EditorUtility.InstanceIDToObject(id) as GameObject;
            if (go && go.CompareTag("EditorOnly") && go.name.StartsWith(_prefix))
            {
                EditorGUI.DrawRect(rect, _backgroundColor);
                GUI.Label(rect, go.name.ToUpper(), new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold,
                    normal = new GUIStyleState { textColor = _textColor }
                });
            }
        };
    }

    [SettingsProvider]
    public static SettingsProvider CreateCustomSettings()
    {
        var provider = new SettingsProvider("Project/Hierarchy Visuals", SettingsScope.Project)
        {
            label = "Hierarchy Visuals",
            guiHandler = (searchContext) =>
            {
                GUILayout.Label("EditorOnly Tag + Prefix Highlighter", EditorStyles.boldLabel);

                _prefix = EditorGUILayout.TextField("Starts With", _prefix);
                _backgroundColor = EditorGUILayout.ColorField("Background Color", _backgroundColor);
                _textColor = EditorGUILayout.ColorField("Text Color", _textColor);
            }
        };

        return provider;
    }
}
