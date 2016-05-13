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

namespace Find_The_Path
{
    /// <summary>
    /// responsible of the loading sound
    /// </summary>
    class LoadingBGS :AudioManager 
    {
        public LoadingBGS()
        {
            this.AudioPath = "Sounds\\BGS_loading";
        }
    
    }
}
