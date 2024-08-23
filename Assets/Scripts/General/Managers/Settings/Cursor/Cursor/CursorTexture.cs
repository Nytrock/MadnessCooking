using UnityEngine;

[CreateAssetMenu(menuName = nameof(Cursor))]
public class CursorTexture : ShowableScriptableObject {
    [SerializeField] private Texture2D _defaultTexture;
    [SerializeField] private CursorStateTexture[] _extraTextures;

    public override Sprite Icon => Sprite.Create(_defaultTexture, new Rect(0, 0, _defaultTexture.width, _defaultTexture.height), Vector2.zero);

    public Texture2D GetCursor(CursorState state) {
        foreach (var texture in _extraTextures)
            if (texture.State == state)
                return texture.Texture;
        return _defaultTexture;
    }
}
