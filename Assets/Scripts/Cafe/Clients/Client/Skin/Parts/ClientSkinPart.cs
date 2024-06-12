using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class ClientSkinPart : MonoBehaviour {
    [SerializeField] private Sprite[] _randomSprites;
    [SerializeField] private SpecialClientSprite[] _specialSprites;
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

        int spriteIndex = Random.Range(0, _randomSprites.Length);
        SetRandomSprite(spriteIndex);
    }

    public virtual void SetRandomSprite(int spriteIndex) {
        _renderer.sprite = _randomSprites[spriteIndex];
    }

    public virtual void SetSpecialSprite(ClientSkinType skinType) {
        foreach (var specialSprite in _specialSprites) {
            if (specialSprite.SkinType == skinType) {
                _renderer.sprite = specialSprite.Sprite;
                return;
            }
        }
        throw new ArgumentNullException($"No sprite for client {skinType}");
    }

    public virtual bool CheckRelationToGroup(ClientSkinGroupPart groupPart) {
        if (groupPart.RandomSpritesCount != RandomSpritesCount)
            return false;

        foreach (var groupSpecialSprite in groupPart.GetSpecialSprites()) {
            if (!_specialSprites.Contains(groupSpecialSprite)) {
                return false;
            }
        }

        return true;
    }

    public IEnumerable<SpecialClientSprite> GetSpecialSprites() {
        foreach (var specialSprite in _specialSprites)
            yield return specialSprite;
        yield break;
    }
}