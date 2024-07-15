using UnityEngine;

public static class CountConverter {
    private static string[] _prefixes = { "K", "M" };
    private static string _overflowMessage = "WHAT";

    public static string ConvertCount(int count, bool isMoney = false) {
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

        string resCountFormatted = $"{resCount:F2}";
        if (isMoney) {
            if (resCountFormatted.Length > MoneyMainUI.MONEY_CELLS_COUNT)
                return $"{Mathf.Floor(resCount)}{_prefixes[index]}";
            else if (resCountFormatted.Length == MoneyMainUI.MONEY_CELLS_COUNT)
                return $"{resCount:F1}{_prefixes[index]}";
        }

        return $"{resCountFormatted}{_prefixes[index]}";
    }
}
