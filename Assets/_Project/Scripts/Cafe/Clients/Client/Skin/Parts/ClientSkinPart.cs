using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class ClientSkinPart : MonoBehaviour {
    [SerializeField] private Sprite[] _randomSprites;
    [SerializeField] protected SpecialClientSprite[] _specialSprites;
    protected SpriteRenderer _renderer;

    public int RandomSpritesCount => _randomSprites.Length;

    protected virtual void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(ClientSkinType skinType) {
        if (skinType != ClientSkinType.Random) {
            SetSpecialSprite(skinType);
            return;
        }

        int spriteIndex = Random.Range(0, RandomSpritesCount);
        SetRandomSprite(spriteIndex);
    }

    public virtual void SetRandomSprite(int spriteIndex) {
        if (RandomSpritesCount == 0) {
            _renderer.sprite = null;
            return;
        }

        _renderer.sprite = _randomSprites[spriteIndex];
    }

    public virtual void SetSpecialSprite(ClientSkinType skinType) {
        foreach (var specialSprite in _specialSprites) {
            if (specialSprite.SkinType == skinType) {
                _renderer.sprite = specialSprite.Sprite;
                return;
            }
        }

        _renderer.sprite = null;
    }

    public virtual bool CheckRelationToGroup(ClientSkinGroupPart groupPart) {
        if (groupPart.RandomSpritesCount != RandomSpritesCount && RandomSpritesCount != 0)
            return false;
        return true;
    }

    public IEnumerable<SpecialClientSprite> GetSpecialSprites() {
        foreach (var specialSprite in _specialSprites)
            yield return specialSprite;
    }
}