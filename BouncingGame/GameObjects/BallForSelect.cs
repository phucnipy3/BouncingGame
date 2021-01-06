using BouncingGame.Models;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingGame.GameObjects
{
    public class BallForSelect : GameObject
    {
        public BallModel Information { get; private set; }

        private TextGameObject name;
        private TextGameObject rarity;
        private TextGameObject size;
        private JumpingBall ball;


        public BallForSelect(BallModel information)
        {
            Information = information;

            if (information.Locked)
            {
                ball = new JumpingBall(information.ShadowSpritePath, 0.1f, new Vector2(350, 600), 150, 0.2f);
            }
            else
            {
                ball = new JumpingBall(information.LargeSpritePath, 0.1f, new Vector2(350, 600), 150, 0.2f);
            }
            ball.Parent = this;
            ball.SetOriginToCenter();
            ball.Scale = 4f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            ball.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            ball.Update(gameTime);
        }
    }


}
