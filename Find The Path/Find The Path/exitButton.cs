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
    /// responsible of closing the game
    /// </summary>
    class exitButton:button
    {
        static exitButton exitBtn = null ; 
        private exitButton() 
        {
        }
        private exitButton(string textureName, Vector2 pos , float width, float height) 
            : base(textureName , pos , width , height)
        {
        }
        public static exitButton getExitButton(string textureName, Vector2 pos, float width, float height)
        {
            if (exitBtn == null)
                exitBtn = new exitButton(textureName, pos, width, height);
            return exitBtn; 
        }
        public override void work()
        {
            FindThePathGame.gameState = "exit";
            playSoundEffect();
        }
    }
}
