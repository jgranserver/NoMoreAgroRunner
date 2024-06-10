using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace NoMoreAgroRunner
{
    public class CustomUIText : UIElement
    {
        private string text;
        private float textScale;
        private Color textColor;
        private DynamicSpriteFont font;

        public CustomUIText(string text, float textScale = 1f, Color textColor = default)
        {
            this.text = text;
            this.textScale = textScale;
            this.textColor = textColor == default ? Color.White : textColor;
            this.font = FontAssets.MouseText.Value;
            this.Width.Set(font.MeasureString(text).X * textScale, 0f);
            this.Height.Set(font.MeasureString(text).Y * textScale, 0f);
        }

        public void SetText(string text)
        {
            this.text = text;
            this.Width.Set(font.MeasureString(text).X * textScale, 0f);
            this.Height.Set(font.MeasureString(text).Y * textScale, 0f);
        }

        public void SetTextColor(Color color)
        {
            this.textColor = color;
        }

        public void SetTextScale(float scale)
        {
            this.textScale = scale;
            this.Width.Set(font.MeasureString(text).X * scale, 0f);
            this.Height.Set(font.MeasureString(text).Y * scale, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Vector2 position = GetDimensions().Position();
            spriteBatch.DrawString(font, text, position, textColor, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
        }
    }

}