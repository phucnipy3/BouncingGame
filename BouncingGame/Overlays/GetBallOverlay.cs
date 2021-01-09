using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class GetBallOverlay : Overlay
    {
        private SpriteGameObject background;
        private AnimatedGameObject giftBox;
        private AnimatedGameObject glowing;

        public GetBallOverlay(): base()
        {
            giftBox = new AnimatedGameObject(0.81f);
            giftBox.LoadAnimation("Sprites/Animations/spr_animation_getball@4x3", "box", false, 0.05f);
            giftBox.PlayAnimation("box", true);
            giftBox.SetOriginToCenter();
            giftBox.LocalPosition = new Vector2(350, 600);
            AddChild(giftBox);

            glowing = new AnimatedGameObject(0.8f);
            glowing.LoadAnimation("Sprites/Animations/spr_animation_glowing@5", "glowing", true, 0.1f);
            glowing.PlayAnimation("glowing", true);
            glowing.SetOriginToCenter();
            glowing.LocalPosition = new Vector2(350, 600);
            AddChild(glowing);

            background = new SpriteGameObject("Sprites/Backgrounds/spr_getball", 0.75f);
            AddChild(background);
        }

        public override void Show()
        {
            base.Show();
            giftBox.PlayAnimation("box", true);
        }
    }
}
