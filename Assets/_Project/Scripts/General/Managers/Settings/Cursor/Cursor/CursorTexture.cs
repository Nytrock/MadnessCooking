using UnityEngine;

namespace MadnessCooking.General {
    [CreateAssetMenu(menuName = nameof(Cursor))]
    public class CursorTexture : ExtendedScriptableObject {
        [SerializeField] private Texture2D _defaultTexture;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private CursorStateTexture[] _extraTextures;

        public override Sprite Icon => Sprite.Create(_defaultTexture, new Rect(0, 0, _defaultTexture.width, _defaultTexture.height), Vector2.zero);
        public Vector2 Offset => _offset;

        public Texture2D GetCursor(CursorState state) {
            foreach (var texture in _extraTextures)
                if (texture.State == state)
                    return texture.Texture;
            return _defaultTexture;
        }
    }
}
