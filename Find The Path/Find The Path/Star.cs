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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Find_The_Path
{
    class Star : entity 
    {
        public static readonly float width = 45;
        public static readonly float height = 45;
        public Star() {
            this.name += "star";
            base.height = height;
            base.width = width; 
        }
        public Star(Vector2 pos) : this() 
        {
            this.position = pos;
            collider = new collisionable(pos, new Vector2(width, height));
        }
        public Star(Vector2 pos, bool isSelected) : this(pos)
        {
            changeSelectState();  
        }
        /// <summary>
        /// calculate the right position of a star on a brick 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Vector2 calculatePosition( string name , Vector2 position) 
        {
            Vector2 pos = position;
            if ((name.Contains("left") && name.Contains("right")) ||
                name.Contains("up") && name.Contains("down"))
                pos = new Vector2((position.X - FindThePathGame.frameLen) + brick.brickWidth / 2 - Star.width / 2 + FindThePathGame.frameLen, (position.Y - FindThePathGame.frameLen) + brick.brickHeight / 2 - Star.height / 2 + FindThePathGame.frameLen);
            else if (name.Contains("left") && name.Contains("up"))
                pos = new Vector2((position.X - FindThePathGame.frameLen) + brick.brickWidth / 3f - Star.width / 2 + FindThePathGame.frameLen, (position.Y - FindThePathGame.frameLen) + brick.brickHeight / 3f - Star.height / 2 + FindThePathGame.frameLen);
            else if (name.Contains("right") && name.Contains("up"))
                pos = new Vector2((position.X - FindThePathGame.frameLen) + brick.brickWidth * 2 / 3f - Star.width / 2 + FindThePathGame.frameLen, (position.Y - FindThePathGame.frameLen) + brick.brickHeight / 3f - Star.height / 2 + FindThePathGame.frameLen);
            else if (name.Contains("left") && name.Contains("down"))
                pos = new Vector2((position.X - FindThePathGame.frameLen) + brick.brickWidth * 1.2f / 3f - Star.width / 2 + FindThePathGame.frameLen, (position.Y - FindThePathGame.frameLen) + brick.brickHeight * 2 / 3f - Star.height / 2 + FindThePathGame.frameLen);
            else if (name.Contains("right") && name.Contains("down"))
                pos = new Vector2((position.X - FindThePathGame.frameLen) + brick.brickWidth * 1.8f / 3f - Star.width / 2 + FindThePathGame.frameLen, (position.Y - FindThePathGame.frameLen) + brick.brickHeight * 2 / 3f - Star.height / 2 + FindThePathGame.frameLen);
            return pos; 
        }
        
    }
}
