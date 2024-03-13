using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(BaseUpgrade))]
public class BaseUpgrade : BuyableObject
{
    public new const string AssetMenuName = nameof(BuyableObject) + "/Upgrades/";
}
