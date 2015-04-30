using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using C3.XNA;

namespace DeepEngine
{
    public static class Solar
    {
        const int shadowThickness = 4;
        const float shadowAlpha = 0.3f;
        const float borderThickness = 2f;

        public static void DrawWindow(this SpriteBatch sb, Rectangle location, Color windowColor, float windowAlpha)
        {
            var shadow1 = new Rectangle(location.Right, location.Top, shadowThickness, location.Height);
            var shadow2 = new Rectangle(location.Left, location.Bottom, location.Width + shadowThickness, shadowThickness);

            Primitives2D.FillRectangle(sb, shadow1, Color.Black * shadowAlpha);
            Primitives2D.FillRectangle(sb, shadow2, Color.Black * shadowAlpha);
            Primitives2D.FillRectangle(sb, location, windowColor * windowAlpha);
            Primitives2D.DrawRectangle(sb, location, Color.Black * 0.8f, borderThickness);
        }

        public static void DrawWindow(this SpriteBatch sb, Rectangle location, Color windowColor, float windowAlpha, String title, SpriteFont titleFont, Color titleColor, float titleAlpha)
        {
            DrawWindow(sb, location, windowColor, windowAlpha);

            var titleSize = titleFont.MeasureString(title);

            var titleRect = new Rectangle(
                location.X + (int)Math.Ceiling(borderThickness), 
                location.Y + (int)Math.Ceiling(borderThickness), 
                location.Width - 2 * (int)Math.Ceiling(borderThickness), 
                (int)titleSize.Y);
            
            Primitives2D.FillRectangle(sb, titleRect, windowColor * windowAlpha);
            sb.DrawString(titleFont, title, new Vector2(location.X + borderThickness, location.Y + borderThickness), titleColor * titleAlpha);
        }

        public static void DrawWindow(this SpriteBatch sb, Rectangle location, Color windowColor, float windowAlpha, String title, SpriteFont titleFont, Color titleColor, float titleAlpha, String body, SpriteFont bodyFont, Color bodyColor, float bodyAlpha)
        {
            DrawWindow(sb, location, windowColor, windowAlpha, title, titleFont, titleColor, titleAlpha);

            var titleSize = titleFont.MeasureString(title);

            var bodyStart = new Vector2(location.X + borderThickness + 5, location.Y + borderThickness + titleSize.Y);

            sb.DrawString(bodyFont, body, bodyStart, bodyColor * bodyAlpha);
        }

        public static void DrawDebugOverlay(this SpriteBatch sb, Rectangle location)
        {
            sb.FillRectangle(location, Color.SlateGray * 0.5f);
        }
    }
}
