using UnityEngine;

[CreateAssetMenu(menuName = nameof(Cursor))]
public class CursorTexture : ScriptableObject {
    [SerializeField] private Texture2D _defaultTexture;
    [SerializeField] private CursorStateTexture[] _extraTextures;

    public Texture2D DefaultTexture => _defaultTexture;

    public Texture2D GetCursor(CursorState state) {
        foreach (var texture in _extraTextures)
            if (texture.State == state)
                return texture.Texture;
        return _defaultTexture;
    }
}
