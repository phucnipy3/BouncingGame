using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BouncingGame.GameObjects
{
    public class ListItemSpreadBall : GameObject
    {
        private List<ItemSpreadBall> items;

        private static ListItemSpreadBall instance = new ListItemSpreadBall();

        public static ListItemSpreadBall Instance
        {
            get
            {
                return instance;
            }
        }

        private ListItemSpreadBall()
        {
            items = new List<ItemSpreadBall>();
        }

        public void AddItem()
        {
            var newItem = new ItemSpreadBall(3);
            items.Add(newItem);
            newItem.Parent = this;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Visible)
                return;
            foreach (var item in items)
            {
                item.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var item in items)
            {
                item.Draw(gameTime, spriteBatch);
            }
        }

        public void MoveDown()
        {
            foreach (var item in items)
            {
                item.MoveDown();
            }
        }

        public override void Reset()
        {
            items.Clear();
        }

        public bool CheckCollisionWithBall(Ball ball)
        {
            var touched = false;
            foreach (var item in items)
            {
                if (!item.Visible)
                    continue;
                if (CollisionDetection.ShapesIntersect(item.BouncingBox, new Circle(ball.Radius, ball.GlobalCenter)))
                {
                    if (!item.Intersecting(ball))
                    {
                        ball.ReflectRandom();
                        item.StartIntersect(ball);
                        touched = true;
                    }

                }
                else
                {
                    if (item.Intersecting(ball))
                    {
                        item.StopIntersect(ball);
                    }
                }
            }

            return touched;
        }
    }
}
