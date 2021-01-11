using BouncingGame.Constants;
using Engine;

namespace BouncingGame.Overlays
{
    public class WarningOverlay: Overlay
    {
        private SpriteGameObject background;

        public WarningOverlay() : base()
        {
            background = new SpriteGameObject("Sprites/Backgrounds/spr_warning", Depth.OverlayBackground2);
            AddChild(background);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonPressed())
            {
                Hide();
            }
        }
    }
}
