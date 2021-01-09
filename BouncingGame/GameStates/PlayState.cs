using BouncingGame.Constants;
using BouncingGame.GameObjects;
using BouncingGame.Helpers;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameStates
{
    public class PlayState : GameState
    {
        bool gameOver = false;
        Button pauseButton;
        Button continueButton;
        Button guideButton;
        Button homeButton;
        Switch volumnButton;
        SpriteGameObject pauseBackground;
        SpriteGameObject guideBackground;
        SpriteGameObject continueBackground;
        Button oneMoreButton;
        Button endGameButton;
        SpriteGameObject gameOverBackground;
        Button getBallButton;
        Button rankButton;
        Button replayButton;
        Button shareButton;
        TextGameObject endGameScore;
        TextGameObject endGameHighScore;
        TextGameObject endGameTotalMoney;
        TextGameObject endGameAdditionMoney;
        TextGameObject playMoney;
        TextGameObject playScore;
        TextGameObject playHighScore;
        private int tempMoney;

        bool pause = false;
        List<int> NumberBricks = new List<int>();
        bool canContinue = true;
        public PlayState()
        {
            for (int i = 0; i < Constant.NumberBrickRate.Length; i++)
            {
                NumberBricks.AddRange(Enumerable.Repeat<int>(i, Constant.NumberBrickRate[i]));
            }

            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_play", 0));
            gameObjects.AddChild(ListBrick.Instance);
            gameObjects.AddChild(ListBall.Instance);
            gameObjects.AddChild(Director.Instance);
            gameObjects.AddChild(ListItemAddBall.Instance);
            gameObjects.AddChild(ListItemAddCoin.Instance);
            gameObjects.AddChild(ListItemClearColumn.Instance);
            gameObjects.AddChild(ListItemClearRow.Instance);
            gameObjects.AddChild(ListItemSpreadBall.Instance);

            pauseButton = new Button("Sprites/Buttons/spr_pause", 0);
            gameObjects.AddChild(pauseButton);
            pauseButton.SetOriginToLeftCenter();
            pauseButton.LocalPosition = new Vector2(20, 75);

            guideButton = new Button("Sprites/Buttons/spr_guide", 0);
            gameObjects.AddChild(guideButton);
            guideButton.SetOriginToLeftCenter();
            guideButton.LocalPosition = new Vector2(120, 75);

            pauseBackground = new SpriteGameObject("Sprites/Backgrounds/spr_pause", 0.75f);
            pauseBackground.Visible = false;
            gameObjects.AddChild(pauseBackground);

            guideBackground = new SpriteGameObject("Sprites/Backgrounds/spr_guide", 0.75f);
            guideBackground.Visible = false;
            gameObjects.AddChild(guideBackground);

            continueButton = new Button("Sprites/Buttons/spr_continue", 1);
            gameObjects.AddChild(continueButton);
            continueButton.SetOriginToLeftCenter();
            continueButton.LocalPosition = new Vector2(20, 75);
            continueButton.Visible = false;

            homeButton = new Button("Sprites/Buttons/spr_back_to_home", 1);
            gameObjects.AddChild(homeButton);
            homeButton.SetOriginToRightCenter();
            homeButton.LocalPosition = new Vector2(680, 75);
            homeButton.Visible = false;

            volumnButton = new Switch("Sprites/Buttons/spr_volume@2", 1);
            gameObjects.AddChild(volumnButton);
            volumnButton.SetOriginToLeftCenter();
            volumnButton.LocalPosition = new Vector2(120, 75);
            volumnButton.Visible = false;

            continueBackground = new Switch("Sprites/Backgrounds/spr_continue", 0.75f);
            gameObjects.AddChild(continueBackground);
            continueBackground.Visible = false;

            oneMoreButton = new Switch("Sprites/Buttons/spr_btn_onemore", 1);
            gameObjects.AddChild(oneMoreButton);
            oneMoreButton.LocalPosition = new Vector2(100, 400);
            oneMoreButton.Visible = false;

            endGameButton = new Switch("Sprites/Buttons/spr_btn_endgame", 1);
            gameObjects.AddChild(endGameButton);
            endGameButton.LocalPosition = new Vector2(400, 400);
            endGameButton.Visible = false;

            gameOverBackground = new Switch("Sprites/Backgrounds/spr_game_over", 0.75f);
            gameObjects.AddChild(gameOverBackground);
            gameOverBackground.Visible = false;

            getBallButton = new Switch("Sprites/Buttons/spr_btn_getball", 1);
            gameObjects.AddChild(getBallButton);
            getBallButton.LocalPosition = new Vector2(400, 800);
            getBallButton.Visible = false;

            rankButton = new Switch("Sprites/Buttons/spr_btn_rank", 1);
            gameObjects.AddChild(rankButton);
            rankButton.LocalPosition = new Vector2(600, 1000);
            rankButton.Visible = false;

            replayButton = new Switch("Sprites/Buttons/spr_btn_replay", 1);
            gameObjects.AddChild(replayButton);
            replayButton.LocalPosition = new Vector2(300, 1000);
            replayButton.Visible = false;

            shareButton = new Switch("Sprites/Buttons/spr_btn_share", 1);
            gameObjects.AddChild(shareButton);
            shareButton.LocalPosition = new Vector2(10, 1000);
            shareButton.Visible = false;

            endGameScore = new TextGameObject("Fonts/EndGameScore", 1, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(endGameScore);
            endGameScore.LocalPosition = new Vector2(350, 215);
            endGameScore.Visible = false;

            endGameHighScore = new TextGameObject("Fonts/EndGameHighScore", 1, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(endGameHighScore);
            endGameHighScore.LocalPosition = new Vector2(350, 360);
            endGameHighScore.Visible = false;

            endGameAdditionMoney = new TextGameObject("Fonts/EndGameMoney", 1, Color.White, TextGameObject.HorizontalAlignment.Right, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(endGameAdditionMoney);
            endGameAdditionMoney.LocalPosition = new Vector2(640, 620);
            endGameAdditionMoney.Visible = false;

            endGameTotalMoney = new TextGameObject("Fonts/EndGameMoney", 1, Color.White, TextGameObject.HorizontalAlignment.Left, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(endGameTotalMoney);
            endGameTotalMoney.LocalPosition = new Vector2(170, 620);
            endGameTotalMoney.Visible = false;

            playScore = new TextGameObject("Fonts/PlayScore", 0, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(playScore);
            playScore.LocalPosition = new Vector2(350, 75);

            playHighScore = new TextGameObject("Fonts/PlayHighScore", 0, Color.White, TextGameObject.HorizontalAlignment.Right, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(playHighScore);
            playHighScore.LocalPosition = new Vector2(690, 75);

            playMoney = new TextGameObject("Fonts/PlayMoney", 0, Color.White, TextGameObject.HorizontalAlignment.Left, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(playMoney);
            playMoney.LocalPosition = new Vector2(100, 1130);

            Reset();
        }

        public void GameOver()
        {
            gameOver = true;
            tempMoney = GameSettingHelper.GetMoney();
            GameSettingHelper.SetMoney(tempMoney + Level);
            if (canContinue)
            {
                ShowContinueOverlay();
            }
            else
            {
                ShowGameOverOverlay();
            }
        }

        public void NextLevel()
        {
            Level++;
            if(Level > GameSettingHelper.GetHighScore())
            {
                GameSettingHelper.SetHighScore(Level);
            }

            var blocks = GenerateNewBlocks();
            ListBrick.Instance.NextLevel(blocks.Where(x => x.Type == BlockType.Brick).ToList());
            ListItemAddBall.Instance.AddItems(blocks.Where(x => x.Type == BlockType.ItemAddBall).Select(x => x.Column));
            ListItemAddBall.Instance.MoveDown();
            ListItemAddCoin.Instance.AddItems(blocks.Where(x => x.Type == BlockType.ItemAddCoin).Select(x => x.Column));
            ListItemAddCoin.Instance.MoveDown();
            ListItemClearColumn.Instance.AddItems(blocks.Where(x => x.Type == BlockType.ItemClearColumn).Select(x => x.Column));
            ListItemClearColumn.Instance.MoveDown();
            ListItemClearRow.Instance.AddItems(blocks.Where(x => x.Type == BlockType.ItemClearRow).Select(x => x.Column));
            ListItemClearRow.Instance.MoveDown();
            ListItemSpreadBall.Instance.AddItems(blocks.Where(x => x.Type == BlockType.ItemSpreadBall).Select(x => x.Column));
            ListItemSpreadBall.Instance.MoveDown();
        }

        private List<Block> GenerateNewBlocks()
        {
            List<Block> blocks = new List<Block>();
            List<int> positions = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
            int addBallPositionIndex = ExtendedGame.Random.Next(positions.Count);
            blocks.Add(new Block
            {
                Type = BlockType.ItemAddBall,
                Column = positions[addBallPositionIndex]
            });
            positions.RemoveAt(addBallPositionIndex);

            if (Level % 10 == 0)
            {
                int numberSpecial = NumberBricks[ExtendedGame.Random.Next(0, NumberBricks.Count)];
                for (int i = 0; i < numberSpecial; i++)
                {
                    int speacialBrickPositionIndex = ExtendedGame.Random.Next(positions.Count);
                    blocks.Add(new Block
                    {
                        Type = BlockType.Brick,
                        Column = positions[speacialBrickPositionIndex],
                        BrickType = BrickType.Special
                    });
                    positions.RemoveAt(speacialBrickPositionIndex);
                }
            }

            if (positions.Count > 0)
            {
                int brickCount = NumberBricks[ExtendedGame.Random.Next(0, NumberBricks.Count)];

                if (brickCount > positions.Count)
                {
                    brickCount = positions.Count;
                }

                for (int i = 0; i < brickCount; i++)
                {
                    int brickPositionIndex = ExtendedGame.Random.Next(positions.Count);

                    bool isSquare = ExtendedGame.Random.NextDouble() <= Constant.SquareRate;
                    blocks.Add(new Block
                    {
                        Type = BlockType.Brick,
                        Column = positions[brickPositionIndex],
                        BrickType = isSquare ? BrickType.Square : (BrickType)ExtendedGame.Random.Next((int)BrickType.Triangle1, (int)BrickType.Triangle4 + 1)
                    });

                    positions.RemoveAt(brickPositionIndex);
                }
            }

            List<BlockType> existingItems = new List<BlockType>();

            foreach (var leftPosition in positions)
            {
                bool isItem = ExtendedGame.Random.NextDouble() <= Constant.ItemRate;
                if (isItem && (existingItems.Count < (int)BlockType.LastRandomItem - (int)BlockType.FirstRandomItem + 1))
                {
                    var type = (BlockType)ExtendedGame.Random.Next((int)BlockType.FirstRandomItem, (int)BlockType.LastRandomItem + 1);
                    while (existingItems.Contains(type))
                    {
                        type = (BlockType)ExtendedGame.Random.Next((int)BlockType.FirstRandomItem, (int)BlockType.LastRandomItem + 1);
                    }
                    existingItems.Add(type);
                    blocks.Add(new Block
                    {
                        Type = type,
                        Column = leftPosition
                    });
                }
            }

            return blocks;
        }

        public override void Update(GameTime gameTime)
        {
            if (gameOver)
            {
                endGameAdditionMoney.Text = "+ " + Level.ToString();
                endGameHighScore.Text = GameSettingHelper.GetHighScore().ToString();
                endGameScore.Text = Level.ToString();
                endGameTotalMoney.Text = tempMoney.ToString();
                if (canContinue)
                {
                    if (oneMoreButton.Pressed)
                    {
                        HideContinueOverlay();
                        ClearDeadRows();
                        gameOver = false;
                        canContinue = false;
                    }

                    if (endGameButton.Pressed)
                    {
                        HideContinueOverlay();
                        ShowGameOverOverlay();
                        canContinue = false;
                    }
                }
                else
                {
                    if (getBallButton.Pressed)
                    {
                        // get ball
                    }

                    if (replayButton.Pressed)
                    {
                        ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
                    }

                }

                return;
            }

            if (pause)
            {
                if (continueButton.Pressed)
                {
                    HidePauseOverlay();
                    pause = false;
                }

                if (volumnButton.Pressed)
                {
                    // do something with volumn
                }

                if (homeButton.Pressed)
                {
                    ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
                    pause = false;
                }

                return;
            }

            if (pauseButton.Pressed)
            {
                ShowPauseOverlay();
                pause = true;

            }

            if (guideButton.Pressed)
            {
                ShowGuideOverlay();
                pause = true;
            }

            playScore.Text = Level.ToString();
            playHighScore.Text = GameSettingHelper.GetHighScore().ToString();
            playMoney.Text = GameSettingHelper.GetMoney().ToString();
            base.Update(gameTime);


        }

        private void ClearDeadRows()
        {
            ListBrick.Instance.ClearDeadRows();
            ListItemAddBall.Instance.ClearDeadRows();
            ListItemAddCoin.Instance.ClearDeadRows();
            ListItemClearColumn.Instance.ClearDeadRows();
            ListItemClearRow.Instance.ClearDeadRows();
            ListItemSpreadBall.Instance.ClearDeadRows();
        }

        private void HideGameOverOverlay()
        {
            gameOverBackground.Visible = false;
            getBallButton.Visible = false;
            rankButton.Visible = false;
            replayButton.Visible = false;
            shareButton.Visible = false;
            endGameAdditionMoney.Visible = false;
            endGameHighScore.Visible = false;
            endGameScore.Visible = false;
            endGameTotalMoney.Visible = false;
        }

        private void ShowGameOverOverlay()
        {
            gameOverBackground.Visible = true;
            getBallButton.Visible = true;
            rankButton.Visible = true;
            replayButton.Visible = true;
            shareButton.Visible = true;
            endGameAdditionMoney.Visible = true;
            endGameHighScore.Visible = true;
            endGameScore.Visible = true;
            endGameTotalMoney.Visible = true;
        }

        private void HideContinueOverlay()
        {
            continueBackground.Visible = false;
            oneMoreButton.Visible = false;
            endGameButton.Visible = false;
        }

        private void ShowContinueOverlay()
        {
            continueBackground.Visible = true;
            oneMoreButton.Visible = true;
            endGameButton.Visible = true;
        }

        private void HidePauseOverlay()
        {
            guideBackground.Visible = false;
            pauseBackground.Visible = false;
            homeButton.Visible = false;
            continueButton.Visible = false;
            volumnButton.Visible = false;
        }

        private void ShowGuideOverlay()
        {
            guideBackground.Visible = true;
            homeButton.Visible = true;
            continueButton.Visible = true;
            volumnButton.Visible = true;
        }

        private void ShowPauseOverlay()
        {
            pauseBackground.Visible = true;
            homeButton.Visible = true;
            continueButton.Visible = true;
            volumnButton.Visible = true;
        }

        public override void Reset()
        {
            base.Reset();
            gameOver = false;
            canContinue = true;
            pause = false;
            HidePauseOverlay();
            HideContinueOverlay();
            HideGameOverOverlay();
            Level = 0;
            NextLevel();
        }

        public int Level { get; private set; }

    }
}
