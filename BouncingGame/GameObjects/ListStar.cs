using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BouncingGame.GameObjects
{
    public class ListStar : GameObject
    {
        private List<Star> stars;
        private float depth;
        public ListStar(float depth)
        {
            stars = new List<Star>();
            this.depth = depth;
        }

        public override void Update(GameTime gameTime)
        {
            stars.RemoveAll(x => !x.Visible);
            foreach(var star in stars)
                star.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var star in stars)
                star.Draw(gameTime, spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();

            stars.Clear();
        }

        public void CreateFireWork(int number, Vector2 startLocation)
        {
            number = Math.Abs(number);
            for (int i = 0; i < number; i++)
                stars.Add(new Star(startLocation, depth));
        }
    }
}
