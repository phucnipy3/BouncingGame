using BouncingGame.Constants;
using BouncingGame.GameObjects;
using BouncingGame.Helpers;
using BouncingGame.Overlays;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameStates
{
    public class PlayState : GameState
    {
        Button pauseButton;
        Button guideButton;
        
        TextGameObject playMoney;
        TextGameObject playScore;
        TextGameObject playHighScore;
        private int tempMoney;
        private PauseOverlay pauseOverlay;
        private ContinueOverlay continueOverlay;
        private EndGameOverlay endGameOverlay;

        private ListStar fireWorkMaker;

        List<int> NumberBricks = new List<int>();
        bool canContinue = true;
        public PlayState()
        {
            for (int i = 0; i < Constant.NumberBrickRate.Length; i++)
            {
                NumberBricks.AddRange(Enumerable.Repeat<int>(i, Constant.NumberBrickRate[i]));
            }

            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_play", Depth.Backgroud));
            gameObjects.AddChild(ListBrick.Instance);
            gameObjects.AddChild(ListBall.Instance);
            gameObjects.AddChild(Director.Instance);
            gameObjects.AddChild(ListItemAddBall.Instance);
            gameObjects.AddChild(ListItemAddCoin.Instance);
            gameObjects.AddChild(ListItemClearColumn.Instance);
            gameObjects.AddChild(ListItemClearRow.Instance);
            gameObjects.AddChild(ListItemSpreadBall.Instance);

            pauseButton = new Button("Sprites/Buttons/spr_pause", Depth.Button);
            gameObjects.AddChild(pauseButton);
            pauseButton.SetOriginToLeftCenter();
            pauseButton.LocalPosition = new Vector2(20, 75);

            guideButton = new Button("Sprites/Buttons/spr_guide", Depth.Button);
            gameObjects.AddChild(guideButton);
            guideButton.SetOriginToLeftCenter();
            guideButton.LocalPosition = new Vector2(130, 75);

            

            

            playHighScore = new TextGameObject("Fonts/PlayHighScore", Depth.Button, Color.White, TextGameObject.HorizontalAlignment.Right, TextGameObject.VerticalAlignment.Top);
            gameObjects.AddChild(playHighScore);
            playHighScore.LocalPosition = new Vector2(680, 30);

            playMoney = new TextGameObject("Fonts/PlayMoney", Depth.Button, Color.White, TextGameObject.HorizontalAlignment.Left, TextGameObject.VerticalAlignment.Center);
            gameObjects.AddChild(playMoney);
            playMoney.LocalPosition = new Vector2(120, 1132);

            playScore = new TextGameObject("Fonts/PlayScore", Depth.Button, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Top);
            gameObjects.AddChild(playScore);
            playScore.LocalPosition = new Vector2(350, 20);

            pauseOverlay = new PauseOverlay();
            gameObjects.AddChild(pauseOverlay);

            continueOverlay = new ContinueOverlay(this);
            gameObjects.AddChild(continueOverlay);

            endGameOverlay = new EndGameOverlay();
            gameObjects.AddChild(endGameOverlay);


            fireWorkMaker = new ListStar(Depth.BallNumber + 0.001f);
            gameObjects.AddChild(fireWorkMaker);
            Reset();
        }

        public void EndGame()
        {
            SetupEndGame();
            canContinue = false;
        }

        public void Continue()
        {

            ClearDeadRows();
            canContinue = false;
        }

        public void GameOver()
        {
            if (canContinue)
            {
                //TODO: -10 coin, warning if not enough
                continueOverlay.Show();
            }
            else
            {
                SetupEndGame();
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

        public override void HandleInput(InputHelper inputHelper)
        {
            if (pauseOverlay.Visible)
            {
                pauseOverlay.HandleInput(inputHelper);
                return;
            }

            if (continueOverlay.Visible)
            {
                continueOverlay.HandleInput(inputHelper);
                return;
            }

            if (endGameOverlay.Visible)
            {
                endGameOverlay.HandleInput(inputHelper);
                return;
            }

            

            base.HandleInput(inputHelper);

            if (pauseButton.Pressed)
            {
                ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_click");
                pauseOverlay.IsGuide = false;
                pauseOverlay.Show();
            }

            if (guideButton.Pressed)
            {
                ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_click");
                pauseOverlay.IsGuide = true;
                pauseOverlay.Show();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (endGameOverlay.Visible)
            {
                endGameOverlay.Update(gameTime);
                return;
            }

            if (continueOverlay.Visible)
                return;

            if (pauseOverlay.Visible)
                return;
            
            playScore.Text = Level.ToString("N0");
            playHighScore.Text = "TOP " + GameSettingHelper.GetHighScore().ToString("N0");
            playMoney.Text = GameSettingHelper.GetMoney().ToString("N0");
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

        private void SetupEndGame()
        {

            endGameOverlay.AdditionMoney = Level;
            endGameOverlay.Score = Level;
            endGameOverlay.HighScore = GameSettingHelper.GetHighScore();
            tempMoney = GameSettingHelper.GetMoney();
            endGameOverlay.TotalMoney = tempMoney;
            GameSettingHelper.SetMoney(tempMoney + Level);

            endGameOverlay.Show();
        }

        public override void Reset()
        {
            base.Reset();
            canContinue = true;
            Level = 0;
            NextLevel();
        }

        public void CreateFireWork(Vector2 startLocation)
        {
            fireWorkMaker.CreateFireWork(ExtendedGame.Random.Next(40, 60), startLocation);
        }

        public int Level { get; private set; }

    }
}
