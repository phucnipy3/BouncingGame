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

        public PlayState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 0));
            gameObjects.AddChild(ListBrick.Instance);
            gameObjects.AddChild(ListBall.Instance);
            gameObjects.AddChild(Director.Instance);
            gameObjects.AddChild(ListItemSpreadBall.Instance);
            pauseButton = new Button("Sprites/UI/spr_pause", 0);
            gameObjects.AddChild(pauseButton);
            pauseButton.LocalPosition = new Vector2(10, 10);
            
        }

        public void GameOver()
        {
            gameOver = true;
        }

        public void NextLevel()
        {
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
            base.Update(gameTime);
        }

        public override void Reset()
        {
            gameOver = false;
            base.Reset();
        }
    }
}
