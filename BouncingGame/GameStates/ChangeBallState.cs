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
        private Button selectLeftBallButton;
        private Button selectRightBallButton;
        private TextGameObject name;
        private TextGameObject rarity;
        private TextGameObject size;

        public ChangeBallState()
        {
            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_changeball", 0));

            name = new TextGameObject("Fonts/BallName", 0, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(name);
            name.LocalPosition = new Vector2(350, 300);

            rarity = new TextGameObject("Fonts/Rarity", 0, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(rarity);
            rarity.LocalPosition = new Vector2(350, 370);

            size = new TextGameObject("Fonts/Size", 0, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(size);
            size.LocalPosition = new Vector2(350, 440);

            backButton = new Button("Sprites/Buttons/spr_btn_back", 0);
            backButton.LocalPosition = new Vector2(10, 160);
            gameObjects.AddChild(backButton);

            selectButton = new Button("Sprites/Buttons/spr_btn_select", 0);
            selectButton.LocalPosition = new Vector2(300, 1000);
            gameObjects.AddChild(selectButton);

            ballForSelect = new BallForSelect();
            gameObjects.AddChild(ballForSelect);

            selectLeftBallButton = new Button("Sprites/Buttons/spr_arrow_left", 0);
            selectLeftBallButton.SetOriginToCenter();
            selectLeftBallButton.LocalPosition = new Vector2(20, 600);
            gameObjects.AddChild(selectLeftBallButton);

            selectRightBallButton = new Button("Sprites/Buttons/spr_arrow_right", 0);
            selectRightBallButton.SetOriginToCenter();
            selectRightBallButton.LocalPosition = new Vector2(600, 600);
            gameObjects.AddChild(selectRightBallButton);
            
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

            if (selectLeftBallButton.Pressed)
            {
                SelectLeftBall();
            }

            if (selectRightBallButton.Pressed)
            {
                SelectRightBall();
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
            if (selectingBall.Locked)
            {
                selectButton.Visible = false;
            }
            else
            {
                selectButton.Visible = true;
            }

            name.Text = selectingBall.Name;
            rarity.Text = selectingBall.Rarity;
            size.Text = selectingBall.Size;
        }
    }
}
