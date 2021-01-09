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
            continueBackground = new Switch("Sprites/Backgrounds/spr_continue", 0.75f);
            AddChild(continueBackground);

            oneMoreButton = new Switch("Sprites/Buttons/spr_btn_onemore", 1);
            AddChild(oneMoreButton);
            oneMoreButton.LocalPosition = new Vector2(100, 400);

            endGameButton = new Switch("Sprites/Buttons/spr_btn_endgame", 1);
            AddChild(endGameButton);
            endGameButton.LocalPosition = new Vector2(400, 400);

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
