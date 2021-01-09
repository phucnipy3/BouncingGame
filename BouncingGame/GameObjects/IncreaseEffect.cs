using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class IncreaseEffect : AnimatedGameObject
    {
        private Vector2 targetPosition;

        public IncreaseEffect(Vector2 hostLocation) : base(Depth.Effect)
        {
            LoadAnimation("Sprites/Animations/spr_animation_increase_one@15", "increase", false, 0.05f);
            PlayAnimation("increase", true);
            SetOriginToCenter();
            LocalPosition = hostLocation;
            targetPosition = hostLocation - new Vector2(0, 100);
        }

        public override void Update(GameTime gameTime)
        {
            velocity = (targetPosition - LocalPosition) * (float)gameTime.ElapsedGameTime.TotalSeconds * 20;
            base.Update(gameTime);
        }

    }
}
