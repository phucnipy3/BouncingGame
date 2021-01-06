using BouncingGame.Constants;
using BouncingGame.GameObjects;
using BouncingGame.Helpers;
using BouncingGame.Models;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameStates
{
    public class ChangeBallState : GameState
    {
        private Button backButton;
        private Button selectButton;
        private BallForSelect ballForSelect;
        private BallModel selectingBall;
        private List<BallModel> listBall;

        public ChangeBallState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_changeball", 0));

            backButton = new Button("Sprites/Buttons/spr_btn_back", 0);
            backButton.LocalPosition = new Vector2(10, 160);
            gameObjects.AddChild(backButton);

            selectButton = new Button("Sprites/Buttons/spr_btn_select", 0);
            selectButton.LocalPosition = new Vector2(300, 1000);
            gameObjects.AddChild(selectButton);

            ballForSelect = new BallForSelect();
            gameObjects.AddChild(ballForSelect);

            
            Reset();
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
                GameSettingHelper.ChangeSelectedBall(selectingBall.Id);
                ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
            }
        }

        public override void Reset()
        {
            base.Reset();
            listBall = GameSettingHelper.GetListBall();
            var selectedBall = GameSettingHelper.GetSelectedBall();
            selectingBall = listBall.FirstOrDefault(x => x.Id == selectedBall.Id);
            UpdateSelectingBall();
        }

        public void SelectLeftBall()
        {
            selectingBall =  listBall[(listBall.IndexOf(selectingBall) - 1) % listBall.Count()];
            UpdateSelectingBall();
        }

        public void SelectRightBall()
        {
            selectingBall = listBall[(listBall.IndexOf(selectingBall) + 1) % listBall.Count()];
            UpdateSelectingBall();
        }

        private void UpdateSelectingBall()
        {
            if (selectingBall == null)
                return;
            ballForSelect.Information = selectingBall;
        }
    }
}
