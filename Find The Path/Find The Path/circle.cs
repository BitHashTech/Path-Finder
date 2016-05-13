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
    /// the loading circle in the loading screen 
    /// </summary>
    class circle : entity
    {
        private float rotate;
        private Vector2 centure; 
        public circle() { }
        public circle(string textureName, Vector2 pos , float width , float height) 
            : base(textureName,pos,width,height)
        {
            rotate = 0; 
            centure = new Vector2(width / 2, height / 2); 
        }
        public float getRotate() 
        {
            return rotate ; 
        }
        public void increaseRotate()
        {
            rotate += 0.15f; 
        }
        public Vector2 getCenture()
        {
            return centure; 
        }
    }
}
