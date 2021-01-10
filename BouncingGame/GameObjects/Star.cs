using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BouncingGame.GameObjects
{
    public class Star: GameObject
    {
        private Texture2D sprite;
        private Rectangle spriteRectangle;
        private Color color;
        private float depth;
        private float time;
        private float visibleTime;

        public Star(Vector2 startLocation, float depth)
        {
            localPosition = startLocation;
            this.depth = depth;

            sprite = ExtendedGame.AssetManager.LoadSprite("Sprites/UI/spr_fireworks");

            var width = ExtendedGame.Random.Next(sprite.Width);
            var height = ExtendedGame.Random.Next(width, Math.Max(width + 5, sprite.Height));
            spriteRectangle = new Rectangle(0, 0, width, height);

            color = new Color(ExtendedGame.Random.Next(255), ExtendedGame.Random.Next(255), ExtendedGame.Random.Next(255));
            var radio = ExtendedGame.Random.NextDouble() * MathHelper.TwoPi;
            velocity = new Vector2((float)Math.Sin(radio), (float)Math.Cos(radio)) * ExtendedGame.Random.Next(70, 150);
            visibleTime = (float)ExtendedGame.Random.NextDouble() * 3;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time > visibleTime)
                Visible = false;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            spriteBatch.Draw(sprite, GlobalPosition, spriteRectangle, color,
                0f, Vector2.Zero, 1f, SpriteEffects.None, depth);
        }
    }
}
