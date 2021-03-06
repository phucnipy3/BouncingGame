﻿using BouncingGame.Constants;
using BouncingGame.Models;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingGame.GameObjects
{
    public class BallForSelect : GameObject
    {
        private BallModel information;
        public BallModel Information {
            get
            {
                return information;
            }
            set 
            {
                information = value;
                ball = new JumpingBall(information.LargeSpritePath, Depth.OverlayButton, new Vector2(350, 600), 100, 0.2f);
                shadowBall = new JumpingBall(information.ShadowSpritePath, Depth.OverlayButton, new Vector2(350, 600), 100, 0.2f);
                ball.Parent = this;
                ball.SetOriginToCenter();
                shadowBall.Parent = this;
                shadowBall.SetOriginToCenter();
            }
        }

        private JumpingBall ball;
        private JumpingBall shadowBall;


        public BallForSelect()
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (information == null)
                return;

            if (information.Locked)
            {

                if (string.IsNullOrWhiteSpace(information.ShadowSpritePath))
                    return;
                shadowBall.Draw(gameTime, spriteBatch);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(information.LargeSpritePath))
                    return;
                ball.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (information == null)
                return;
            ball.Update(gameTime);
            shadowBall.Update(gameTime);
        }
    }


}
