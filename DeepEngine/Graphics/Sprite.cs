using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class Sprite
    {
        private SpriteSheet SpriteSheet { get; set; }
        
        public Texture2D Texture { get { return SpriteSheet.Texture; } }
        public Point[] Varieties { get; set; }
        public Point[] frames { get; set; }
        
        public int VarietyCount
        {
            get { return Varieties.Length; }
        }
        public int FrameCount
        {
            get { return frames.Length; }
        }

        private Sprite(SpriteSheet ss)
        {
            SpriteSheet = ss;
        }

        public static Sprite CreateHorizontalFramesVerticalVarieties(SpriteSheet ss, int frameStart, int frameCount, int varietyStart, int varietyCount)
        {
            Sprite sprite = new Sprite(ss);
            Debug.Assert(ss.YCount >= (varietyStart + varietyCount));
            Debug.Assert(ss.XCount >= (frameStart + frameCount));

            sprite.Varieties = new Point[varietyCount];

            for (int i = 0; i < varietyCount; i++)
            {
                sprite.Varieties[i] = new Point(0, i + varietyStart);
            }

            sprite.frames = new Point[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                sprite.frames[i] = new Point(i + frameStart, 0);
            }

            return sprite;
        }

        public static Sprite CreateVerticalFramesHorizontalVarieties(SpriteSheet ss, int frameStart, int frameCount, int varietyStart, int varietyCount)
        {
            Sprite sprite = new Sprite(ss);
            Debug.Assert(ss.XCount >= (varietyStart + varietyCount));
            Debug.Assert(ss.YCount >= (frameStart + frameCount));

            sprite.Varieties = new Point[varietyCount];

            for (int i = 0; i < varietyCount; i++)
            {
                sprite.Varieties[i] = new Point(i + varietyStart, 0);
            }

            sprite.frames = new Point[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                sprite.frames[i] = new Point(0, i + frameStart);
            }

            return sprite;
        }

        public static Sprite CreateCustomVarieties(SpriteSheet ss, Point[] points)
        {
            Sprite sprite = new Sprite(ss);

            sprite.Varieties = new Point[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                sprite.Varieties[i] = points[i];
            }

            sprite.frames = new Point[1];
            sprite.frames[0] = Point.Zero;

            return sprite;
        }

        public static Sprite CreateCustomFrames(SpriteSheet ss, Point[] points)
        {
            Sprite sprite = new Sprite(ss);
            sprite.frames = new Point[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                sprite.frames[i] = points[i];
            }

            sprite.Varieties = new Point[1];
            sprite.Varieties[0] = Point.Zero;

            return sprite;
        }

        public Rectangle GetBounds(int variety, int frame)
        {
            return SpriteSheet.GetBounds(Varieties[variety].X + frames[frame].X, Varieties[variety].Y + frames[frame].Y);
        }
    }
}
