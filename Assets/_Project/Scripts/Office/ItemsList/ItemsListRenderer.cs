using UnityEngine;

public class ItemsListRenderer : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private ItemsListCategoryRenderer _categoryRendererPrefab;
    [SerializeField] private RectTransform _categoriesContainer;
    [SerializeField] ItemsListItemDescription _description;

    private void Awake() {
        ChangeState(false);
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);

        if (!newState)
            _description.ChangeState(false);
    }

    public void CreateCategory<TItem>(BuyableItemManager<TItem> itemManager)
        where TItem : BuyableItem {
        ItemsListCategoryRenderer categoryRenderer = Instantiate(_categoryRendererPrefab, _categoriesContainer);
        categoryRenderer.Setup(itemManager, _description);
        _categoriesContainer.ForceUpdateRect();
    }

    public void CreateGraymanCategory(GraymanManager graymanManager) {
        ItemsListCategoryRenderer categoryRenderer = Instantiate(_categoryRendererPrefab, _categoriesContainer);
        categoryRenderer.SetupGrayman(graymanManager, _description);
        _categoriesContainer.ForceUpdateRect();
    }
}
