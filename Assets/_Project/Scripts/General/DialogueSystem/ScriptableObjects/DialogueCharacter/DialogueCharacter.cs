using UnityEngine;

[CreateAssetMenu(menuName = nameof(DialogueCharacter))]
public class DialogueCharacter : ExtendedScriptableObject {
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private DialogueCharacterEmotionSprite[] _emotionSprites;
    [SerializeField] private PitchableAudioInfo _voiceInfo;

    private string _characterName;

    public PitchableAudioInfo VoiceInfo => _voiceInfo;
    public string Name => _characterName;
    public override Sprite Icon => _defaultSprite;

    public override void Initialize() {
        _characterName = $"{nameof(DialogueCharacter)}.{name}";
    }

    public Sprite GetEmotionSprite(DialogueCharacterEmotion emotion) {
        foreach (var emotionSprite in _emotionSprites)
            if (emotionSprite.Emotion == emotion)
                return emotionSprite.Sprite;
        return _defaultSprite;
    }
}
