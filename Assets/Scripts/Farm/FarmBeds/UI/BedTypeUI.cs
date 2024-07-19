using UnityEngine;
using UnityEngine.UI;

public class BedTypeUI : MonoBehaviour {
    [SerializeField] private BedType _bedType;
    [SerializeField] private GameObject _UI;
    [SerializeField] private ItemInfoRendererWithCount _renderer;

    [Header("Side buttons")]
    [SerializeField] private bool _isSideButtonsWork;
    [SerializeField] private Button _pestsButton;
    [SerializeField] private Button _waterButton;
    [SerializeField] private Button _fertilizeButton;

    private FarmBedData _bedData;
    private FarmData _data;

    public BedType BedType => _bedType;

    private void Start() {
        _UI.SetActive(false);
    }

    public void ChangeMode() {
        _UI.SetActive(!_UI.activeSelf);
    }

    public void ChangeMode(bool newValue) {
        _UI.SetActive(newValue);
    }

    public void UpdateInfo(FarmBed farmBed) {
        _renderer.SetItemInfo(farmBed.BedData.PlantedIngredient);
        _renderer.SetCount(farmBed.BedData.Count);
    }

    public void CheckWater() {
        _waterButton.interactable = _data.FarmWell.ReadyCount > 0
            && !_bedData.WaterBoost.IsEternal;
    }

    public void CheckFertilize() {
        _fertilizeButton.interactable = _data.Puncher.FertilizerCount > 0 &&
            !_bedData.FertilizeBoost.IsEternal;
    }

    public void UpdateCount() {
        _renderer.SetCount(_bedData.Count);
    }

    public void UpdateSideButtons(FarmBed farmBed) {
        _bedData = farmBed.BedData;
        _pestsButton.interactable = !_bedData.PestsGenerator.IsPestsRemoved;
        if (!_isSideButtonsWork)
            return;

        CheckWater();
        CheckFertilize();
    }

    public void Bind(FarmData data) {
        _data = data;
    }
}
