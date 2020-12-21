using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomLevel
{
    public enum Type
    {
        None = 0,
        SquareBlock = 1,
        TriangleBlock = 2,
        SpecialBlock = 3,
        AddBallItem = 4,
        SpreadBallItem = 5,
        BreakHorizontalItem = 6,
        BreakVerticalItem = 7,
        AddCoinItem = 8
    }

    public class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            int n = 1;

            Console.WriteLine("SquareBlock = 1\nTriangleBlock = 2\nSpecialBlock = 3\nAddBallItem = 4\nSpreadBallItem = 5\nBreakHorizontalItem = 6\nBreakVerticalItem = 7\nAddCoinItem = 8");
            Console.WriteLine("Esc to exit. Press any key to view more...");
            Console.WriteLine();
            Console.WriteLine("Level\tCol1\tCol2\tCol3\tCol4\tCol5\tCol6\tCol7");

            while (true)
            {
                Console.Write(n + "\t");
                foreach (Type type in GetTypes(n))
                    if (type == 0)
                        Console.Write("\t");
                    else Console.Write((int)type + "\t");
                Console.WriteLine();
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;
                n++;
            }
        }

        private static List<Type> GetTypes(int level)
        {
            var types = new Type[7];
            List<int> pos = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
            int randPos = random.Next(pos.Count);

            types[pos[randPos]] = Type.AddBallItem;
            pos.RemoveAt(randPos);

            if (level % 10 == 0)
            {
                int numberSpecial = random.Next(1, 6);
                for (int i = 0; i < numberSpecial; i++)
                {
                    randPos = random.Next(pos.Count);
                    types[pos[randPos]] = Type.SpecialBlock;
                    pos.RemoveAt(randPos);
                }
            }

            List<Type> items = new List<Type>() { Type.AddCoinItem, Type.SpreadBallItem, Type.BreakHorizontalItem, Type.BreakVerticalItem };
            while (pos.Count > 0)
            {
                randPos = random.Next(pos.Count);
                int type = random.Next() % 10;
                if (type < 6)
                    types[pos[randPos]] = random.Next() % 5 == 0 ? Type.TriangleBlock : Type.SquareBlock;
                if (type == 6 && items.Count > 0)
                {
                    int randItem = random.Next(items.Count);
                    types[pos[randPos]] = items[randItem];
                    items.RemoveAt(randItem);
                }
                pos.RemoveAt(randPos);
            }
            return types.ToList();
        }
    }
}
