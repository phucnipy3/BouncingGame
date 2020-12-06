using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BouncingGame.GameObjects
{
    public class ListItemClearRow : GameObject
    {
        private List<ItemClearRow> items;

        private static ListItemClearRow instance = new ListItemClearRow();

        public static ListItemClearRow Instance
        {
            get
            {
                return instance;
            }
        }

        private ListItemClearRow()
        {
            items = new List<ItemClearRow>();
        }

        public void AddItem()
        {
            var newItem = new ItemClearRow(4);
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

        public void CheckCollisionWithBall(Ball ball)
        {
            foreach (var item in items)
            {
                if (!item.Visible)
                    continue;
                if (CollisionDetection.ShapesIntersect(item.BouncingBox, new Circle(ball.Radius, ball.GlobalCenter)))
                {
                    if (!item.Intersecting(ball))
                    {
                        ListBrick.Instance.ClearRow(item.Row);
                        item.StartIntersect(ball);
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
        }
    }
}
