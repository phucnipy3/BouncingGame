using Microsoft.Xna.Framework;

namespace BouncingGame.Constants
{
    public class NormalVector
    {
        public static Vector2 StandRight
        {
            get
            {
                return new Vector2(1, 0);
            }
        }

        public static Vector2 StandLeft
        {
            get
            {
                return new Vector2(-1, 0);
            }
        }

        public static Vector2 LieBottom
        {
            get
            {
                return new Vector2(0, 1);
            }
        }
        public static Vector2 LieTop
        {
            get
            {
                return new Vector2(0, -1);
            }
        }

        public static Vector2 InclinedUpLeft
        {
            get
            {
                return new Vector2(-1, 1);
            }
        }

        public static Vector2 InclinedUpRight
        {
            get
            {
                return new Vector2(1, 1);
            }
        }

        public static Vector2 InclinedDownLeft
        {
            get
            {
                return new Vector2(-1, -1);
            }
        }

        public static Vector2 InclinedDownRight
        {
            get
            {
                return new Vector2(1, -1);
            }
        }

    }
}
