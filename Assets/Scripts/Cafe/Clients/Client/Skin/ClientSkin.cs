using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SortingGroup), typeof(Animator))]
public class ClientSkin : MonoBehaviour {
    [SerializeField, Min(0)] private int _foregroundSortingLayer;
    [SerializeField, Min(0)] private int _backgroundSortingLayer;
    [SerializeField] private ClientSkinPart[] _skinParts;

    private Transform _skin;
    private SortingGroup _sortingGroup;
    private Animator _animator;

    private void Awake() {
        _skin = transform;
        _sortingGroup = GetComponent<SortingGroup>();
        _animator = GetComponent<Animator>();
        _sortingGroup.sortingOrder = _foregroundSortingLayer;
        CheckSkinParts();
    }

    private void CheckSkinParts() {
        List<ClientSkinGroupPart> groupParts = new();
        List<ClientSkinPart> soloParts = new();

        foreach (var part in _skinParts) {
            if (part as ClientSkinGroupPart)
                groupParts.Add(part as ClientSkinGroupPart);
            else
                soloParts.Add(part);
        }

        foreach (var part in soloParts) {
            foreach (var groupPart in groupParts) {
                if (groupPart.IsPartInGroup(part)) {
                    throw new ArgumentException($"Skin part {part.name} in group " +
                        $"{groupPart.name}, so it cannot be solo in skins list");
                }
            }
        }
    }

    public void StartNewCycle(ClientData data) {
        if (data.SkinType == ClientSkinType.None)
            data.SetSkinType(GetRandomSkinType(data.Type));

        foreach (var skinPart in _skinParts)
            skinPart.SetSprite(data.SkinType);
        _sortingGroup.sortingOrder = _foregroundSortingLayer;
    }

    private ClientSkinType GetRandomSkinType(ClientType type) {
        if (type == ClientType.GrayMan) {
            return ClientSkinType.GrayMan;
        } else {
            int skinChance = Random.Range(0, 100);
            if (skinChance == 42) {
                int skinCount = Enum.GetNames(typeof(ClientSkinType)).Length;
                return (ClientSkinType)Random.Range(3, skinCount);
            } else {
                return ClientSkinType.Random;
            }
        }
    }

    public void RotateSkin(Direction direction) {
        if (direction == Direction.Right)
            _skin.localScale = new Vector2(-1, 1);
        else
            _skin.localScale = Vector2.one;
    }

    public void ChangeSortingLayer(bool isWalk) {
        if (isWalk)
            _sortingGroup.sortingOrder = _foregroundSortingLayer;
        else
            _sortingGroup.sortingOrder = _backgroundSortingLayer;
    }

    public void ChangeWalkState(bool isWalk) {
        _animator.SetBool("isWalk", isWalk);
    }

    public void ChangeEnable(bool value) {
        _animator.enabled = value;
    }
}