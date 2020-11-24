using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class ListBall : GameObjectList
    {
        public List<Ball> Balls { get; private set; }

        public Vector2 DropPosition { get; set; } = new Vector2(9, 1050);


        private static ListBall instance = new ListBall();


        public static ListBall Instance
        {
            get
            {
                return instance;
            }
        }

        public bool Shooting
        {
            get
            {
                return Balls.Any(x => x.Shooting);
            }
        }

        public bool NonDrop
        {
            get
            {
                return Balls.All(x => x.Shooting);
            }
        }

        public Ball FirstDropBall { get; set; }

        private ListBall()
        {
            Balls = new List<Ball>();
        }

        public void AddBall()
        {
            var newBall = new Ball();
            newBall.LocalPosition = DropPosition;
            Balls.Add(newBall);
            AddChild(newBall);
        }

        public void Shoot(float rotation)
        {
            double peddingTime = 0;
            foreach (var ball in Balls)
            {
                ball.Shoot(rotation, peddingTime);
                peddingTime += 6d * 0.01699999998;
            }
        }

        public void AllDrop()
        {
            DropPosition = FirstDropBall.LocalPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
