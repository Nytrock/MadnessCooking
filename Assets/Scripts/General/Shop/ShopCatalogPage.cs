using System.Collections.Generic;
using UnityEngine;

public class ShopCatalogPage : MonoBehaviour {
    [SerializeField] private BaseBuyPanel _buyPanelPrefab;
    [SerializeField, Min(1)] private int _maxItemCount;
    protected readonly List<BaseBuyPanel> _buyPanels = new();

    public int MaxItemCount => _maxItemCount;
    public int ItemCount => _buyPanels.Count;

    public void GeneratePanel(BuyPanelData panelData) {
        BaseBuyPanel buyPanel = Instantiate(_buyPanelPrefab, transform);
        buyPanel.Setup(panelData);
        _buyPanels.Add(buyPanel);
    }

    public void ChangeState(bool newValue) {
        gameObject.SetActive(newValue);
    }

    public BaseBuyPanel PopFirstPanel() {
        BaseBuyPanel panel = _buyPanels[0];
        _buyPanels.RemoveAt(0);
        return panel;
    }

    public void AddPanel(BaseBuyPanel panel) {
        panel.transform.SetParent(transform);
        _buyPanels.Add(panel);
    }

    public void DestroyPanelByIndex(int index) {
        _buyPanels[index].Destroy();
        _buyPanels.RemoveAt(index);
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
