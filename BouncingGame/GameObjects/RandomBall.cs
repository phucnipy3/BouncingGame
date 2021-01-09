using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class RandomBall : SpriteGameObject
    {
        private Vector2 targetPosition;
        public bool IsAtTop { get; private set; } = false;
        public RandomBall(string spriteName): base(spriteName, Depth.RandomBall)
        {
            Scale = 150 / sprite.Width;
            SetOriginToCenter();
            LocalPosition = new Vector2(350, 600);
            targetPosition = LocalPosition - new Vector2(0, 100);
        }

        public override void Update(GameTime gameTime)
        {
            velocity = (targetPosition - LocalPosition) * (float)gameTime.ElapsedGameTime.TotalSeconds * 150;

            base.Update(gameTime);

            if(Vector2.Distance(targetPosition, LocalPosition) < 5)
            {
                LocalPosition = targetPosition;
                IsAtTop = true;
            }
        }
    }
}
