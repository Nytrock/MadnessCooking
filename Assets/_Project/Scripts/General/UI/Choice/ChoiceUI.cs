using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChoiceUI<TItem, TButton> : MonoBehaviour
    where TItem : BuyableItem where TButton : ChoiceButton<TItem> {

    [SerializeField] protected GameObject _UI;
    [SerializeField] protected ChoicePool<TItem, TButton> _choiceButtonPool;
    [SerializeField] protected Button _submitButton;

    protected readonly List<TButton> _choiceButtons = new();
    protected TButton _choosedButton;

    protected virtual void Awake() {
        _submitButton.onClick.AddListener(SubmitChoice);
    }

    protected virtual void Start() {
        Disable();
    }

    protected virtual void Activate() {
        _submitButton.interactable = false;
        _UI.SetActive(true);
    }

    public virtual void Disable() {
        if (_choosedButton != null)
            _choosedButton.ChangeChoosedState();
        _choosedButton = null;
        _UI.SetActive(false);
    }

    public virtual void SelectButton(TButton button) {
        if (_choosedButton != null)
            _choosedButton.ChangeChoosedState();

        if (_choosedButton == button) {
            _submitButton.interactable = false;
            _choosedButton = null;
            return;
        }

        _choosedButton = button;
        _submitButton.interactable = true;
        _choosedButton.ChangeChoosedState();
    }

    protected void DestoyOldButtons() {
        foreach (var button in _choiceButtons)
            _choiceButtonPool.PutObject(button);
        _choiceButtons.Clear();
    }

    protected virtual void GenerateChoiceButtons() {
        foreach (var item in GetItems())
            GenerateChoiceButton(item);
    }

    protected virtual TButton GenerateChoiceButton(TItem item) {
        TButton choiceButton = _choiceButtonPool.GetObject();
        choiceButton.Setup(item, delegate { SelectButton(choiceButton); });
        _choiceButtons.Add(choiceButton);
        return choiceButton;
    }

    protected abstract IEnumerable<TItem> GetItems();
    public abstract void SubmitChoice();
}
