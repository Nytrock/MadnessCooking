public static class DirectionMethods {
    public static Direction ToDirection(this float normalizedDirection) {
        if (normalizedDirection == 0f)
            return Direction.None;
        else if (normalizedDirection == 1f)
            return Direction.Right;
        return Direction.Left;
    }

    public static Direction ToDirection(this bool isRight) {
        if (isRight)
            return Direction.Right;
        return Direction.Left;
    }
}
