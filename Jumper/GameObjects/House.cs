using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumper.GameObjects
{
    public class House : GameObject
    {
        public House(Texture2D texture, Vector2 position, int width, int height) 
            : base(texture, position, width, height) 
        {
 
        }

    }
}
