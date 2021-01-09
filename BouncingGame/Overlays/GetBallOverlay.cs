using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.Overlays
{
    public class GetBallOverlay : Overlay
    {
        private SpriteGameObject background;
        private AnimatedGameObject giftBox;
        private AnimatedGameObject glowing;
        private float loadingTime = 1f;
        private float timeEslapsed = 0;
        private TextGameObject message;

        public GetBallOverlay() : base()
        {
            giftBox = new AnimatedGameObject(Depth.GiftBox);
            giftBox.LoadAnimation("Sprites/Animations/spr_animation_getball@4x3", "box", false, 0.05f);
            giftBox.PlayAnimation("box", true);
            giftBox.SetOriginToCenter();
            giftBox.LocalPosition = new Vector2(350, 600);
            AddChild(giftBox);

            glowing = new AnimatedGameObject(Depth.OverlayButton);
            glowing.LoadAnimation("Sprites/Animations/spr_animation_glowing@5", "glowing", true, 0.1f);
            glowing.PlayAnimation("glowing", true);
            glowing.SetOriginToCenter();
            glowing.LocalPosition = new Vector2(350, 600);
            AddChild(glowing);

            background = new SpriteGameObject("Sprites/Backgrounds/spr_getball", Depth.OverlayBackground);
            AddChild(background);

            message = new TextGameObject("Fonts/GetBall", Depth.OverlayMessage, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            message.Visible = false;
            message.Text = "Click to get ball!";
            AddChild(message);
            Reset();
        }

        public override void Show()
        {
            base.Show();
            giftBox.PlayAnimation("box", true);
            timeEslapsed = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timeEslapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeEslapsed > loadingTime)
            {
                message.Visible = true;
            }

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (timeEslapsed > loadingTime && inputHelper.MouseLeftButtonPressed())
            {
                Hide();
            }
        }
    }
}
