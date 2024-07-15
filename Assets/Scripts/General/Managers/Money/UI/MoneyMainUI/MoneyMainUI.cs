using System.Collections;
using UnityEngine;

public class MoneyMainUI : MoneyBaseUI {
    [SerializeField] private MoneyCell[] _moneyCells = new MoneyCell[MONEY_CELLS_COUNT];
    [SerializeField, Min(0)] private float _cellsCheckDelay;
    public const int MONEY_CELLS_COUNT = 5;

    protected override void StartAnimation() {
        StartCoroutine(CheckMoneyCells());
    }

    private IEnumerator CheckMoneyCells() {
        for (int i = 0; i < _moneyCells.Length; i++) {
            int countIndex = _countConverted.Length - i - 1;
            string symbol;
            if (countIndex >= 0)
                symbol = _countConverted[countIndex].ToString();
            else
                symbol = "";

            if (_moneyCells[i].NowSymbol != symbol) {
                _moneyCells[i].SetNewSymbol(symbol, _isCountAdded);
                yield return new WaitForSeconds(_cellsCheckDelay);
            }
        }
    }
}
