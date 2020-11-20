using BouncingGame.GameObjects;
using Engine;
using Microsoft.Xna.Framework;
using System;

namespace BouncingGame.GameStates
{
    public class PlayState : GameState
    {
        public Vector2 DropPosition { get; set; } = new Vector2(350, 1050);

        private Director director;
        public PlayState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 1));
            director = new Director();
            gameObjects.AddChild(director);
            AddNewRow(8);
        }

        private void AddNewRow(int level)
        {
            int durability = GetDurability(level);
            gameObjects.AddChild(new Brick(durability, new Vector2(0, 300)));
            gameObjects.AddChild(new Brick(durability, new Vector2(100, 300)));
            gameObjects.AddChild(new Brick(durability, new Vector2(200, 300)));
            gameObjects.AddChild(new Brick(durability, new Vector2(300, 300)));
            gameObjects.AddChild(new Brick(durability, new Vector2(400, 300)));
            gameObjects.AddChild(new Brick(durability, new Vector2(500, 300)));
            gameObjects.AddChild(new Brick(durability, new Vector2(600, 300)));
        }

        private int GetDurability(int level)
        {
            return level * 10;
        }
    }
}
