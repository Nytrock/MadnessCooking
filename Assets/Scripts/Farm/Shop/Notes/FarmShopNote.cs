using System;
using UnityEngine;

[Serializable]
public class FarmShopNote {
    [SerializeField] private UpgradeType _type;
    [SerializeField] private string _note;

    public UpgradeType Type => _type;
    public string Note => _note;
}
