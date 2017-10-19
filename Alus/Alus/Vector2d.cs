namespace Alus
{
    public struct Vector2d
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public static Vector2d UnitX = new Vector2d(1, 0);
        public static Vector2d UnitY = new Vector2d(0, 1);

        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2d operator -(Vector2d vector)
        {
            return new Vector2d(-vector.X, -vector.Y);
        }

        public static Vector2d operator *(Vector2d vector, double weight)
        {
            return new Vector2d(weight * vector.X, weight * vector.Y);
        }

        public static Vector2d operator *(double weight, Vector2d vector)
        {
            return new Vector2d(weight * vector.X, weight * vector.Y);
        }
    }
}
