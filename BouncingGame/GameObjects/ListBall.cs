using BouncingGame.Constants;
using BouncingGame.GameStates;
using BouncingGame.Helpers;
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

        public Vector2 BallOffset { get; set; }


        private static ListBall instance = new ListBall();


        public static ListBall Instance
        {
            get
            {
                return instance;
            }
        }

        private int ballNumber = 0;

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

        private string spriteName;
        private TextGameObject totalBall;
        private float speed;

        private ListBall()
        {
            totalBall = new TextGameObject("Fonts/TotalBall", Depth.BallNumber, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            Reset();
        }

        public void AddBall()
        {
            var newBall = new Ball(spriteName, speed);
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
            totalBall.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public void Increase(int n)
        {
            ballNumber += n;
        }

        public override void Reset()
        {
            var model = GameSettingHelper.GetSelectedBall();
            spriteName = model.OriginSpritePath;
            speed = model.Speed;
            DropPosition = new Vector2(350, 1050);
            ballNumber = 1;
            Clear();
            AddBall();
            BallOffset = new Vector2(0, balls[0].Height) / 2;
            totalBall.Reset();
        }

        public override void Update(GameTime gameTime)
        {
            int remainingBall = balls.Where(x => x.LocalPosition == DropPosition).Count();
            totalBall.Visible = remainingBall > 0;
            totalBall.Text = "x" + remainingBall.ToString();
            totalBall.LocalPosition = DropPosition - 2 * BallOffset - new Vector2(0, 20);
            if (!Shooting)
            {
                while (ballNumber > balls.Count)
                {
                    AddBall();
                }
            }

            base.Update(gameTime);
        }
    }
}
