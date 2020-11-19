﻿using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameStates
{
    public class HomeState : GameState
    {
        Button getBallButton;
        Button changeBallButton;
        Button playButton;
        public HomeState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 1));
            
            getBallButton = new Button("Sprites/Buttons/spr_btn_get_ball", 1);
            getBallButton.LocalPosition = new Vector2(100, 100);
            changeBallButton = new Button("Sprites/Buttons/spr_btn_change_ball", 1);
            changeBallButton.LocalPosition = new Vector2(200, 200);
            playButton = new Button("Sprites/Buttons/spr_btn_play", 1);
            playButton.LocalPosition = new Vector2(300, 300);

            gameObjects.AddChild(getBallButton);
            gameObjects.AddChild(changeBallButton);
            gameObjects.AddChild(playButton);

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (getBallButton.Pressed)
            {
                // TODO: switch to get ball
                //ExtendedGame.GameStateManager.SwitchTo();
            }
            if (changeBallButton.Pressed)
            {
                // TODO: switch to change ball
                //ExtendedGame.GameStateManager.SwitchTo();
            }
            if (playButton.Pressed)
            {
                // TODO: switch to play
                //ExtendedGame.GameStateManager.SwitchTo();
            }
        }
    }
}