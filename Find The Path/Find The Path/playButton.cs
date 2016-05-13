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
    /// responsible of going to choose level 
    /// </summary>
    class playButton:button
    {
        static playButton playBtn = null; 
        private playButton() { }
        private playButton(string textureName,Vector2 pos, float width, float height) 
            : base(textureName , pos, width , height)
        {
        }
        public static playButton getPlayButton(string textureName,Vector2 pos, float width, float height)
        {
            if (playBtn == null)
                playBtn = new playButton(textureName, pos, width, height);
            return playBtn;
        }
        public override void work()
        {
            FindThePathGame.gameState = "choose";
            playSoundEffect();
        }
    }
}
