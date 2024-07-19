using System;
using UnityEngine;

[Serializable]
public class FarmShopNote : UpgradeTypeImage {
    [SerializeField] private string _note;

    public string Note => _note;
}
