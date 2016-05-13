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
using Microsoft.Xna.Framework;

namespace Find_The_Path
{
    abstract class player:entity
    {
        protected const float velosity = 1;
        const float width = 40;
        const float height = 40;
        public player()
        {
            base.width = width ; 
            base.height = height ;
        }
        /// <summary>
        /// the player is on the middle of a brick not at the edge 
        /// </summary>
        /// <param name="pos"></param>
        public override void setPosition(Vector2 pos)
        {
            pos.X += 55;
            pos.Y += 55;
            this.position = pos;
            this.collider = new collisionable(pos, new Vector2(width, height));
        }

        abstract public void move(string from, string to, Vector2 brickPos);
    }
}
 