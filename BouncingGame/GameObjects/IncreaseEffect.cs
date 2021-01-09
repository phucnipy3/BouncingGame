using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class IncreaseEffect : AnimatedGameObject
    {
        private Vector2 targetPosition;

        public IncreaseEffect(Vector2 hostLocation) : base(1f)
        {
            LoadAnimation("Sprites/Animations/", "increase", false, 0.5f);
            PlayAnimation("increase", true);
            SetOriginToCenter();
            LocalPosition = hostLocation;
            targetPosition = hostLocation - new Vector2(0, 10);
        }

        public override void Update(GameTime gameTime)
        {
            velocity = (targetPosition - LocalPosition) * (float)gameTime.ElapsedGameTime.TotalSeconds * 10;
            base.Update(gameTime);
        }

    }
}
