using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumper.GameObjects
{
    public abstract class GameObject
    {
        protected int _width;
        protected int _height;

        protected Vector2 _position;
        protected Texture2D _texture;

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, _width, _height); }
            set 
            {
                _position.X = value.X;
                _position.Y = value.Y;
            }
        }
        public bool Alive { get; set; }
        public virtual bool OnScreen 
        {
            get
            {
                return (Rectangle.Left < JumperGame.Width
                    && Rectangle.Right > 0
                    && Rectangle.Bottom > 0
                    && Rectangle.Top < JumperGame.Height);
            } 
        }

        public GameObject(Texture2D texture, Vector2 position, int width, int height)
        {
            _texture = texture;
            _position = position;
            _width = width;
            _height = height;
            Alive = true;
        }

        public virtual void Update(GameTime gameTime)
        { 
        
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, this.Rectangle, Color.White);
        }
    }
}
