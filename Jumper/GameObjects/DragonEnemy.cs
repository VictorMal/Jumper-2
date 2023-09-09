using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumper.GameObjects
{
    public class DragonEnemy : AnimatedSprite
    {
        int _timeForShooting;
        bool _canShooting = true;

        public bool CanShooting { get { return _canShooting; } }
        public int Speed { get; set; }
        public void HasShooted()
        {
            _canShooting = false;
            _timeForShooting = 3000;
        }


        public DragonEnemy(Texture2D idleTexture, Texture2D movingTexture, Vector2 position, int width, int height, int movingFrameWidth, int movingFrameHeight)
            : base(idleTexture,movingTexture,position,width,height,movingFrameWidth,movingFrameHeight)
        {             
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_timeForShooting > 0)
            {
                _timeForShooting -= gameTime.ElapsedGameTime.Milliseconds;
            }           
            else if (_timeForShooting <= 0)
            {
                _canShooting = true;
            }           
        }
    }
}
