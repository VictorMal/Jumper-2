using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jumper.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumper.GameObjects
{
    public class Block : GameObject
    {

        public Block(Texture2D texture, Vector2 position,int width, int height):base(texture,position,width,height)
        {         
        }

        public override bool OnScreen
        {
            get
            {
                return Rectangle.Left < JumperGame.Width && Rectangle.Right > 0;
            }
        }

        public void MoveBlock(int dx)
        {
            _position.X += dx;           
        }
    }
}
