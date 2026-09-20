using UnityEngine;

namespace MadnessCooking.General {
    public static class CountConverter {
        private readonly static string[] _prefixes = { "K", "M" };
        private readonly static string _overflowMessage = "WHAT";

        public static string ToCount(int count) {
            int index = -1;
            float resCount = count;
            while (index < _prefixes.Length) {
                if (resCount < 1000f)
                    break;
                resCount /= 1000f;
                index++;
            }

            if (index == -1)
                return count.ToString();
            else if (index >= _prefixes.Length)
                return _overflowMessage;

            string result = resCount.ToString("N2");
            return $"{result}{_prefixes[index]}";
        }

        public static string ToMainMoneyCount(int count) {
            int index = -1;
            float resCount = count;
            while (index < _prefixes.Length) {
                if (resCount < 1000f)
                    break;
                resCount /= 1000f;
                index++;
            }

            if (index == -1)
                return count.ToString();
            else if (index == _prefixes.Length)
                return _overflowMessage;

            string resCountFormatted = resCount.ToString("F2");
            string resCountWithPrefix = $"{resCountFormatted}{_prefixes[index]}";

            if (resCountWithPrefix.Length <= MoneyMainUI.MONEY_CELLS_COUNT)
                return resCountWithPrefix;
            else if (resCountWithPrefix.Length == MoneyMainUI.MONEY_CELLS_COUNT + 1)
                return $"{resCount:F1}{_prefixes[index]}";
            return $"{Mathf.Floor(resCount)}{_prefixes[index]}";
        }
    }
}
