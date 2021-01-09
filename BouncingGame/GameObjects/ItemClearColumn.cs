using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BouncingGame.GameObjects
{
    public class ItemClearColumn : SpriteGameObject
    {
        public int Row { get; private set; } = 0;
        private Vector2 targetPosition;
        private bool intersected = false;
        private int column;
        private List<ClearColumnEffect> visualEffects = new List<ClearColumnEffect>();

        public int Column
        {
            get
            {
                return column;
            }
        }

        public ItemClearColumn(int column): base("Sprites/UI/spr_item_break_vertical", 0f)
        {
            SetOriginToCenter();
            LocalPosition = new Vector2(50 + column * 100,50 + 150);
            targetPosition = LocalPosition;
            this.column = column;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (var effect in visualEffects)
            {
                effect.Draw(gameTime, spriteBatch);
            }
        }

        public void MoveDown()
        {
            if (intersected)
            {
                Visible = false;
                return;
            }
            Row++;
            targetPosition = LocalPosition + new Vector2(0, 100);
            velocity = new Vector2(0, 1) * Constant.MoveDownVelocity;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var effect in visualEffects)
            {
                effect.Update(gameTime);
            }

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

        public bool Intersecting(Ball ball)
        {
            return intersectingBalls.Contains(ball);
        }

        private List<Ball> intersectingBalls = new List<Ball>();

        public void StartIntersect(Ball ball)
        {
            if (!Intersecting(ball))
            {
                intersectingBalls.Add(ball);
            }
            intersected = true;
        }

        public void StopIntersect(Ball ball)
        {
            if (Intersecting(ball))
            {
                intersectingBalls.Remove(ball);
            }
        }

        public void PlayEffect()
        {
            // play sound
            visualEffects.Add(new ClearColumnEffect(column));
        }
    }
}
