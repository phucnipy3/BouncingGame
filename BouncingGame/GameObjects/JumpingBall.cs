using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class JumpingBall : SpriteGameObject
    {

        private float gravityY = 1000;
        private float bottom;

        public JumpingBall(string spriteName, float depth, Vector2 dropPoint, float distanceInPixel, float durationInSecond) : base(spriteName, depth)
        {
            LocalPosition = dropPoint;
            this.bottom = dropPoint.Y + distanceInPixel;
            gravityY = distanceInPixel / (durationInSecond * durationInSecond);
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Y += gravityY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
            if (LocalPosition.Y > bottom)
            {
                velocity.Y *= -1;
                LocalPosition = new Vector2(
                    LocalPosition.X, (float)((int)bottom));
            } 
        }
    }
}
