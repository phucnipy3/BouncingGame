using BouncingGame.Constants;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class ConfirmOverlay : Overlay
    {
        private SpriteGameObject background;
        private Button cancelButton;
        private Button continueButton;
        private GetBallOverlay getBallOverlay;

        public ConfirmOverlay(GetBallOverlay getBallOverlay): base()
        {
            this.getBallOverlay = getBallOverlay;

            background = new SpriteGameObject("Sprites/Backgrounds/spr_confirm", Depth.OverlayBackground2);
            AddChild(background);

            cancelButton = new Button("Sprites/Buttons/spr_btn_cancel", Depth.OverlayButton2);
            cancelButton.LocalPosition = new Vector2(100, 675);
            AddChild(cancelButton);

            continueButton = new Button("Sprites/Buttons/spr_btn_continue", Depth.OverlayButton2);
            continueButton.LocalPosition = new Vector2(450, 675);
            AddChild(continueButton);

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (cancelButton.Pressed)
            {
                Hide();
            }

            if (continueButton.Pressed)
            {
                Hide();
                // - coin
                getBallOverlay.Show();
            }
        }

    }
}
