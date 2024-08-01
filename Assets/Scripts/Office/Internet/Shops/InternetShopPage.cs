using UnityEngine;

[RequireComponent(typeof(BaseShop))]
public class InternetShopPage : InternetPage {
    private BaseShop _shop;

    private void Awake() {
        _shop = GetComponent<BaseShop>();
    }

    public override void ChangeState(bool newValue) {
        _shop.ChangeShopState(newValue);
    }
}
