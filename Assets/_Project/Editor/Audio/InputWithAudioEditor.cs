using MadnessCooking.General;
using TMPro.EditorUtilities;
using UnityEditor;

[CustomEditor(typeof(InputWithAudio))]
[CanEditMultipleObjects]
public class InputWithAudioEditor : TMP_InputFieldEditor {
    private SerializedProperty _audioSource;

    protected override void OnEnable() {
        base.OnEnable();
        _audioSource = serializedObject.FindProperty(nameof(_audioSource));
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        base.OnInspectorGUI();

        _audioSource.DrawPropertyField();
        serializedObject.ApplyModifiedProperties();
    }
}
