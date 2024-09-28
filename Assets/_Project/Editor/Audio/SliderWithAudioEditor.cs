using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(SliderWithAudio))]
[CanEditMultipleObjects]
public class SliderWithAudioEditor : SliderEditor {
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
