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
    class chooseLevelButton : button
    {
        public chooseLevelButton() { }
        public chooseLevelButton(string textureName,Vector2 pos, float width, float height) 
            : base(textureName , pos, width , height)
        {
        }
        public override void work()
        {
            string tmpName = getName();
            tmpName = tmpName.Remove(0, 8);
            FindThePathGame.gameState = "loading" + tmpName; 
            playSoundEffect();
        }
    }
}
