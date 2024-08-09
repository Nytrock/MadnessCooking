using UnityEngine;

public class BarnStorageUI : MonoBehaviour, IActivable {
    [SerializeField] private GameObject _panel;
    [SerializeField] private BarnStorage _barnStorage;
    [SerializeField] private ItemInfoRendererWithCount _milkRenderer;
    [SerializeField] private ItemInfoRendererWithCount _flourRenderer;

    private void Awake() {
        _barnStorage.CountsUpdated += UpdateCounts;
    }

    private void UpdateCounts(int milkCount, int flourCount) {
        _milkRenderer.SetCount(milkCount);
        _flourRenderer.SetCount(flourCount);
    }

    private void Start() {
        ChangeState(false);
        _milkRenderer.SetItemInfo(ConstIngredients.Instance.Milk);
        _flourRenderer.SetItemInfo(ConstIngredients.Instance.Flour);
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }
}
