using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BouncingGame.GameObjects
{
    public class ItemAddCoin : GameObject
    {
        private int row = 0;
        private SpriteGameObject item;
        private Vector2 targetPosition;
        private AnimatedGameObject increase;
        private VisibilityTimer visibilityTimerIncrease;

        public ItemAddCoin(int column)
        {
            item = new SpriteGameObject("Sprites/UI/spr_item_add_coin", 0f);
            item.SetOriginToCenter();
            item.Parent = this;
            item.LocalPosition = new Vector2(50, 50);
            LocalPosition = new Vector2(column * 100, 150);
            targetPosition = LocalPosition;
            increase = new AnimatedGameObject(0.25f);
            increase.LoadAnimation("Sprites/Animations/spr_animation_increase_one@15", "increase", false, 0.03f);
            increase.LocalPosition = new Vector2(-50, -50);
            visibilityTimerIncrease = new VisibilityTimer(increase);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            item.Draw(gameTime, spriteBatch);
            visibilityTimerIncrease.Draw(gameTime, spriteBatch);
        }

        public void MoveDown()
        {
            row++;
            targetPosition = LocalPosition + new Vector2(0, 100);
            velocity = new Vector2(0, 1) * Constant.MoveDownVelocity;
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);

            if (targetPosition.Y - localPosition.Y <= 0)
            {
                LocalPosition = targetPosition;
                velocity = Vector2.Zero;
            }

            if (LocalPosition.Y >= 946)
            {
                Visible = false;
            }
        }

        public Circle BouncingBox
        {
            get
            {
                return new Circle(item.Width / 2, item.GlobalPosition);
            }
        }

        public void PlayAnimation()
        {
            visibilityTimerIncrease.StartVisible(0.5f);
            increase.PlayAnimation("increase");
        }


    }
}
