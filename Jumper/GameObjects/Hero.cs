using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumper.Helpers;
using Jumper.Enums;

namespace Jumper.GameObjects
{
    public class Hero : AnimatedSprite
    {
        Texture2D _heroJumpTexture;
        BonusEnum _bonusEnum = BonusEnum.None;

        int _jumpFrameWidth;
        int _jumpFrameHeight;

        int _timeForShocked;
        int _timeForBonus;
        double _timeForShooting;

        int _oldSpeed = -1;

        float _maxYVelocity = 7;

        bool _isShocked;
        bool _hasBonus;
        bool _canShooting = false;

        public float _yVelocity;
        public bool _isJumping;

        private int JumpFramesLength { get { return _heroJumpTexture.Width / _jumpFrameWidth; } }

        public int Speed { get; set; }
        public bool Win { get; set; }
        public bool IsShocked { get { return _isShocked; } }
        public bool CanShooting { get { return _canShooting; } }
        public BonusEnum Bonus { get { return _bonusEnum; } }

        public Hero(Texture2D hero_idle, Texture2D hero_run, Texture2D hero_jump, Vector2 position, int width, int heigth, int runFrameWidth, int runFrameHeight, int jumpFrameWidth, int jumpFrameHeight)
            : base(hero_idle, hero_run, position, width, heigth, runFrameWidth, runFrameHeight)
        {
            _heroJumpTexture = hero_jump;
            _jumpFrameWidth = jumpFrameWidth;
            _jumpFrameHeight = jumpFrameHeight;
        }

        public void Jump()
        {
            if (!_isJumping && _yVelocity == 0.0f)
            {
                _isJumping = true;
                _currentFrame = 0;
                _timeElapsed = 0;
                _yVelocity = _maxYVelocity;
            }
        }

        public void Shocked()
        {
            if (_bonusEnum != BonusEnum.Armor)
            {
                _timeForShocked = 3000;
                _isShocked = true;
                this.Stop();
            }
        }

        public void HasShooted()
        {
            _timeForShooting = 1000;
            _canShooting = false;
        }

        public void SetBonus(BonusEnum bonusEnum)
        {
            //отмена прошлых бонусов
            switch (_bonusEnum)
            { 
                case BonusEnum.Speed:
                    Speed = _oldSpeed;
                    break;
                case BonusEnum.Gun:
                    _canShooting = false;
                    break;
            }

            _bonusEnum = bonusEnum;
            _timeForBonus = 5000;
            _hasBonus = true;

            switch (bonusEnum)
            {
                case BonusEnum.Speed:
                    _oldSpeed = Speed;
                    Speed += 2;
                    break;
                case BonusEnum.Gun:
                    _canShooting = true;
                    break;
            }
        }

        //update only time and sprites and shocked
        public override void Update(GameTime gameTime)
        {
            _timeElapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (_timeElapsed > _timeForFrame)
            {
                if (_isJumping)
                    _currentFrame = (_currentFrame + 1) % JumpFramesLength;
                else if (_isMoving)
                    _currentFrame = (_currentFrame + 1) % MovingFramesLength;

                _timeElapsed = 0;
            }

            if (_hasBonus)
            {
                if (_timeForBonus > 0)
                {
                    _timeForBonus -= gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                {
                    switch (_bonusEnum)
                    {
                        case BonusEnum.Speed:
                            Speed = _oldSpeed;
                            break; 
                        case BonusEnum.Gun:
                            _canShooting = false;
                            break;
                    }

                    _bonusEnum = BonusEnum.None;
                    _hasBonus = false;
                }
            }


            if (_timeForShooting > 0)
                _timeForShooting -= gameTime.ElapsedGameTime.Milliseconds;
            else if (!_canShooting && _bonusEnum == BonusEnum.Gun)
                _canShooting = true;

            if (_timeForShocked > 0)
                _timeForShocked -= gameTime.ElapsedGameTime.Milliseconds;
            else if (_isShocked)
                _isShocked = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (!_isMovingRight)
                effects = SpriteEffects.FlipHorizontally;


            if (_isJumping)
            {
                Rectangle sourceRect = new Rectangle(_currentFrame * _jumpFrameWidth, 0, _jumpFrameWidth, _jumpFrameHeight);
                spriteBatch.Draw(_heroJumpTexture, Rectangle, sourceRect, Color.White, 0, Vector2.Zero, effects, 0);
            }
            else if (_isMoving)
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
