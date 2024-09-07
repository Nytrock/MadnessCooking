using UnityEngine;

public class InternetShopPage : InternetPage {
    [SerializeField] private BaseShop _shop;

    public override void ChangeState(bool newValue) {
        base.ChangeState(newValue);
        _shop.ChangeShopState(newValue);
    }
}
