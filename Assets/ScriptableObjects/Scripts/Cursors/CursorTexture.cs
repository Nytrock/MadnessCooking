using UnityEngine;

[CreateAssetMenu(menuName = nameof(Cursor))]
public class CursorTexture : ScriptableObject {
    [SerializeField] private Texture2D _mainTexture;
    [SerializeField] private CursorStateTexture[] _extraTextures;

    public Texture2D MainTexture => _mainTexture;

    public Texture2D GetCursor(CursorState state) {
        foreach (var texture in _extraTextures)
            if (texture.State == state)
                return texture.Texture;
        return _mainTexture;
    }
}
