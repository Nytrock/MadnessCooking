namespace MadnessCooking.General {
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
            return direction switch {
                Direction.Right => Direction.Left,
                Direction.Left => Direction.Right,
                _ => Direction.None,
            };
        }

        public static float ToFloat(this Direction direction) {
            return direction switch {
                Direction.Right => 1f,
                Direction.Left => -1f,
                _ => 0f,
            };
        }
    }
}
