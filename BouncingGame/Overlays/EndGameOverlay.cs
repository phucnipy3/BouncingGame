using BouncingGame.Constants;
using BouncingGame.GameStates;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class EndGameOverlay:Overlay
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
        private PlayState playState;

        public int Score { get; set; }
        public int HighScore { get; set; }
        public int TotalMoney { get; set; }
        public int AdditionMoney { get; set; }

        public EndGameOverlay(): base()
        {
            background = new Switch("Sprites/Backgrounds/spr_game_over", 0.75f);
            AddChild(background);

            getBallButton = new Switch("Sprites/Buttons/spr_btn_getball", 1);
            AddChild(getBallButton);
            getBallButton.LocalPosition = new Vector2(400, 800);

            rankButton = new Switch("Sprites/Buttons/spr_btn_rank", 1);
            AddChild(rankButton);
            rankButton.LocalPosition = new Vector2(600, 1000);

            replayButton = new Switch("Sprites/Buttons/spr_btn_replay", 1);
            AddChild(replayButton);
            replayButton.LocalPosition = new Vector2(300, 1000);

            shareButton = new Switch("Sprites/Buttons/spr_btn_share", 1);
            AddChild(shareButton);
            shareButton.LocalPosition = new Vector2(10, 1000);

            scoreText = new TextGameObject("Fonts/EndGameScore", 1, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            AddChild(scoreText);
            scoreText.LocalPosition = new Vector2(350, 215);

            highScoreText = new TextGameObject("Fonts/EndGameHighScore", 1, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            AddChild(highScoreText);
            highScoreText.LocalPosition = new Vector2(350, 360);

            additionMoneyText = new TextGameObject("Fonts/EndGameMoney", 1, Color.White, TextGameObject.HorizontalAlignment.Right, TextGameObject.VerticalAlignment.Center);
            AddChild(additionMoneyText);
            additionMoneyText.LocalPosition = new Vector2(640, 620);

            totalMoneyText = new TextGameObject("Fonts/EndGameMoney", 1, Color.White, TextGameObject.HorizontalAlignment.Left, TextGameObject.VerticalAlignment.Center);
            AddChild(totalMoneyText);
            totalMoneyText.LocalPosition = new Vector2(170, 620);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (getBallButton.Pressed)
            {
                // get ball
            }

            if (replayButton.Pressed)
            {
                ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
            }
        }

        public override void Show()
        {
            scoreText.Text = Score.ToString();
            highScoreText.Text = HighScore.ToString();
            totalMoneyText.Text = TotalMoney.ToString();
            additionMoneyText.Text = "+ " + AdditionMoney.ToString();

            base.Show();
        }
    }
}
