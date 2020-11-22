using BouncingGame.GameStates;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class ListBall: GameObjectList
    {
        public List<Ball> Balls { get; private set; }


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

        public ListBall()
        {
            Balls = new List<Ball>();
        }

        public void AddBall()
        {
            var newBall = new Ball();
            Balls.Add(newBall);
            AddChild(newBall);
        }

        public void Shoot(float rotation)
        {
            double peddingTime = 0;
            foreach(var ball in Balls)
            {
                ball.Shoot(rotation, peddingTime);
                peddingTime += 6d * 0.01699999998;
            }
        }

        public void AllDrop()
        {
            LocalPosition = FirstDropBall.GlobalPosition;
            ((PlayState)ExtendedGame.GameStateManager.GetGameState(Bouncing.StateName_Play)).DropPosition = LocalPosition;

            foreach(var ball in Balls)
            {
                ball.LocalPosition = Vector2.Zero;
            }
        }
    }
}
