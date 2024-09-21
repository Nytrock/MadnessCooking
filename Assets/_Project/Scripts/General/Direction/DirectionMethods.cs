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

    public static Direction Reverse(this Direction direction) {
        switch (direction) {
            case Direction.Right: return Direction.Left;
            case Direction.Left: return Direction.Right;
            default: return Direction.None;
        }
    }

    public static float ToFloat(this Direction direction) {
        switch (direction) {
            case Direction.Right: return 1f;
            case Direction.Left: return -1f;
            default: return 0f;
        }
    }
}
