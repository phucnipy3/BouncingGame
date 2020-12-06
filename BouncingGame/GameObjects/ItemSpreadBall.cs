using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncingGame.GameObjects
{
    public class ItemSpreadBall : GameObject
    {
        private int row = 0;
        private SpriteGameObject item;
        private Vector2 targetPosition;
        private bool intersected = false;

        public int Row
        {
            get
            {
                return row;
            }
        }

        public ItemSpreadBall(int column)
        {
            item = new SpriteGameObject("Sprites/UI/spr_item_spread_ball", 0f);
            item.SetOriginToCenter();
            item.Parent = this;
            item.LocalPosition = new Vector2(50, 50);
            LocalPosition = new Vector2(column * 100, 150);
            targetPosition = LocalPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            item.Draw(gameTime, spriteBatch);
        }

        public void MoveDown()
        {
            if (intersected)
            {
                Visible = false;
                return;
            }
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
    }
}
