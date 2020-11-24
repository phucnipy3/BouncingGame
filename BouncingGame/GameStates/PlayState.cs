using BouncingGame.GameObjects;
using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameStates
{
    public class PlayState : GameState
    {
        Ball ball = new Ball();

        public PlayState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 1));
            gameObjects.AddChild(ListBall.Instance);
            gameObjects.AddChild(Director.Instance);
            gameObjects.AddChild(ListBrick.Instance);

            ListBrick.Instance.NextLevel();
            ListBall.Instance.AddBall();
            ListBall.Instance.AddBall();
            ListBall.Instance.AddBall();
            ListBall.Instance.AddBall();



        }
    }
}
