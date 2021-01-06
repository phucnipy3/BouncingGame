namespace BouncingGame.Models
{
    public class BallModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rarity { get; set; }
        public string Size { get; set; }
        public string OriginSpritePath { get; set; }
        public string LargeSpritePath { get; set; }
        public string ShadowSpritePath { get; set; }
        public bool Locked { get; set; }

    }
}
