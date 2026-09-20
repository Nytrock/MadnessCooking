using MadnessCooking.General;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Settings;

[CustomEditor(typeof(LocalizedText))]
public class LocalizedTextEditor : Editor {
    private SerializedProperty _isLogging;
    private string[] _options;

    public void OnEnable() {
        // this needs because for SOME REASON without selected locale I cannot get list of all tables
        LocalizationSettings.SelectedLocale = LocalizationSettings.ProjectLocale;

        var tables = LocalizationSettings.StringDatabase.GetAllTables();
        tables.WaitForCompletion();
        if (tables.Result is null)
            return;

        _options = tables.Result.Select(table => table.TableCollectionName).ToArray();
        _isLogging = serializedObject.FindProperty(nameof(_isLogging));
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        if (_options is null) {
            _isLogging.DrawPropertyField();
            serializedObject.ApplyModifiedProperties();
            return;
        }

        LocalizedText text = (LocalizedText)target;
        int nowOptionIndex = Mathf.Max(Array.IndexOf(_options, text.Table), 0);
        int selectedOptionIndex = EditorGUILayout.Popup("Table", nowOptionIndex, _options);

        _isLogging.DrawPropertyField();
        serializedObject.ApplyModifiedProperties();

        if (selectedOptionIndex == nowOptionIndex)
            return;
        text.SetTable(_options[selectedOptionIndex]);
    }
}
