using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class ListBrick: GameObjectList
    {
        private static ListBrick instance = new ListBrick();
        public static ListBrick Instance
        {
            get
            {
                return instance;
            }
        }
        private List<Brick> bricks
        {
            get
            {
                return children.Select(x => (Brick)x).ToList();
            }
        }

        public int Level { get; set; }
        private ListBrick()
        {
            Reset();
        }

        public void NextLevel(List<Block> bricks)
        {
            Level++;
            AddNewBricks(bricks);
            MoveDown();
        }

        private void MoveDown()
        {
            foreach(var brick in bricks)
            {
                brick.MoveDown();
            }
        }

        private void AddNewBricks(List<Block> bricks)
        {
            foreach(var brick in bricks)
            {
                int durability = Level;
                AddChild(new Brick(durability, (int)brick.BrickType % 5, brick.Column, brick.BrickType == BrickType.Special));
            }
        }

        public override void Reset()
        {
            Clear();
            bricks.Clear();
            Level = 0;
        }

        public List<Vector2> GetNormalVectorsWhenTouchBall(Ball ball)
        {
            List<Vector2> normals = new List<Vector2>();
            foreach(var brick in bricks)
            {
                Vector2 normal;
                if(brick.CheckCollisionWithBall(ball, out normal))
                {
                    normals.Add(normal);
                    brick.Touched();
                    ball.AddTouchedBrick(brick);
                }
            }

            return normals;
        }

        public override void Update(GameTime gameTime)
        {
            RemoveChild(bricks.Where(x => !x.Visible).Select(x => x));
            base.Update(gameTime);
        }

        public void ClearRow(int row)
        {
            foreach (var brick in bricks)
            {
                if(brick.Row == row)
                {
                    brick.Touched();
                }
            }
        }

        public void ClearColumn(int column)
        {
            foreach (var brick in bricks)
            {
                if (brick.Column == column)
                {
                    brick.Touched();
                }
            }
        }

        public void ClearDeadRows()
        {
            foreach (var brick in bricks)
            {
                if (brick.Row > 6)
                    brick.Visible = false;
            }
        }
    }
}
