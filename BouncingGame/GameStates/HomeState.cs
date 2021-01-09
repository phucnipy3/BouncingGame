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

        private GetBallOverlay getBallOverlay;

        public HomeState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", Depth.Backgroud));

            getBallButton = new Button("Sprites/Buttons/spr_btn_get_ball", Depth.Button);
            getBallButton.LocalPosition = new Vector2(620, 210);
            changeBallButton = new Button("Sprites/Buttons/spr_btn_change_ball", Depth.Button);
            changeBallButton.LocalPosition = new Vector2(100, 800);
            playButton = new Button("Sprites/Buttons/spr_btn_play", Depth.Button);
            playButton.LocalPosition = new Vector2(400, 800);


            gameObjects.AddChild(getBallButton);
            gameObjects.AddChild(changeBallButton);
            gameObjects.AddChild(playButton);

            getBallOverlay = new GetBallOverlay();
            gameObjects.AddChild(getBallOverlay);

            Reset();
        }

        public override void HandleInput(InputHelper inputHelper)
        {

            if (getBallOverlay.Visible)
            {
                getBallOverlay.HandleInput(inputHelper);
                return;
            }

            base.HandleInput(inputHelper);

            if (getBallButton.Pressed)
            {
                // TODO: switch to get ball
                //ExtendedGame.GameStateManager.SwitchTo();

                // TODO: remove this code
                //GameSettingHelper.GenerateListBall();

                getBallOverlay.Show();
            }
            if (changeBallButton.Pressed)
            {
                ExtendedGame.GameStateManager.SwitchTo(StateName.ChangeBall);
            }
            if (playButton.Pressed)
            {
                ExtendedGame.GameStateManager.SwitchTo(StateName.Play);
            }
        }

        public override void Reset()
        {
            base.Reset();
            selectedBall = GameSettingHelper.GetSelectedBall();
            jumpingBall = new JumpingBall(selectedBall.OriginSpritePath, 0, new Vector2(600, 650), 150, 0.2f);
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
        }
    }
}
