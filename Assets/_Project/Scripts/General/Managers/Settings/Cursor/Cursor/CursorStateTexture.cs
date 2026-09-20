using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class CursorStateTexture {
        [SerializeField] private CursorState _state;
        [SerializeField] private Texture2D _texture;

        public CursorState State => _state;
        public Texture2D Texture => _texture;
    }
}
