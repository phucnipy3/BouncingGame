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
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 1));
            gameObjects.AddChild(ListBrick.Instance);
            gameObjects.AddChild(ListBall.Instance);
            gameObjects.AddChild(Director.Instance);
        }

        public void GameOver()
        {
            gameOver = true;
        }

        public void NextLevel()
        {
            ListBrick.Instance.NextLevel();
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
