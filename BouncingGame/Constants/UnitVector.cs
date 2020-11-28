using Microsoft.Xna.Framework;

namespace BouncingGame.Constants
{
    public class UnitVector
    {
        public static Vector2 Angle180
        {
            get
            {
                return new Vector2(-1, 0);
            }
        }

        public static Vector2 Angle0
        {
            get
            {
                return new Vector2(1, 0);
            }
        }

        public static Vector2 Angle90
        {
            get
            {
                return new Vector2(0, -1);
            }
        }
        public static Vector2 Angle270
        {
            get
            {
                return new Vector2(0, 1);
            }
        }

        public static Vector2 Angle135
        {
            get
            {
                var x =  new Vector2(-1, -1);
                x.Normalize();
                return x;
            }
        }

        public static Vector2 Angle45
        {
            get
            {
                var x = new Vector2(1, -1);
                x.Normalize();
                return x;
            }
        }

        public static Vector2 Angle225
        {
            get
            {
                var x = new Vector2(-1, 1);
                x.Normalize();
                return x;
            }
        }

        public static Vector2 Angle315
        {
            get
            {
                var x = new Vector2(1, 1);
                x.Normalize();
                return x;
            }
        }

        public static Vector2 Combine(Vector2 u1, Vector2 u2)
        {
            u1.Normalize();
            u2.Normalize();
            Vector2 tempVector = u1 + u2;
            tempVector.Normalize();
            return tempVector;
        }

    }
}
