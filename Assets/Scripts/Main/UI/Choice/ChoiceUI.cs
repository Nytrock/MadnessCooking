using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChoiceUI<T> : MonoBehaviour
{
    [SerializeField] protected GameObject _UI;
    [SerializeField] protected CameraManager _cameraManager;
    [SerializeField] protected T _choiceButtonPrefab;
    [SerializeField] protected Transform _choiceButtonsContainer;
    [SerializeField] protected Button _submitButton;
    protected List<T> _choiceButtons;
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
            SetSelectedState(_chosedIndex);

        _submitButton.interactable = index != _chosedIndex;
        if (index == _chosedIndex) {
            _chosedIndex = -1;
            return;
        }

        _chosedIndex = index;
        SetSelectedState(_chosedIndex);
    }

    public virtual void SetChoice()
    {
        Disable();
    }

    public void Disable()
    {
        if (_chosedIndex != -1)
            SetSelectedState(_chosedIndex);
        _chosedIndex = -1;
        _submitButton.interactable = false;
        _cameraManager.ChangeWorkMode(true);
        _UI.SetActive(false);
    }

    protected abstract void GenerateChoiceButtons();
    protected abstract void SetSelectedState(int index);
}
