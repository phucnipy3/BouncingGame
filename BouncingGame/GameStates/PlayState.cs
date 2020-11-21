using BouncingGame.GameObjects;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace BouncingGame.GameStates
{
    public class PlayState : GameState
    {
        public Vector2 DropPosition { get; set; } = new Vector2(350, 1050);

        private Director director;
        private ListBall listBall;

        public PlayState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 1));
            listBall = new ListBall();
            gameObjects.AddChild(listBall);
            listBall.LocalPosition = DropPosition;
            director = new Director(listBall);
            gameObjects.AddChild(director);
            AddNewRow(8);

            listBall.AddBall();
            listBall.AddBall();
            listBall.AddBall();

            
        }

        private void AddNewRow(int level)
        {
            int durability = GetDurability(level);
            gameObjects.AddChild(new Brick(durability, new Vector2(0, 250), listBall));
            gameObjects.AddChild(new Brick(durability, new Vector2(100, 250), listBall));
            gameObjects.AddChild(new Brick(durability, new Vector2(200, 250), listBall));
            gameObjects.AddChild(new Brick(durability, new Vector2(300, 250), listBall));
            gameObjects.AddChild(new Brick(durability, new Vector2(600, 250), listBall));
        }

        private int GetDurability(int level)
        {
            return level * 10;
        }
    }
}
