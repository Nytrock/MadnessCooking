using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoneyCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _nowText;
    [SerializeField] private TextMeshProUGUI[] _newTexts;
    private Animator _animator;
    private string _newSymbol;

    public string NowSymbol => _nowText.text;

    private void Awake() {
        _nowText.text = "";
        _animator = GetComponent<Animator>();
    }

    public void SetNewSymbol(string symbol, bool isAdded) {
        _newSymbol = symbol;
        foreach (var newText in _newTexts)
            newText.text = symbol;

        if (!gameObject.activeInHierarchy) {
            UpdateSymbol();
            return;
        }

        if (isAdded)
            _animator.SetTrigger("isAdded");
        else
            _animator.SetTrigger("isSubtracted");
    }

    public void UpdateSymbol() {
        _nowText.text = _newSymbol;
    }
}
