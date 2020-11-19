using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// A game object that shows text (instead of an image).
    /// </summary>
    public class TextGameObject : GameObject
    {
        /// <summary>
        /// The text that this object should draw on the screen.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The color to use when drawing the text.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The font to use.
        /// </summary>
        protected SpriteFont font;

        /// <summary>
        /// The depth (between 0 and 1) at which this text should be drawn.
        /// A larger value means that the text will be drawn on top.
        /// </summary>
        protected float depth;

        /// <summary>
        /// An enum that describes the different ways in which a text can be aligned horizontally.
        /// </summary>
        public enum HorizontalAlignment
        {
            Left,
            Right,
            Center
        }

        /// <summary>
        /// An enum that describes the different ways in which a text can be aligned vertically.
        /// </summary>
        public enum VerticalAlignment
        {
            Top,
            Center,
            Bottom
        }

        /// <summary>
        /// The horizontal alignment of this text.
        /// </summary>
        protected HorizontalAlignment horizontalAlignment;

        /// <summary>
        /// The horizontal alignment of this text.
        /// </summary>
        protected VerticalAlignment verticalAlignment;

        /// <summary>
        /// Creates a new TextGameObject with the given details.
        /// </summary>
        /// <param name="fontName">The name of the font to use.</param>
        /// <param name="depth">The depth at which the text should be drawn.</param>
        /// <param name="color">The color with which the text should be drawn.</param>
        /// <param name="horizontalAlignment">The horizontal alignment to use.</param>

        public TextGameObject(string fontName, float depth, Color color, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top)
        {
            font = ExtendedGame.AssetManager.LoadFont(fontName);
            Color = color;
            this.depth = depth;
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;

            Text = "";
        }

        /// <summary>
        /// Draws this TextGameObject on the screen.
        /// </summary>
        /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">A sprite batch object used for drawing sprites.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            // calculate the origin
            Vector2 origin = new Vector2(OriginX, OriginY);

            // draw the text
            spriteBatch.DrawString(font, Text, GlobalPosition,
                Color, 0f, origin, 1, SpriteEffects.None, depth);
        }

        /// <summary>
        /// Gets the x-coordinate to use as an origin for drawing the text.
        /// This coordinate depends on the horizontal alignment and the width of the text.
        /// </summary>
        float OriginX
        {
            get
            {
                // left-aligned
                if (horizontalAlignment == HorizontalAlignment.Left)
                    return 0;

                // right-aligned
                if (horizontalAlignment == HorizontalAlignment.Right)
                    return font.MeasureString(Text).X;

                // centered
                return (font.MeasureString(Text).X + 1) / 2.0f;
            }
        }

        float OriginY
        {
            get
            {
                // top
                if(verticalAlignment == VerticalAlignment.Top)
                {
                    return 0;
                }

                // center
                if(verticalAlignment == VerticalAlignment.Center)
                {
                    return (font.MeasureString(Text).Y + 1) / 2.0f;
                }

                // bottom
                return font.MeasureString(Text).Y;
            }
        }
    }
}