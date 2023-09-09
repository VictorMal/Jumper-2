using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumper.GameObjects
{
    public class AnimatedSprite : GameObject
    {
        protected Texture2D _movingTexture;

        protected int _currentFrame;
        protected int _movingFrameWidth;
        protected int _movingFrameHeight;

        protected int _timeElapsed;
        protected int _timeForFrame = 75;

        protected bool _isMoving;
        protected bool _isMovingRight;

        protected int MovingFramesLength { get { return _movingTexture.Width / _movingFrameWidth; } }

        public bool IsMoving { get { return _isMoving; } }
        public bool IsMovingRight { get { return _isMovingRight; } }


        public AnimatedSprite(Texture2D idleTexture,Texture2D movingTexture, Vector2 position, int width, int height,int movingFrameWidth, int movingFrameHeight)
            : base(idleTexture, position, width, height)
        {
            _movingTexture = movingTexture;
            _movingFrameWidth = movingFrameWidth;
            _movingFrameHeight = movingFrameHeight;
        }

        public void Stop()
        {
            _isMoving = false;
        }

        public void Move(bool isMovingRight)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                _timeElapsed = 0;
                _currentFrame = 0;
            }
            _isMovingRight = isMovingRight;
        }



        public override void Update(GameTime gameTime)
        {
            _timeElapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (_timeElapsed > _timeForFrame)
            {
                if (_isMoving)
                {
                    _currentFrame = (_currentFrame + 1) % MovingFramesLength;
                }

                _timeElapsed = 0;
            }      
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (!_isMovingRight)
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
