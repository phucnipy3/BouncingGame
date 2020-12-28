using BouncingGame.Constants;
using BouncingGame.GameObjects;
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
        Ball ball = new Ball();
        bool gameOver = false;
        Button pauseButton;
        Button continueButton;
        Button guideButton;
        Button homeButton;
        Button volumnButton;
        SpriteGameObject pauseBackground;
        SpriteGameObject guideBackground;
        bool pause = false;
        List<int> NumberBricks = new List<int>();
        public PlayState()
        {
            for(int i = 0; i< Constant.NumberBrickRate.Length; i++)
            {
                NumberBricks.AddRange(Enumerable.Repeat<int>(i, Constant.NumberBrickRate[i]));
            }

            gameObjects.AddChild(new SpriteGameObject("Sprites/Backgrounds/spr_home", 0));
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
            pauseButton.LocalPosition = new Vector2(10, 10);
            guideButton = new Button("Sprites/Buttons/spr_guide", 0);
            gameObjects.AddChild(guideButton);
            guideButton.LocalPosition = new Vector2(100, 10);

            pauseBackground = new SpriteGameObject("Sprites/Backgrounds/spr_pause", 0.75f);
            pauseBackground.Visible = false;
            gameObjects.AddChild(pauseBackground);
            guideBackground = new SpriteGameObject("Sprites/Backgrounds/spr_guide", 0.75f);
            guideBackground.Visible = false;
            gameObjects.AddChild(guideBackground);

            continueButton = new Button("Sprites/Buttons/spr_continue", 1);
            gameObjects.AddChild(continueButton);
            continueButton.LocalPosition = new Vector2(10, 10);
            continueButton.Visible = false;
            homeButton = new Button("Sprites/Buttons/spr_back_to_home", 1);
            gameObjects.AddChild(homeButton);
            homeButton.LocalPosition = new Vector2(600, 10);
            homeButton.Visible = false; 
            volumnButton = new Button("Sprites/Buttons/spr_mute", 1);
            gameObjects.AddChild(volumnButton);
            volumnButton.LocalPosition = new Vector2(100, 10);
            volumnButton.Visible = false;
            Reset();
        }

        public void GameOver()
        {
            gameOver = true;
        }

        public void NextLevel()
        {
            Level++;
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

                if(brickCount > positions.Count)
                {
                    brickCount = positions.Count;
                }

                for(int i = 0; i< brickCount; i++)
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

            foreach(var leftPosition in positions)
            {
                bool isItem = ExtendedGame.Random.NextDouble() <= Constant.ItemRate;
                if (isItem && (existingItems.Count < (int)BlockType.LastRandomItem - (int)BlockType.FirstRandomItem + 1))
                {
                    var type = (BlockType)ExtendedGame.Random.Next((int)BlockType.FirstRandomItem, (int)BlockType.LastRandomItem +1);
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
                Reset();
            }

            if (pause)
            {
                if (continueButton.Pressed)
                {
                    HideOverlay();
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

            base.Update(gameTime);


        }

        private void HideOverlay()
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
            pause = false;
            HideOverlay();
            Level = 0;
            NextLevel();
        }

        public int Level { get; private set; }

    }
}
