using BouncingGame.Constants;
using BouncingGame.GameObjects;
using BouncingGame.Helpers;
using BouncingGame.Models;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingGame.GameStates
{
    public class HomeState : GameState
    {
        Button getBallButton;
        Button changeBallButton;
        Button playButton;
        JumpingBall jumpingBall;
        BallModel selectedBall;

        // TODO: get selected ball and display
        public HomeState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 0));
            
            getBallButton = new Button("Sprites/Buttons/spr_btn_get_ball", 0);
            getBallButton.LocalPosition = new Vector2(620, 210);
            changeBallButton = new Button("Sprites/Buttons/spr_btn_change_ball", 0);
            changeBallButton.LocalPosition = new Vector2(100, 800);
            playButton = new Button("Sprites/Buttons/spr_btn_play", 0);
            playButton.LocalPosition = new Vector2(400, 800);



            gameObjects.AddChild(getBallButton);
            gameObjects.AddChild(changeBallButton);
            gameObjects.AddChild(playButton);

            

            Reset();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (getBallButton.Pressed)
            {
                // TODO: switch to get ball
                //ExtendedGame.GameStateManager.SwitchTo();

                // TODO: remove this code
                GameSettingHelper.GenerateListBall();
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
