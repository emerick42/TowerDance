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


namespace TowerDance
{
    class Positionnable
    {
        protected Vector2 position;

        public Positionnable()
        {
            position = new Vector2();
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

    }
}
