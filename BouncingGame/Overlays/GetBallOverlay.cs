﻿using BouncingGame.Constants;
using BouncingGame.GameObjects;
using BouncingGame.Helpers;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingGame.Overlays
{
    public class GetBallOverlay : Overlay
    {
        private SpriteGameObject background;
        private AnimatedGameObject giftBox;
        private AnimatedGameObject glowing;
        private float loadingTime = 1f;
        private float timeEslapsed = 0;
        private TextGameObject message;
        private RandomBall randomBall;
        private ListStar fireWorkMaker;
        private float interval = 0.6f;

        public GetBallOverlay() : base()
        {
            giftBox = new AnimatedGameObject(Depth.GiftBox);
            giftBox.LoadAnimation("Sprites/Animations/spr_animation_getball@4x3", "box", false, 0.05f);
            giftBox.PlayAnimation("box", true);
            giftBox.SetOriginToCenter();
            giftBox.LocalPosition = new Vector2(320, 600);
            AddChild(giftBox);

            glowing = new AnimatedGameObject(Depth.OverlayButton2);
            glowing.LoadAnimation("Sprites/Animations/spr_animation_glowing@5x2", "glowing", true, 0.1f);
            glowing.PlayAnimation("glowing", true);
            glowing.SetOriginToCenter();
            glowing.LocalPosition = new Vector2(350, 500);
            AddChild(glowing);

            background = new SpriteGameObject("Sprites/Backgrounds/spr_getball", Depth.OverlayBackground2);
            AddChild(background);

            message = new TextGameObject("Fonts/GetBall", Depth.OverlayMessage, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            AddChild(message);
            message.Visible = false;
            message.Text = "Click to get ball";
            message.LocalPosition = new Vector2(350, 900);

            fireWorkMaker = new ListStar(Depth.RandomBall + 0.001f);
            AddChild(fireWorkMaker);
            Reset();
        }

        public override void Show()
        {
            base.Show();
            giftBox.PlayAnimation("box", true);
            timeEslapsed = 0f;
            message.Visible = false;
            randomBall = new RandomBall(GameSettingHelper.GetRandomBall().LargeSpritePath);
            glowing.Visible = false;
            ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_gift");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timeEslapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(!message.Visible)
            {
                if (timeEslapsed > loadingTime)
                {
                    message.Visible = true;
                    timeEslapsed -= loadingTime;
                }
            }
            else
            {
                if(timeEslapsed > interval)
                {
                    timeEslapsed -= interval;
                    fireWorkMaker.CreateFireWork(ExtendedGame.Random.Next(40, 60), new Vector2(ExtendedGame.Random.Next(14), ExtendedGame.Random.Next(5,20)) * 50);
                }
            }
            
            

            if (randomBall != null)
            {
                randomBall.Update(gameTime);
                if (randomBall.IsAtTop)
                {
                    glowing.Visible = true;
                }
                else
                {
                    glowing.Visible = false;
                }
            }


        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (message.Visible && inputHelper.MouseLeftButtonPressed())
            {
                Hide();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (randomBall != null)
                randomBall.Draw(gameTime, spriteBatch);
        }

        public override void Hide()
        {
            base.Hide();
            if (randomBall != null)
                randomBall.Visible = false;
        }
    }
}
