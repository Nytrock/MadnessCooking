using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCatalog : MonoBehaviour
{
    [SerializeField] private Transform _pagesContainer;
    [SerializeField] private ShopCatalogPage _pagePrefab;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    private BaseShop _shop;

    private readonly List<ShopCatalogPage> _pages = new();
    private int _nowPage = 0;

    public Transform PagesContainer => _pagesContainer;

    public void SetShop(BaseShop shop)
    {
        if (_pagePrefab.ItemType != shop.Type) {
            Debug.LogError("Buyable object type of shop and buy panel don't match");
            return;
        }

        _shop = shop;
    }

    public void GeneratePanel(BuyableObject item)
    {
        if (_pages.Count == 0 || _pages[^1].ItemCount == _pages[^1].MaxItemCount) {
            GeneratePage();
            UpdateButtons();
        }
        _pages[^1].GeneratePanel(item);
    }

    public void GeneratePage()
    {
        var page = Instantiate(_pagePrefab, _pagesContainer);
        page.SetShop(_shop);
        _pages.Add(page);
        page.ChangeState(false);
    }

    public void ActivateFirstPage()
    {
        _pages[_nowPage].ChangeState(false);
        _nowPage = 0;
        _pages[_nowPage].ChangeState(true);
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
        _nextButton.gameObject.SetActive(_pages.Count != 1);
        _previousButton.gameObject.SetActive(_pages.Count != 1);
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
