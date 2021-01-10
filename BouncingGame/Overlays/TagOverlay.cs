using BouncingGame.Constants;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class TagOverlay : Overlay
    {
        private SpriteGameObject background;
        private Button backButton;

        public TagOverlay() : base()
        {
            background = new SpriteGameObject("Sprites/Backgrounds/spr_tag", Depth.OverlayBackground2);
            AddChild(background);

            backButton = new Button("Sprites/Buttons/spr_btn_back", Depth.OverlayButton2);
            backButton.LocalPosition = new Vector2(20, 100);
            AddChild(backButton);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (backButton.Pressed)
                Hide();
        }
    }
}
