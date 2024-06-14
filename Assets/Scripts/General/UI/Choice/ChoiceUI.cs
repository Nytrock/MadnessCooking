using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChoiceUI<TItem, TButton> : MonoBehaviour
    where TItem : BuyableItem where TButton : ChoiceButton<TItem> {

    [SerializeField] protected GameObject _UI;
    [SerializeField] protected ChoicePool<TItem, TButton> _choiceButtonPool;
    [SerializeField] protected Button _submitButton;
    protected readonly List<TButton> _choiceButtons = new();
    protected int _chosedIndex = -1;

    protected virtual void Start() {
        _UI.SetActive(false);
    }

    protected virtual void Activate() {
        _submitButton.interactable = false;
        _UI.SetActive(true);
    }

    public virtual void Disable() {
        if (_chosedIndex != -1)
            SetSelectedState(_chosedIndex);
        _chosedIndex = -1;
        _UI.SetActive(false);
    }

    protected abstract void GenerateChoiceButtons();
    protected abstract void SetSelectedState(int index);
    public abstract void SetChoice();
}
