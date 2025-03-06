using System;
using System.Linq;
using UnityEditor;
using UnityEngine.Localization.Settings;

public class LocalizedTextEditor : Editor {
    public override void OnInspectorGUI() {
        LocalizedText text = (LocalizedText)target;

        var tables = LocalizationSettings.StringDatabase.GetAllTables();
        tables.WaitForCompletion();
        string[] options = tables.Result.Select(table => table.TableCollectionName).ToArray();
        int selectedOptionIndex = Array.IndexOf(options, text.Table);

        EditorGUILayout.Popup(selectedOptionIndex, options);
    }
}
