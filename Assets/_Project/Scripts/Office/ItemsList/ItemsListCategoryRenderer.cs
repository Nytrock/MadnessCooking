using UnityEngine;

public class ItemsListCategoryRenderer : MonoBehaviour {
    [SerializeField] private LocalizedText _categoryTitle;
    [SerializeField] private string _categoryPrefix;
    [SerializeField] private ItemsListItemRenderer _itemRendererPrefab;
    [SerializeField] private RectTransform _itemsContainer;

    private string _categoryName;
    private int _allItemsCount;
    private int _nowItemsCount;

    public void Setup<TItem>(BuyableItemManager<TItem> itemManager, ItemsListItemDescription description)
        where TItem : BuyableItem {

        _categoryName = _categoryPrefix + typeof(TItem).Name;
        _allItemsCount = itemManager.AllItemsCount;
        _nowItemsCount = itemManager.AvailableItemsCount;
        UpdateCategoryTitle();

        foreach (var item in itemManager.GetAllItems()) {
            ItemsListItemRenderer itemRenderer = Instantiate(_itemRendererPrefab, _itemsContainer);
            itemRenderer.Setup(item, description);
            itemManager.ItemAdded += itemRenderer.CheckNewItem;
        }

        itemManager.ItemAdded += AddNewItem;
    }

    private void AddNewItem(BuyableItem item) {
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
