using Engine;
using Microsoft.Xna.Framework;
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

        private List<Brick> bricks;
        public List<Brick> Bricks
        {
            get
            {
                return bricks;
            }
        }
        private int level = 10;
        private ListBrick()
        {
            bricks = new List<Brick>();
        }

        public void NextLevel()
        {
            level++;
            AddNewBricks();
        }

        private void AddNewBricks()
        {
            int durability = level * 10;
            bricks.Add(new Brick(durability, new Vector2(3, 250 + 3)));
            AddChild(bricks.Last());
            bricks.Add(new Brick(durability, new Vector2(100 + 3, 250 + 3)));
            AddChild(bricks.Last());
            bricks.Add(new Brick(durability, new Vector2(200 + 3, 250 + 3)));
            AddChild(bricks.Last());
            bricks.Add(new Brick(durability, new Vector2(300 + 3, 250 + 3)));
            AddChild(bricks.Last());
            bricks.Add(new Brick(durability, new Vector2(600 + 3, 250 + 3)));
            AddChild(bricks.Last());
        }
    }
}
