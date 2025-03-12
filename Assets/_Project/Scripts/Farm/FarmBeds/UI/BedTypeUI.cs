using UnityEngine;
using UnityEngine.UI;

public class BedTypeUI : MonoBehaviour {
    [SerializeField] private BedType _bedType;
    [SerializeField] private GameObject _UI;
    [SerializeField] private ItemInfoRendererWithCount _renderer;

    [Header("Side buttons")]
    [SerializeField] private BedTypeUIPestsButton _pestsButton;
    [SerializeField] private Button _waterButton;
    [SerializeField] private Button _fertilizeButton;

    private FarmBedData _bedData;
    private FarmData _data;

    public BedType BedType => _bedType;

    private void Start() {
        _UI.SetActive(false);
    }

    public void ChangeState() {
        _UI.SetActive(!_UI.activeSelf);
    }

    public void ChangeState(bool newValue) {
        _UI.SetActive(newValue);
    }

    public void UpdateInfo(FarmBed farmBed) {
        _renderer.SetItemInfo(farmBed.Data.PlantedIngredient);
        _renderer.SetCount(farmBed.Data.Count);
    }

    public void CheckWater() {
        if (_waterButton == null) return;

        _waterButton.interactable = _data.FarmWell.ReadyCount > 0
            && !_bedData.WaterBoost.IsEternal;
    }

    public void CheckFertilize() {
        if (_fertilizeButton == null) return;

        _fertilizeButton.interactable = _data.Puncher.FertilizerCount > 0 &&
            !_bedData.FertilizeBoost.IsEternal;
    }

    public void UpdateCount() {
        _renderer.SetCount(_bedData.Count);
    }

    public void UpdateSideButtons(FarmBed farmBed) {
        _bedData = farmBed.Data;
        _pestsButton.UpdateState(_bedData.PestsGenerator);

        CheckWater();
        CheckFertilize();
    }

    public void Bind(FarmData data) {
        _data = data;
    }
}
