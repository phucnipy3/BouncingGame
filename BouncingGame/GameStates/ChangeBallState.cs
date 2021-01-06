using BouncingGame.Constants;
using BouncingGame.GameObjects;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncingGame.GameStates
{
    public class ChangeBallState: GameState
    {
        private Button backButton;
        private Button selectButton;
        private BallForSelect ballForSelect;

        public ChangeBallState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_changeball", 0));

            backButton = new Button("Sprites/Buttons/spr_btn_back", 0);
            backButton.LocalPosition = new Vector2(10, 160);
            gameObjects.AddChild(backButton);

            selectButton = new Button("Sprites/Buttons/spr_btn_select", 0);
            selectButton.LocalPosition = new Vector2(300, 1000);
            gameObjects.AddChild(selectButton);


            Reset();
            // TODO: Get selected ball and create ball for select

            ballForSelect = new BallForSelect(new BallModel
            {
                Locked = false,
                SpritePath = "Sprites/UI/spr_ball_normal_4mm"
            });

            gameObjects.AddChild(ballForSelect);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (backButton.Pressed)
            {
                ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
            }

            if (selectButton.Pressed)
            {
                //TODO: change the ball
                ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
            }
        }
    }
}
