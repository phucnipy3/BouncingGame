using BouncingGame.Constants;
using BouncingGame.GameStates;
using BouncingGame.Helpers;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class EndGameOverlay : Overlay
    {
        private SpriteGameObject background;
        private Button getBallButton;
        private Button rankButton;
        private Button replayButton;
        private Button shareButton;
        private TextGameObject scoreText;
        private TextGameObject highScoreText;
        private TextGameObject totalMoneyText;
        private TextGameObject additionMoneyText;
        private Button tagButton;
        private PlayState playState;
        private GetBallOverlay getBallOverlay;
        private ConfirmOverlay confirmOverlay;

        public int Score { get; set; }
        public int HighScore { get; set; }
        public int TotalMoney { get; set; }
        public int AdditionMoney { get; set; }

        public EndGameOverlay() : base()
        {
            background = new Switch("Sprites/Backgrounds/spr_game_over", Depth.OverlayBackground);
            AddChild(background);

            tagButton = new Switch("Sprites/Buttons/spr_btn_tag", Depth.OverlayButton);
            AddChild(tagButton);
            tagButton.SetOriginToLeftTop();
            tagButton.LocalPosition = new Vector2(20, 120);

            getBallButton = new Switch("Sprites/Buttons/spr_btn_getball", Depth.OverlayButton);
            AddChild(getBallButton);
            getBallButton.LocalPosition = new Vector2(400, 770);

            rankButton = new Switch("Sprites/Buttons/spr_btn_rank", Depth.OverlayButton);
            AddChild(rankButton);
            rankButton.SetOriginToLeftBottom();
            rankButton.LocalPosition = new Vector2(30, 1100);

            replayButton = new Switch("Sprites/Buttons/spr_btn_replay", Depth.OverlayButton);
            replayButton.SetOriginToCenterBottom();
            AddChild(replayButton);
            replayButton.LocalPosition = new Vector2(350, 1100);

            shareButton = new Switch("Sprites/Buttons/spr_btn_share", Depth.OverlayButton);
            AddChild(shareButton);
            shareButton.SetOriginToRightBottom();
            shareButton.LocalPosition = new Vector2(670, 1100);

            scoreText = new TextGameObject("Fonts/EndGameScore", Depth.OverlayButton, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            AddChild(scoreText);
            scoreText.LocalPosition = new Vector2(350, 252);

            highScoreText = new TextGameObject("Fonts/EndGameHighScore", Depth.OverlayButton, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            AddChild(highScoreText);
            highScoreText.LocalPosition = new Vector2(350, 400);

            additionMoneyText = new TextGameObject("Fonts/EndGameMoney", Depth.OverlayButton, Color.White, TextGameObject.HorizontalAlignment.Right, TextGameObject.VerticalAlignment.Center);
            AddChild(additionMoneyText);
            additionMoneyText.LocalPosition = new Vector2(640, 620);

            totalMoneyText = new TextGameObject("Fonts/EndGameMoney", Depth.OverlayButton, Color.White, TextGameObject.HorizontalAlignment.Left, TextGameObject.VerticalAlignment.Center);
            AddChild(totalMoneyText);
            totalMoneyText.LocalPosition = new Vector2(150, 620);

            getBallOverlay = new GetBallOverlay();
            AddChild(getBallOverlay);

            confirmOverlay = new ConfirmOverlay(getBallOverlay);
            AddChild(confirmOverlay);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (getBallOverlay.Visible)
            {
                getBallOverlay.HandleInput(inputHelper);
                return;
            }

            if (confirmOverlay.Visible)
            {
                confirmOverlay.HandleInput(inputHelper);
                return;
            }

            base.HandleInput(inputHelper);

            if (getBallButton.Pressed)
            {
                confirmOverlay.Show();
            }

            if (replayButton.Pressed)
            {
                ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
            }
        }

        public override void Show()
        {
            base.Show();

            getBallOverlay.Visible = false;
            confirmOverlay.Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            scoreText.Text = Score.ToString("N0");
            highScoreText.Text = HighScore.ToString("N0");
            int realtimeMoney = GameSettingHelper.GetMoney();
            if (TotalMoney != realtimeMoney - AdditionMoney)
            {
                totalMoneyText.Text = GameSettingHelper.GetMoney().ToString("N0");
                additionMoneyText.Visible = false;
            }
            else
            {
                totalMoneyText.Text = TotalMoney.ToString("N0");
                additionMoneyText.Text = "+ " + AdditionMoney.ToString("N0");
            }
        }
    }
}
