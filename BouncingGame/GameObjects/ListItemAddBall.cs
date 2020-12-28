﻿using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncingGame.GameObjects
{
    public class ListItemAddBall: GameObject
    {
        private List<ItemAddBall> items;

        private static ListItemAddBall instance = new ListItemAddBall();

        public static ListItemAddBall Instance
        {
            get
            {
                return instance;
            }
        }

        private ListItemAddBall()
        {
            items = new List<ItemAddBall>();
        }

        public void AddItem()
        {
            var newItem = new ItemAddBall(4);
            items.Add(newItem);
            newItem.Parent = this;
        }

        public void AddItems(IEnumerable<int> cols)
        {
            foreach(var col in cols)
            {
                var newItem = new ItemAddBall(col);
                items.Add(newItem);
                newItem.Parent = this;
            }
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
                    item.Visible = false;
                    item.PlayAnimation();
                    ListBall.Instance.Increase(1);
                }
            }
        }
    }
}
