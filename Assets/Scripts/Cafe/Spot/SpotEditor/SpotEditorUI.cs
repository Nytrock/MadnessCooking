using UnityEngine;

public class SpotEditorUI : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _buttonAdd;
    [SerializeField] private SpotEditorChooseButtons _buttonsContainer;
    [SerializeField] private SpotEditor _editor;

    private void Start()
    {
        ChangeUIState(false);
        _buttonAdd.SetActive(true);
        _buttonsContainer.gameObject.SetActive(false);

        _editor.EditorActivated += delegate { ChangeUIState(true); Move(_editor.SpotManager.GetLengthAllSpots()); };
        _editor.EditorDisabled += delegate { ChangeUIState(false); Move(-_editor.SpotManager.GetLengthAllSpots()); };

        _editor.SpotManager.SpotsPositionChanged += Move;
    }

    public void ChangeUIState(bool newState)
    {
        _container.SetActive(newState);
    }

    public void Move(float offset)
    {
        _container.transform.position += new Vector3(offset, 0, 0);
    }

    public void ChangeContainerState()
    {
        bool value = _buttonAdd.activeSelf;
        _buttonsContainer.gameObject.SetActive(value);
        _buttonAdd.SetActive(!value);

        if (value)
            _buttonsContainer.ChangeButtonNumber(_editor.SpotManager.GetFreeSpace());
    }

    public void SetPreview(int previewIndex) {
        _editor.SetPreviewIndex(previewIndex);
    }

    public void AddSpot()
    {
        ChangeContainerState();
        _editor.AddNewSpot();
    }
}
