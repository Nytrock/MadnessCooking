using System.Collections.Generic;
using UnityEngine;

public class ShopCatalogPage : MonoBehaviour {
    [SerializeField] private BaseBuyPanel _buyPanelPrefab;
    [SerializeField] private ShopCatalogPageUpper _upper;
    [SerializeField] private Transform _container;

    [SerializeField, Min(1)] private int _maxItemCount;

    protected readonly List<BaseBuyPanel> _buyPanels = new();

    public int MaxItemCount => _maxItemCount;
    public int ItemCount => _buyPanels.Count;

    public void GeneratePanel(BuyPanelData panelData) {
        BaseBuyPanel buyPanel = Instantiate(_buyPanelPrefab);
        buyPanel.Setup(panelData);
        AddPanel(buyPanel);
    }

    public void ChangeState(bool newValue) {
        gameObject.SetActive(newValue);
    }

    public BaseBuyPanel PopFirstPanel() {
        BaseBuyPanel panel = _buyPanels[0];
        RemovePanelByIndex(0);
        return panel;
    }

    public void AddPanel(BaseBuyPanel panel) {
        if (_upper)
            _upper.ActivateUpper(_buyPanels.Count);
        panel.transform.SetParent(_container);
        _buyPanels.Add(panel);
    }

    private void RemovePanelByIndex(int index) {
        _buyPanels.RemoveAt(index);
        if (_upper)
            _upper.DisableUpper(_buyPanels.Count);
    }

    public void DestroyPanelByIndex(int index) {
        _buyPanels[index].Destroy();
        RemovePanelByIndex(index);
    }

    public void UpdatePanelDataByIndex(int index, BuyPanelData updatedData) {
        _buyPanels[index].Setup(updatedData);
    }

    public void Destroy() {
        foreach (var panel in _buyPanels)
            panel.Destroy();

        Destroy(gameObject);
    }
}
