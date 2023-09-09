using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumper.GameObjects
{
    public class KnightEnemy : AnimatedSprite
    {
        public int Speed { get; set; }

        public KnightEnemy(Texture2D idleTexture, Texture2D movingTexture, Vector2 position, int width , int height, int movingFrameWidth, int movingFrameHeight, int speed)
            : base(idleTexture,movingTexture, position, width, height, movingFrameWidth, movingFrameHeight)
        {
            this.Speed = speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (_isMovingRight)
                effects = SpriteEffects.FlipHorizontally;

            if (_isMoving)
            {
                Rectangle sourceRect = new Rectangle(_currentFrame * _movingFrameWidth, 0, _movingFrameWidth, _movingFrameHeight);
                spriteBatch.Draw(_movingTexture, Rectangle, sourceRect, Color.White, 0, Vector2.Zero, effects, 0);
            }
            else//if Stop
            {
                spriteBatch.Draw(_texture, Rectangle, null, Color.White, 0, Vector2.Zero, effects, 0);
            }
        }

    }
}
