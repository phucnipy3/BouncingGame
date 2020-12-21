using BouncingGame.Constants;
using BouncingGame.GameObjects;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using System;

namespace BouncingGame.GameStates
{
    public class PlayState : GameState
    {
        Ball ball = new Ball();
        bool gameOver = false;
        Button pauseButton;
        Button continueButton;
        Button guideButton;
        Button homeButton;
        Button volumnButton;
        SpriteGameObject pauseBackground;
        SpriteGameObject guideBackground;
        bool pause = false;

        public PlayState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 0));
            gameObjects.AddChild(ListBrick.Instance);
            gameObjects.AddChild(ListBall.Instance);
            gameObjects.AddChild(Director.Instance);
            gameObjects.AddChild(ListItemSpreadBall.Instance);
            pauseButton = new Button("Sprites/Buttons/spr_pause", 0);
            gameObjects.AddChild(pauseButton);
            pauseButton.LocalPosition = new Vector2(10, 10);
            guideButton = new Button("Sprites/Buttons/spr_guide", 0);
            gameObjects.AddChild(guideButton);
            guideButton.LocalPosition = new Vector2(100, 10);

            pauseBackground = new SpriteGameObject("Sprites/Backgrounds/spr_pause", 0.75f);
            pauseBackground.Visible = false;
            gameObjects.AddChild(pauseBackground);
            guideBackground = new SpriteGameObject("Sprites/Backgrounds/spr_guide", 0.75f);
            guideBackground.Visible = false;
            gameObjects.AddChild(guideBackground);

            continueButton = new Button("Sprites/Buttons/spr_continue", 1);
            gameObjects.AddChild(continueButton);
            continueButton.LocalPosition = new Vector2(10, 10);
            continueButton.Visible = false;
            homeButton = new Button("Sprites/Buttons/spr_back_to_home", 1);
            gameObjects.AddChild(homeButton);
            homeButton.LocalPosition = new Vector2(600, 10);
            homeButton.Visible = false; 
            volumnButton = new Button("Sprites/Buttons/spr_mute", 1);
            gameObjects.AddChild(volumnButton);
            volumnButton.LocalPosition = new Vector2(100, 10);
            volumnButton.Visible = false;
            Level = 1;
        }

        public void GameOver()
        {
            gameOver = true;
        }

        public void NextLevel()
        {
            Level++;
            ListBrick.Instance.NextLevel();
            ListItemSpreadBall.Instance.AddItem();
            ListItemSpreadBall.Instance.MoveDown();
        }

        public override void Update(GameTime gameTime)
        {
            if (gameOver)
            {
                Reset();
            }

            if (pause)
            {
                if (continueButton.Pressed)
                {
                    HideOverlay();
                    pause = false;
                }

                if (volumnButton.Pressed)
                {
                    // do something with volumn
                }

                if (homeButton.Pressed)
                {
                    ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
                    pause = false;
                }

                return;
            }

            if (pauseButton.Pressed)
            {
                ShowPauseOverlay();
                pause = true;

            }

            if (guideButton.Pressed)
            {
                ShowGuideOverlay();
                pause = true;
            }

            base.Update(gameTime);


        }

        private void HideOverlay()
        {
            guideBackground.Visible = false;
            pauseBackground.Visible = false;
            homeButton.Visible = false;
            continueButton.Visible = false;
            volumnButton.Visible = false;
        }

        private void ShowGuideOverlay()
        {
            guideBackground.Visible = true;
            homeButton.Visible = true;
            continueButton.Visible = true;
            volumnButton.Visible = true;
        }

        private void ShowPauseOverlay()
        {
            pauseBackground.Visible = true;
            homeButton.Visible = true;
            continueButton.Visible = true;
            volumnButton.Visible = true;
        }

        public override void Reset()
        {
            gameOver = false;
            pause = false;
            HideOverlay();
            base.Reset();
        }

        public int Level { get; private set; }

    }
}
