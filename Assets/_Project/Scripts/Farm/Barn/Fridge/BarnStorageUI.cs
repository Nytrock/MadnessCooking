using UnityEngine;

public class BarnStorageUI : MonoBehaviour, IActivable {
    [SerializeField] private GameObject _panel;
    [SerializeField] private BarnStorage _barnStorage;
    [SerializeField] private ItemInfoRendererWithCount _milkRenderer;
    [SerializeField] private ItemInfoRendererWithCount _flourRenderer;

    private void Awake() {
        _barnStorage.MilkCountUpdated += UpdateMilkCount;
        _barnStorage.FlourCountUpdated += UpdateFlourCount;
    }

    private void UpdateMilkCount(int milkCount) {
        _milkRenderer.SetCount(milkCount);
    }

    private void UpdateFlourCount(int flourCount) {
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
