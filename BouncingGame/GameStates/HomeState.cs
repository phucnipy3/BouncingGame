using BouncingGame.Constants;
using BouncingGame.GameObjects;
using BouncingGame.Helpers;
using BouncingGame.Models;
using BouncingGame.Overlays;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingGame.GameStates
{
    public class HomeState : GameState
    {
        private Button getBallButton;
        private Button changeBallButton;
        private Button playButton;
        private JumpingBall jumpingBall;
        private BallModel selectedBall;
        private SpriteGameObject logo;
        private TextGameObject moneyText;

        private GetBallOverlay getBallOverlay;
        private ConfirmOverlay confirmOverlay;

        public HomeState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_play", Depth.Backgroud));

            getBallButton = new Button("Sprites/Buttons/spr_btn_get_ball", Depth.Button);
            getBallButton.SetOriginToRightTop();
            getBallButton.LocalPosition = new Vector2(680, 170);
            
            changeBallButton = new Button("Sprites/Buttons/spr_btn_change_ball", Depth.Button);
            changeBallButton.SetOriginToLeftBottom();
            changeBallButton.LocalPosition = new Vector2(100, 940);
            
            playButton = new Button("Sprites/Buttons/spr_btn_play", Depth.Button);
            playButton.SetOriginToRightBottom();
            playButton.LocalPosition = new Vector2(600, 940);

            logo = new SpriteGameObject("Sprites/UI/spr_logo_name", Depth.Button);
            logo.SetOriginToCenter();
            logo.LocalPosition = new Vector2(350, 550);
            gameObjects.AddChild(logo);

            moneyText = new TextGameObject("Fonts/PlayMoney", Depth.Button, Color.White, TextGameObject.HorizontalAlignment.Left, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(moneyText);
            moneyText.LocalPosition = new Vector2(120, 1132);


            gameObjects.AddChild(getBallButton);
            gameObjects.AddChild(changeBallButton);
            gameObjects.AddChild(playButton);

            getBallOverlay = new GetBallOverlay();
            gameObjects.AddChild(getBallOverlay);

            confirmOverlay = new ConfirmOverlay(getBallOverlay);
            gameObjects.AddChild(confirmOverlay);

            Reset();
        }

        public override void HandleInput(InputHelper inputHelper)
        {

            if (getBallOverlay.Visible)
            {
                getBallOverlay.HandleInput(inputHelper);
                return;
            }

            if (confirmOverlay.Visible)
            {
                confirmOverlay.HandleInput(inputHelper);
                return;
            }

            base.HandleInput(inputHelper);

            if (getBallButton.Pressed)
            {
                ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_click");

                // TODO: remove this code
                //GameSettingHelper.GenerateListBall();

                confirmOverlay.Show();
            }
            if (changeBallButton.Pressed)
            {
                ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_click");
                ExtendedGame.GameStateManager.SwitchTo(StateName.ChangeBall);
            }
            if (playButton.Pressed)
            {
                ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_click");
                ExtendedGame.GameStateManager.SwitchTo(StateName.Play);
            }
        }

        public override void Reset()
        {
            base.Reset();
            selectedBall = GameSettingHelper.GetSelectedBall();
            jumpingBall = new JumpingBall(selectedBall.OriginSpritePath, 0, new Vector2(525, 720), 110, 0.2f);
            jumpingBall.SetOriginToCenterBottom();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (jumpingBall != null)
                jumpingBall.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (jumpingBall != null)
                jumpingBall.Update(gameTime);
            moneyText.Text = GameSettingHelper.GetMoney().ToString("N0");
        }
    }
}
