using UnityEngine;

namespace MadnessCooking.General {
    public class MoneyMainUI : MoneyBaseUI {
        [SerializeField] private MoneyCell[] _moneyCells;
        public const int MONEY_CELLS_COUNT = 5;

        protected override void StartAnimation() {
            string count = CountConverter.ToMainMoneyCount(_nowCount);

            for (int i = 0; i < _moneyCells.Length; i++) {
                int countIndex = count.Length - i - 1;
                string symbol;
                if (countIndex >= 0)
                    symbol = count[countIndex].ToString();
                else
                    symbol = "";

                if (_moneyCells[i].NowSymbol != symbol)
                    _moneyCells[i].SetNewSymbol(symbol, _isCountAdded);
            }
        }
    }
}
