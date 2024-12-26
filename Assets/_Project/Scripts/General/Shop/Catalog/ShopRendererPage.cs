using System.Collections.Generic;
using UnityEngine;

public class ShopRendererPage : MonoBehaviour {
    [SerializeField] private BaseBuyPanel _buyPanelPrefab;
    [SerializeField] private ShopRendererPageUpper _upper;
    [SerializeField] private Transform _container;

    [SerializeField, Min(1)] private int _maxItemCount;

    [SerializeField] protected List<BaseBuyPanel> _buyPanels = new();

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
        if (_upper != null)
            _upper.ActivateUpper(_buyPanels.Count);

        panel.transform.SetParent(_container, false);
        _buyPanels.Add(panel);
    }

    private void RemovePanelByIndex(int index) {
        _buyPanels.RemoveAt(index);
        if (_upper != null)
            _upper.DisableUpper(_buyPanels.Count);
    }

    public void DestroyPanelByIndex(int index) {
        _buyPanels[index].Destroy();
        RemovePanelByIndex(index);
    }

    public BaseBuyPanel GetPanelByIndex(int index) {
        return _buyPanels[index];
    }

    public void UpdatePanelDataByIndex(int index, BuyPanelData updatedData) {
        _buyPanels[index].Setup(updatedData);
    }

    public void Destroy() {
        foreach (var panel in _buyPanels)
            panel.Destroy();

        Destroy(gameObject);
    }

    public void DisableAllUppers() {
        if (_upper == null)
            return;

        _upper.DisableAllUppers();
    }
}
