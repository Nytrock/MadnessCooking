using System.Collections.Generic;
using UnityEngine;

public class ShopCatalogPage : MonoBehaviour
{
    [SerializeField] private BaseBuyPanel _buyPanelPrefab;
    [SerializeField, Min(1)] private int _maxItemCount;
    private readonly List<BaseBuyPanel> _buyPanels = new();
    private BaseShop _shop;

    public int MaxItemCount => _maxItemCount;
    public int ItemCount => _buyPanels.Count;

    public void SetShop(BaseShop shop)
    {
        _shop = shop;
    }

    public void GeneratePanel(BuyableObject item)
    {
        var buyPanel = Instantiate(_buyPanelPrefab, transform);
        buyPanel.Setup(item, _shop);
        _buyPanels.Add(buyPanel);
    }

    public void ChangeState(bool newValue)
    {
        gameObject.SetActive(newValue);
    }
    public BaseBuyPanel PopFirstPanel()
    {
        var panel = _buyPanels[0];
        _buyPanels.RemoveAt(0);
        return panel;
    }

    public void AddPanel(BaseBuyPanel panel)
    {
        panel.transform.SetParent(transform);
        _buyPanels.Add(panel);
    }

    public void DestroyPanelByIndex(int index)
    {
        _buyPanels[index].Destroy();
        _buyPanels.RemoveAt(index);
    }

    public void UpdatePanelByIndex(int index, BuyableObject item)
    {
        _buyPanels[index].Setup(item, _shop);
    }

    public void Destroy()
    {
        foreach (var panel in _buyPanels)
            panel.Destroy();

        Destroy(gameObject);
    }
}
