using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class ClearColumnEffect : AnimatedGameObject
    {
        public ClearColumnEffect(int col) : base(1)
        {
            LoadAnimation("Sprites/Animations/", "col", false, 0.5f);
            PlayAnimation("col", true);
            SetOriginToCenter();
            LocalPosition = new Vector2(50 + col * 100 , 600);
        }
    }
}
