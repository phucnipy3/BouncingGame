using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingGame.GameObjects
{
    public class Star: GameObject
    {
        private Texture2D sprite;
        private Rectangle spriteRectangle;
        private Color color;
        private Vector2 velocity;
        private int depth;
        private float time;
        private float visibleTime;

        public Star(Vector2 startLocation, int depth)
        {
            localPosition = startLocation;
            this.depth = depth;
            // get sprite
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time > visibleTime)
                Visible = false;
        }
    }
}
