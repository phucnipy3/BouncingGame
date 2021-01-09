using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingGame.GameObjects
{
    public class ItemAddBall : SpriteGameObject
    {
        public int Row { get; private set; } = 0;
        private Vector2 targetPosition;
        private IncreaseEffect visualEffect;

        public ItemAddBall(int column): base("Sprites/UI/spr_item_add_ball", Depth.Item)
        {
            SetOriginToCenter();
            LocalPosition = new Vector2(50 + column * 100,50 + 150);
            targetPosition = LocalPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (visualEffect != null)
                visualEffect.Draw(gameTime, spriteBatch);
        }

        public void MoveDown()
        {
            Row++;
            targetPosition = LocalPosition + new Vector2(0, 100);
            velocity = new Vector2(0, 1) * Constant.MoveDownVelocity;
        }

        public override void Update(GameTime gameTime)
        {
            if (visualEffect != null)
                visualEffect.Update(gameTime);

            base.Update(gameTime);

            if (targetPosition.Y - localPosition.Y <= 0)
            {
                LocalPosition = targetPosition;
                velocity = Vector2.Zero;
            }

            if (LocalPosition.Y >= 996)
            {
                Visible = false;
            }
        }

        public Circle BouncingBox
        {
            get
            {
                return new Circle(Width / 2, GlobalPosition);
            }
        }

        public void PlayEffect()
        {
            visualEffect = new IncreaseEffect(GlobalPosition);
        }


    }
}
