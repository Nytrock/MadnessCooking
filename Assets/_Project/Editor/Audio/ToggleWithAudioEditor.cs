using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ToggleWithAudio))]
[CanEditMultipleObjects]
public class ToggleWithAudioEditor : ToggleEditor {
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
