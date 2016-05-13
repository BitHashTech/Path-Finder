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
using Microsoft.Xna.Framework;

namespace Find_The_Path
{
    /// <summary>
    /// the main element of a level ( the bricks with the pathes ) 
    /// </summary>
    class brick:entity
    {
        static readonly public float brickHeight = 150f;
        static readonly public float brickWidth = 150f;
        Star star; 
        public brick()
        {
            this.height = brickHeight; 
            this.width = brickWidth;
            star = null; 
            movability = new nonMovable(); 
        }
        public brick(string textureName, Vector2 pos , bool hasStar) : this() 
        {
            this.name = this.name + textureName;
            this.position = new Vector2(pos.X * width + FindThePathGame.frameLen, pos.Y * height + FindThePathGame.frameLen);
            if (hasStar)
            {
                star = new Star(Star.calculatePosition(this.name,this.position));    
            }
        }
        /// <summary>
        /// deep copy constructor 
        /// </summary>
        /// <param name="br"></param>
        public brick(brick br)
        {
            this.name = br.getName();
            this.color = br.getColor();
            this.height = br.getHeight();
            this.setTexture(br.getImage());
            this.movability = br.getMovability();
            this.position = br.getPosition();
            this.select = br.getSelect();
            this.star = br.getStar();
            this.width = br.getWidth(); 
        }
        /// <summary>
        /// while the brick positions changes, the star position needs to be updated
        /// </summary>
        public void updateStarPos()
        {
            if (hasStar())
            {
                Vector2 pos = Star.calculatePosition(this.name, new Vector2(this.position.X - FindThePathGame.frameLen, this.position.Y - FindThePathGame.frameLen));
                this.star.setPosition(pos);
                this.star.updateCollider(); 
            }
        }
        public bool hasStar()
        {
            return star != null;
        }
        public Star getStar()
        {
            if (hasStar())
            {
                return star;
            }
            return null; 
        }
        /// <summary>
        /// make the star disappear 
        /// </summary>
        public void UnloadStar()
        {
            star.setColor(Color.Transparent);
        }
        public bool isMovable() 
        {
            return movability.isMovable(); 
        }
        public void changeStarSelectState()
        {
            if (hasStar())
            {
                star.changeSelectState();
            }
        }
    }
}
