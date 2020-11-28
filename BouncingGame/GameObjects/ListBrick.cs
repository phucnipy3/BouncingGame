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

        
        private int level = 10;
        private ListBrick()
        {
            Reset();
        }

        public void NextLevel()
        {
            level++;
            AddNewBricks();
            MoveDown();
        }

        private void MoveDown()
        {
            foreach(var brick in bricks)
            {
                brick.MoveDown();
            }
        }

        private void AddNewBricks()
        {
            int durability = level * 10;
            AddChild(new Brick(durability, new Vector2(0, 150)));
            AddChild(new Brick(durability, new Vector2(100, 150)));
            AddChild(new Brick(durability, new Vector2(200, 150)));
            AddChild(new Brick(durability, new Vector2(300, 150)));
            AddChild(new Brick(durability, new Vector2(600, 150)));
        }

        public override void Reset()
        {
            Clear();
            bricks.Clear();
            level = 0;
            NextLevel();
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
    }
}
