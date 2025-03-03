using System.Collections.Generic;
using UnityEngine;

public class ShopRendererPage : MonoBehaviour {
    [SerializeField] private BaseBuyPanel _buyPanelPrefab;
    [SerializeField] private HoverTextPanel _sideInfoHoverPanel;
    [SerializeField] private Transform _container;
    [SerializeField] private int _maxItemCount;

    protected readonly List<BaseBuyPanel> _buyPanels = new();

    public int MaxItemCount {
        get {
            if (_maxItemCount == -1)
                return int.MaxValue;
            return _maxItemCount;
        }
    }
    public int ItemCount => _buyPanels.Count;

    public void GeneratePanel(BuyPanelData panelData) {
        BaseBuyPanel buyPanel = Instantiate(_buyPanelPrefab);
        buyPanel.SetSideInfoHoverPanel(_sideInfoHoverPanel);
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
        panel.transform.SetParent(_container, false);
        _buyPanels.Add(panel);
    }

    private void RemovePanelByIndex(int index) {
        _buyPanels.RemoveAt(index);
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
}
