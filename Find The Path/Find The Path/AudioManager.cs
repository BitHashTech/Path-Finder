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


using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Find_The_Path
{
    /// <summary>
    /// responsible for playing different sounds
    /// </summary>
    abstract class AudioManager
    {
        public AudioManager()
        { }
        protected string AudioPath;
        protected Song audio;

        
        public Song getSong()
        {
            return audio;
        }
        public string getSongPath()
        {
            return AudioPath;
        }

        public void setSong(Song song)
        {
            audio = song;
        }
    }
}
