using BouncingGame.Constants;
using BouncingGame.GameStates;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class ContinueOverlay : Overlay
    {
        private SpriteGameObject continueBackground;
        private Button oneMoreButton;
        private Button endGameButton;
        private PlayState playState;

        public ContinueOverlay(PlayState playState):base()
        {
            continueBackground = new Switch("Sprites/Backgrounds/spr_continue", Depth.OverlayBackground2);
            AddChild(continueBackground);

            oneMoreButton = new Switch("Sprites/Buttons/spr_btn_onemore", Depth.OverlayButton2);
            AddChild(oneMoreButton);
            oneMoreButton.SetOriginToRightCenter();
            oneMoreButton.LocalPosition = new Vector2(670, 650);

            endGameButton = new Switch("Sprites/Buttons/spr_btn_endgame", Depth.OverlayButton2);
            AddChild(endGameButton);
            endGameButton.SetOriginToLeftCenter();
            endGameButton.LocalPosition = new Vector2(30, 650);

            this.playState = playState;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (oneMoreButton.Pressed)
            {
                Hide();
                playState.Continue();
                
            }

            if (endGameButton.Pressed)
            {
                Hide();
                playState.EndGame();
            }
        }
    }
}
