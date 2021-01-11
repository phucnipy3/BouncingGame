using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncingGame.GameObjects
{
    public class Block
    {
        public BlockType Type { get; set; }
        public int Column { get; set; }
        public BrickType BrickType { get; set; }
    }

    public enum BlockType
    {
        Brick = 0,
        ItemAddBall = 1,
        ItemClearRow = 2,
        ItemClearColumn = 3,
        ItemSpreadBall = 4,
        ItemAddCoin = 5,
        FirstRandomItem = 2,
        LastRandomItem = 5
    }

    public enum BrickType
    {
        Square = 0,
        Triangle1 = 1,
        Triangle2 = 2,
        Triangle3 = 3,
        Triangle4 = 4,
        Special = 5,
        FirstTriangle = 1,
        LastTriangle = 4,
    }

}
