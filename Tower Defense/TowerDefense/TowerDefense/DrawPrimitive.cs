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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace TowerDefense
{
    class DrawPrimitive
    {
        GraphicsDevice graphics;
        Texture2D blank;

        public DrawPrimitive()
        {        }

        public DrawPrimitive(GraphicsDevice g)
        {
            graphics = g;
        }

        public void load()
        {
            blank = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
        }

        public void load(GraphicsDevice g)
        {
            graphics = g;
            blank = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
        }

        public void unload()
        {
            blank.Dispose();
        }

        public void update(GameTime gameTime)
        {
        }

        public void drawLine(SpriteBatch sb, float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            sb.Draw(blank, point1, null, color, angle, Vector2.Zero, new Vector2(length, width), SpriteEffects.None, 0);
        }

    }
}
