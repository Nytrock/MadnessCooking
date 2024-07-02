public static class CountConverter {
    private static string[] _prefixes = { "K", "M", "B" };
    private static string _overflowMessage = "WHAT";

    public static string ConvertCount(int count) {
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
        else
            return $"{resCount:F2}{_prefixes[index]}";
    }
}
