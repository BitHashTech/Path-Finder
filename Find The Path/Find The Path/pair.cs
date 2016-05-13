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
    /// a template class to make pair of 2 objects
    /// </summary>
    /// <typeparam name="T">
    /// first data type
    /// </typeparam>
    /// <typeparam name="U">
    /// second data type
    /// </typeparam>
    public class pair<T, U>
    {
        public pair()
        {
            first = default(T);
            second = default(U);
        }

        public pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }

        public T first { get; set; }
        public U second { get; set; }
    };

}
