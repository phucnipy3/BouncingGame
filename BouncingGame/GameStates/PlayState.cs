using BouncingGame.GameObjects;
using Engine;
using Microsoft.Xna.Framework;
using System;

namespace BouncingGame.GameStates
{
    public class PlayState : GameState
    {
        Ball ball = new Ball();
        bool gameOver = false;

        public PlayState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 0));
            gameObjects.AddChild(ListBrick.Instance);
            gameObjects.AddChild(ListBall.Instance);
            gameObjects.AddChild(Director.Instance);
            gameObjects.AddChild(ListItemAddBall.Instance);
        }

        public void GameOver()
        {
            gameOver = true;
        }

        public void NextLevel()
        {
            ListBrick.Instance.NextLevel();
            ListItemAddBall.Instance.AddItem();
            ListItemAddBall.Instance.MoveDown();
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
