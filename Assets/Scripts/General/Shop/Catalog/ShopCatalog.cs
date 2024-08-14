using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCatalog : MonoBehaviour {
    [SerializeField] private Transform _pagesContainer;
    [SerializeField] private ShopCatalogPage _pagePrefab;
    [SerializeField] private GameObject _emptyMessage;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    private readonly List<ShopCatalogPage> _pages = new();
    private int _nowPage = 0;

    public void GeneratePanel(BuyPanelData panelData) {
        if (_pages.Count == 0 || _pages[^1].ItemCount == _pages[^1].MaxItemCount) {
            GeneratePage();
            UpdateButtons();
        }

        _pages[^1].GeneratePanel(panelData);
    }

    public void GeneratePage() {
        ShopCatalogPage page = Instantiate(_pagePrefab, _pagesContainer);
        _pages.Add(page);
        page.ChangeState(false);
    }

    public void ActivateFirstPage() {
        _pages[_nowPage].ChangeState(false);
        _nowPage = 0;
        _pages[_nowPage].ChangeState(true);
        UpdateButtons();
    }

    public void NextPage() {
        _pages[_nowPage].ChangeState(false);
        _nowPage++;
        _pages[_nowPage].ChangeState(true);
        UpdateButtons();
    }

    public void PreviousPage() {
        _pages[_nowPage].ChangeState(false);
        _nowPage--;
        _pages[_nowPage].ChangeState(true);
        UpdateButtons();
    }

    private void UpdateButtons() {
        _nextButton.gameObject.SetActive(_nowPage < _pages.Count - 1);
        _previousButton.gameObject.SetActive(_nowPage > 0);
    }

    private void CalculateIndexes(int index, out int startPageIndex, out int panelIndex) {
        startPageIndex = index / _pages[0].MaxItemCount;
        panelIndex = index % _pages[0].MaxItemCount;
    }

    public void RemovePanel(int removedItemIndex) {
        CalculateIndexes(removedItemIndex, out int startPageIndex, out int panelIndex);
        _pages[startPageIndex].DestroyPanelByIndex(panelIndex);
        UpdatePages(startPageIndex);
        UpdateEmptyState();
    }

    private void UpdatePages(int startPageIndex) {
        for (int i = startPageIndex; i < _pages.Count - 1; i++) {
            BaseBuyPanel panel = _pages[i + 1].PopFirstPanel();
            _pages[i].AddPanel(panel);
        }

        if (_pages[^1].ItemCount == 0 && _pages.Count > 1)
            DestroyLastPage();
    }

    public void ReplacePanelItem(int updatedItemIndex, BuyPanelData newData) {
        CalculateIndexes(updatedItemIndex, out int startPageIndex, out int panelIndex);
        _pages[startPageIndex].UpdatePanelDataByIndex(panelIndex, newData);
    }

    public void UpdatePanel(int updatedItemIndex, BuyPanelData panelData) {
        if (updatedItemIndex >= _pages.Count * _pagePrefab.MaxItemCount)
            return;

        CalculateIndexes(updatedItemIndex, out int startPageIndex, out int panelIndex);
        _pages[startPageIndex].UpdatePanelDataByIndex(panelIndex, panelData);
    }

    private void DestroyLastPage() {
        _pages[^1].Destroy();

        if (_nowPage == _pages.Count - 1)
            PreviousPage();
        _pages.RemoveAt(_pages.Count - 1);

        UpdateButtons();
    }

    public void UpdateEmptyState() {
        if (_emptyMessage == null)
            return;

        bool isEmpty = _pages.Count == 1 && _pages[_nowPage].ItemCount == 0;
        _emptyMessage.SetActive(isEmpty);
    }
}
