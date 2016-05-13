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
    /// responsible of going to the next level
    /// </summary>
    class nextButton : button
    {
        static nextButton nextBtn = null; 
        private nextButton() { }
        private nextButton(string textureName,Vector2 pos, float width, float height) 
            : base(textureName , pos, width , height)
        {
        }
        public static nextButton getNextButton(string textureName, Vector2 pos, float width, float height)
        {
            if (nextBtn == null)
                nextBtn = new nextButton(textureName, pos, width, height);
            return nextBtn;
        }
        public override void work()
        {
            FindThePathGame.gameState = "nextLevel";
            playSoundEffect();
        }
    }
}
