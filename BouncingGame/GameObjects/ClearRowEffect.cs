using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class ClearRowEffect : AnimatedGameObject
    {
        public ClearRowEffect(int row) : base(1)
        {
            LoadAnimation("Sprites/Animations/", "row", false, 0.5f);
            PlayAnimation("row", true);
            SetOriginToCenter();
            LocalPosition = new Vector2(350, 200 + row * 100);
        }
    }
}
