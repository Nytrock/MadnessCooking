using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChoiceUI<T, K> : MonoBehaviour where K: ChoiceButton<T>
{
    [SerializeField] protected GameObject _UI;
    [SerializeField] protected CameraManager _cameraManager;
    [SerializeField] protected ChoicePool<T, K> _choiceButtonPool;
    [SerializeField] protected Button _submitButton;
    [SerializeField] protected List<K> _choiceButtons;
    protected int _chosedIndex = -1;

    protected virtual void Start()
    {
        _UI.SetActive(false);
    }

    protected virtual void Activate()
    {
        _submitButton.interactable = false;
        _cameraManager.ChangeWorkMode(false);
        _UI.SetActive(true);
    }

    public virtual void Disable()
    {
        if (_chosedIndex != -1)
            SetSelectedState(_chosedIndex);
        _chosedIndex = -1;
        _cameraManager.ChangeWorkMode(true);
        _UI.SetActive(false);
    }

    protected abstract void GenerateChoiceButtons();
    protected abstract void SetSelectedState(int index);
    public abstract void SetChoice();
}
