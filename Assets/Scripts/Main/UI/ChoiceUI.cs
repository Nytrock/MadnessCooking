using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChoiceUI : MonoBehaviour
{
    [SerializeField] protected GameObject _UI;
    [SerializeField] protected CameraManager _cameraManager;
    [SerializeField] protected ChoiceButton _choiceButtonPrefab;
    [SerializeField] protected Transform _choiceButtonsContainer;
    [SerializeField] protected Button _submitButton;
    protected List<ChoiceButton> _choiceButtons;
    protected int _chosedIndex = -1;

    protected virtual void Start()
    {
        _UI.SetActive(false);
    }

    protected void Activate()
    {
        _cameraManager.ChangeWorkMode(false);
        _UI.SetActive(true);
    }

    public void Choice(int index)
    {
        if (_chosedIndex != -1)
            _choiceButtons[_chosedIndex].ChangeSelectedState();

        _submitButton.interactable = index != _chosedIndex;
        if (index == _chosedIndex) {
            _chosedIndex = -1;
            return;
        }

        _chosedIndex = index;
        _choiceButtons[_chosedIndex].ChangeSelectedState();
    }

    public virtual void SetChoice()
    {
        Disable();
    }

    public void Disable()
    {
        if (_chosedIndex != -1)
            _choiceButtons[_chosedIndex].ChangeSelectedState();
        _chosedIndex = -1;
        _submitButton.interactable = false;
        _cameraManager.ChangeWorkMode(true);
        _UI.SetActive(false);
    }

    protected abstract void GenerateChoiceButtons();
}
