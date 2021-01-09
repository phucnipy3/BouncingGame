using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class ClearColumnEffect : AnimatedGameObject
    {
        public ClearColumnEffect(int col) : base(1)
        {
            LoadAnimation("Sprites/Animations/spr_animation_item_break_vertical@10", "col", false, 0.01f);
            PlayAnimation("col", true);
            SetOriginToCenter();
            LocalPosition = new Vector2(50 + col * 100 , 600);
        }
    }
}
