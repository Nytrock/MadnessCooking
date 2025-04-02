using System.Collections.Generic;
using UnityEngine;

public class ItemsListCategoryRenderer : MonoBehaviour {
    [SerializeField] private LocalizedText _categoryTitle;
    [SerializeField] private string _categoryPrefix;
    [SerializeField] private ItemsListItemRenderer _itemRendererPrefab;
    [SerializeField] private RectTransform _itemsContainer;

    private readonly List<BuyableItem> _allItems = new();
    private string _categoryName;
    private int _allItemsCount;
    private int _nowItemsCount;

    public void Setup<TItem>(BuyableItemManager<TItem> itemManager, ItemsListItemDescription description)
        where TItem : BuyableItem {

        _categoryName = _categoryPrefix + typeof(TItem).Name;
        _allItemsCount = itemManager.AllItemsCount;
        _nowItemsCount = 0;
        UpdateCategoryTitle();

        foreach (var item in itemManager.GetAllItems()) {
            ItemsListItemRenderer itemRenderer = Instantiate(_itemRendererPrefab, _itemsContainer);
            itemRenderer.Setup(item, description);
            itemManager.ItemAdded += itemRenderer.CheckNewItem;
            _allItems.Add(item);
        }

        itemManager.ItemAdded += AddNewItem;
    }

    public void SetupGrayman(GraymanManager graymanManager, ItemsListItemDescription description) {
        _categoryName = _categoryPrefix + graymanManager.GraymanName;
        _allItemsCount = 1;
        _nowItemsCount = 1;

        ItemsListItemRenderer itemRenderer = Instantiate(_itemRendererPrefab, _itemsContainer);
        itemRenderer.Setup(graymanManager.GraymanItem, description);
        itemRenderer.CheckNewItem(graymanManager.GraymanItem);
    }

    private void AddNewItem(BuyableItem item) {
        if (!_allItems.Contains(item))
            return;
        _nowItemsCount++;
    }

    private void OnEnable() {
        UpdateCategoryTitle();
    }

    private void UpdateCategoryTitle() {
        _categoryTitle.AddArguments("nowCount", _nowItemsCount.ToString());
        _categoryTitle.AddArguments("allCount", _allItemsCount.ToString());
        _categoryTitle.SetText(_categoryName);
    }
}
