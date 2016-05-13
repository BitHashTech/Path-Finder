//************************************************\\
//*********            © 2016           **********\\
//*********     Ain Shams University    **********\\
//* Faculty of Computers and Information Science *\\
//*********    Object Oriented Course   **********\\
//*********          Team    :          **********\\
//*********         Ahmed Salah         **********\\
//*********       Moataz Yassin         **********\\
//*********     Omar Abd EL-Hakim       **********\\
//*********    Mohamed Yousry Yahia     **********\\
//************************************************\\


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Find_The_Path
{
    /// <summary>
    /// the main context of things 
    /// </summary>
    class entity
    {
        protected string name;
        private Texture2D image;
        protected Vector2 position;
        protected Color color;
        protected moveState movability;
        protected CollisionState collider;
        protected selectState select;
        protected SoundEffect soundEffect ; 
        protected float height, width;
        public entity()
        {
            name = "Texture\\" ;
            color = new Color(255, 255, 255);
            movability = new nonMovable();
            select = new nonSelected();
            soundEffect = null;
            collider = new nonCollisionable(); 
        }
        public entity(string textureName, Vector2 pos , float width , float height) 
            : this() 
        {
            this.height = height;
            this.width = width; 
            this.name = this.name + textureName;
            this.position = pos;
        }
        public bool hasCollider()
        {
            return collider.hasCollider(); 
        }
        public Rectangle getRectangle()
        {
            return collider.getRectangle(); 
        }
        public void changeCollisionState()
        {
            if (collider.GetType().ToString().Contains("non") == true)
                collider = new collisionable(position, new Vector2(width, height));
            else
                collider = new nonCollisionable();
            changeColor(); 
        }
        public void updateCollider()
        {
            if (hasCollider() == true)
                this.collider = new collisionable(position, new Vector2(width, height));
        }
        public void changeSelectState()
        {
            if (select.GetType().ToString().Contains("non") == true)
                select = new selected();
            else
                select = new nonSelected();
            changeColor();
        }
        private void changeColor()
        {
            select.changeColor(ref this.color);
        }
        public void move(Vector2 destination)
        {
            movability.move(ref this.position, destination);
        }


        public void setTexture(Texture2D texture)
        {
            this.image = texture;
        }
        public void setSoundEffect(SoundEffect soundEffect)
        {
            this.soundEffect = soundEffect; 
        }
        public void playSoundEffect()
        {
            this.soundEffect.Play(); 
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public virtual void setPosition(Vector2 pos)
        {
            this.position = new Vector2(pos.X + FindThePathGame.frameLen, pos.Y + FindThePathGame.frameLen); 
        }
        public void setColor(Color clr)
        {
            this.color = clr;
        }
        public void setDimentions(Vector2 dimentions)
        {
            this.width = dimentions.X;
            this.height = dimentions.Y;
        }



        public string getName() 
        {
            return name ; 
        }
        public Texture2D getImage()
        {
            return image; 
        }
        public Vector2 getPosition()
        {
            return position; 
        }
        public Color getColor()
        {
            return color; 
        }
        public float getHeight()
        {
            return height; 
        }
        public float getWidth()
        {
            return width;
        }
        public moveState getMovability()
        {
            return movability;
        }
        public selectState getSelect()
        {
            return select;
        }

        static public void swap<T> (ref T ent1 , ref T ent2)
        {
            T hold = ent1;
            ent1 = ent2;
            ent2 = hold; 
        }
    }

}
