using BouncingGame.Constants;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class PauseOverlay : Overlay
    {
        Button continueButton;
        Switch volumnButton;
        SpriteGameObject pauseBackground;
        SpriteGameObject guideBackground;
        Button homeButton;

        public bool IsGuide { get; set; } = false;

        public PauseOverlay():base()
        {
            pauseBackground = new SpriteGameObject("Sprites/Backgrounds/spr_pause", Depth.OverlayBackground);
            AddChild(pauseBackground);

            guideBackground = new SpriteGameObject("Sprites/Backgrounds/spr_guide", Depth.OverlayBackground);
            AddChild(guideBackground);

            continueButton = new Button("Sprites/Buttons/spr_continue", Depth.OverlayButton);
            AddChild(continueButton);
            continueButton.SetOriginToLeftCenter();
            continueButton.LocalPosition = new Vector2(20, 75);

            homeButton = new Button("Sprites/Buttons/spr_back_to_home", Depth.OverlayButton);
            AddChild(homeButton);
            homeButton.SetOriginToRightCenter();
            homeButton.LocalPosition = new Vector2(680, 75);

            volumnButton = new Switch("Sprites/Buttons/spr_volume@2", Depth.OverlayButton);
            AddChild(volumnButton);
            volumnButton.SetOriginToLeftCenter();
            volumnButton.LocalPosition = new Vector2(120, 75);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (continueButton.Pressed)
            {
                Hide();
            }

            if (volumnButton.Pressed)
            {
                // do something with volumn
            }

            if (homeButton.Pressed)
            {
                ExtendedGame.GameStateManager.SwitchTo(StateName.Home);
            }
        }

        public override void Show()
        {
            base.Show();

            if (IsGuide)
            {
                pauseBackground.Visible = false;
            }
            else
            {
                guideBackground.Visible = false;
            }
        }

    }
}
