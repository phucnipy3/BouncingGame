﻿using BouncingGame.GameObjects;
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
            AddNewRow(8);

            ListBall.Instance.AddBall();
            ListBall.Instance.AddBall();
            ListBall.Instance.AddBall();
            ListBall.Instance.AddBall();



        }

        private void AddNewRow(int level)
        {
            int durability = GetDurability(level);
            gameObjects.AddChild(new Brick(durability, new Vector2(3, 250 + 3)));
            gameObjects.AddChild(new Brick(durability, new Vector2(100 + 3, 250 + 3)));
            gameObjects.AddChild(new Brick(durability, new Vector2(200 + 3, 250 + 3)));
            gameObjects.AddChild(new Brick(durability, new Vector2(300 + 3, 250 + 3)));
            gameObjects.AddChild(new Brick(durability, new Vector2(600 + 3, 250 + 3)));
        }

        private int GetDurability(int level)
        {
            return level * 10;
        }


    }
}