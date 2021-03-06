﻿using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BouncingGame.GameObjects
{
    public class ListItemClearColumn : GameObject
    {
        private List<ItemClearColumn> items;

        private static ListItemClearColumn instance = new ListItemClearColumn();

        public static ListItemClearColumn Instance
        {
            get
            {
                return instance;
            }
        }

        private ListItemClearColumn()
        {
            items = new List<ItemClearColumn>();
        }

        public void AddItem()
        {
            var newItem = new ItemClearColumn(4);
            items.Add(newItem);
            newItem.Parent = this;
        }

        public void AddItems(IEnumerable<int> cols)
        {
            foreach (var col in cols)
            {
                var newItem = new ItemClearColumn(col);
                items.Add(newItem);
                newItem.Parent = this;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Visible)
                return;
            items.RemoveAll(x => !x.Visible);
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
                        ListBrick.Instance.ClearColumn(item.Column);
                        item.StartIntersect(ball);
                        item.PlayEffect();
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

        public void ClearDeadRows()
        {
            foreach (var item in items)
            {
                if (item.Row > 6)
                    item.Visible = false;
            }
        }
    }
}
