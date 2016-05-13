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
    /// <summary>
    /// identify the object movability as movable
    /// </summary>
    class movable:moveState
    {
        public movable() { }
        public override bool isMovable()
        {
            return true; 
        }
        public override void move(ref Vector2 initial, Vector2 destination)
        {
            initial = new Vector2(destination.X + FindThePathGame.frameLen, destination.Y + FindThePathGame.frameLen); 
        }
    }
}
