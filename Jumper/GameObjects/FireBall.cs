using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumper.GameObjects
{
    public class FireBall : GameObject
    {
        public int Speed { get; set; }
        public bool isDirectionRight { get; set; }
        public AnimatedSprite Owner { get; set; }

        public FireBall(Texture2D texture, Vector2 position, int width, int height, int speed, bool isDirectionRight, AnimatedSprite owner) : base(texture, position, width, height) 
        {
            this.Speed = speed;
            this.isDirectionRight = isDirectionRight;
            this.Owner = owner;
        }
    }
}
