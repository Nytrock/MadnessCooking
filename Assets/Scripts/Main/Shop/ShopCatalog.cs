using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCatalog : MonoBehaviour
{
    [SerializeField] private Transform _pagesContainer;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    private readonly List<ShopCatalogPage> _pages = new();
    private int _nowPage = 0;

    public Transform PagesContainer => _pagesContainer;

    public void AddPage(ShopCatalogPage page)
    {
        _pages.Add(page);
        page.ChangeState(false);
    }

    public void ActivateFirstPage()
    {
        _pages[0].ChangeState(true);
        UpdateButtons();
    }

    public void NextPage()
    {
        _pages[_nowPage].ChangeState(false);
        _nowPage++;
        _pages[_nowPage].ChangeState(true);
        UpdateButtons();
    }
    
    public void PreviousPage()
    {
        _pages[_nowPage].ChangeState(false);
        _nowPage--;
        _pages[_nowPage].ChangeState(true);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        _nextButton.interactable = _nowPage < _pages.Count - 1;
        _previousButton.interactable = _nowPage > 0;
        if (!_nextButton.interactable && !_previousButton.interactable) {
            _nextButton.gameObject.SetActive(false);
            _previousButton.gameObject.SetActive(false);
        }
    }

    public void RemovePanel(int removedItemIndex)
    {
        var startPageIndex = removedItemIndex / _pages[0].MaxItemCount;
        var panelIndex = removedItemIndex % _pages[0].MaxItemCount;
        _pages[startPageIndex].DestroyPanelByIndex(panelIndex);
        UpdatePages(startPageIndex);
    }

    private void UpdatePages(int startPageIndex)
    {
        for (int i = startPageIndex; i < _pages.Count - 1; i++) {
            var panel = _pages[i + 1].PopFirstPanel();
            _pages[i].AddPanel(panel);
        }

        if (_pages[^1].ItemCount == 0 && _pages.Count > 1)
            DestroyLastPage();
    }

    public void UpdatePanel(int updatedItemIndex, BuyableObject newItem)
    {
        var startPageIndex = updatedItemIndex / _pages[0].MaxItemCount;
        var panelIndex = updatedItemIndex % _pages[0].MaxItemCount;
        _pages[startPageIndex].UpdatePanelByIndex(panelIndex, newItem);
    }

    private void DestroyLastPage() {
        _pages[^1].Destroy();

        if (_nowPage == _pages.Count - 1)
            PreviousPage();
        _pages.RemoveAt(_pages.Count - 1);

        UpdateButtons();
    }
}
