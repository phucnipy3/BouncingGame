using BouncingGame.Constants;
using BouncingGame.GameStates;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class ListBall : GameObjectList
    {
        private List<Ball> balls 
        {
            get
            {
                return children.Select(x => (Ball)x).ToList();
            }
        }

        public Vector2 DropPosition { get; set; } = new Vector2(350, 1050);


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
                return balls.Any(x => x.Shooting);
            }
        }

        public bool NonDrop
        {
            get
            {
                return balls.All(x => x.Shooting);
            }
        }

        public Ball FirstDropBall { get; set; }

        private ListBall()
        {
            Reset();
        }

        public void AddBall()
        {
            var newBall = new Ball();
            newBall.LocalPosition = DropPosition;
            AddChild(newBall);
        }

        public void Shoot(float rotation)
        {
            double peddingTime = 0;
            foreach (var ball in balls)
            {
                ball.Shoot(rotation, peddingTime);
                peddingTime += 6d * 0.01699999998;
            }
        }

        public void AllDrop()
        {
            DropPosition = FirstDropBall.LocalPosition;
            ((PlayState)ExtendedGame.GameStateManager.GetGameState(StateName.Play)).NextLevel();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Reset()
        {
            DropPosition = new Vector2(405, 367);
            Clear();
            AddBall();
        }
    }
}
