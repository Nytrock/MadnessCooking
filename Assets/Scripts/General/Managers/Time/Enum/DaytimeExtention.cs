public static class DaytimeExtention {
    public static string GetText(this Daytime daytime) {
        return nameof(Daytime) + "." + daytime.ToString();
    }
}
