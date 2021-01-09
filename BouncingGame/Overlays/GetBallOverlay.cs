using BouncingGame.GameObjects;
using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class GetBallOverlay : Overlay
    {
        private SpriteGameObject background;
        private AnimatedGameObject giftBox;
        private AnimatedGameObject glowing;

        public GetBallOverlay()
        {
            giftBox = new AnimatedGameObject(0.81f);
            giftBox.LoadAnimation("Sprites/Animations/spr_animation_increase_one@15", "increase", false, 0.05f);
            giftBox.PlayAnimation("increase", true);
            giftBox.SetOriginToCenter();
            giftBox.LocalPosition = new Vector2(350, 600);
            AddChild(giftBox);

            glowing = new AnimatedGameObject(0.81f);
            glowing.LoadAnimation("Sprites/Animations/spr_animation_increase_one@15", "increase", false, 0.05f);
            glowing.PlayAnimation("increase", true);
            glowing.SetOriginToCenter();
            glowing.LocalPosition = new Vector2(350, 600);
            AddChild(glowing);
        }


    }
}
