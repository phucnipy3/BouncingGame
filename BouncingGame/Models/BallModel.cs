using Microsoft.Xna.Framework;

namespace BouncingGame.Models
{
    public class BallModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rarity { get; set; }
        public Color Color
        {
            get 
            {
                switch (Rarity)
                {
                    case Constants.Rarity.Normal: return Color.White;
                    case Constants.Rarity.Rare: return new Color(10, 71, 154);
                    case Constants.Rarity.Epic: return new Color(104, 19, 185);
                    case Constants.Rarity.Unique: return new Color(231, 207, 45);
                    case Constants.Rarity.Legendary: return new Color(240, 32, 32);
                    default:
                        return Color.White;
                }
            } 
        }
        public string Size { get; set; }
        public string OriginSpritePath { get; set; }
        public string LargeSpritePath { get; set; }
        public string ShadowSpritePath { get; set; }
        public bool Locked { get; set; }

    }
}
