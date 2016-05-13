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
using System.IO;
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
    /// a level made by hands and read from a text file 
    /// </summary>
    class handMadeLevel : level
    {
        public handMadeLevel()
        {
        }
        public handMadeLevel(int num)
        {
            levelNumber = num;
            readLevel(num);
        }
        /// <summary>
        ///read level from file
        ///and construct the level in bricks
        /// </summary>
        /// <param name="num"></param>
        public void readLevel(int num)
        {

            int[,] stars = new int[4, 4];
            string fileName = "Data\\star" + num.ToString() + ".txt";
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            for (int i = 0; i < 4; i++)
            {
                string line = sr.ReadLine();
                string[] nums = line.Split(' ');
                for (int j = 0; j < 4; j++)
                {
                    stars[i, j] = int.Parse(nums[j]);
                }
            }
            sr.Close();
            fs.Close();
            fileName = "Data\\level" + num.ToString() + ".txt";
            fs = new FileStream(fileName, FileMode.Open);
            sr = new StreamReader(fs);
            int row = 0;
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] nums = line.Split(' ');

                for (int col = 0; col < width; col++)
                {
                    string tmpName = refer[int.Parse(nums[col])];
                    string[] cutName = tmpName.Split('_');
                    String suffix = cutName[cutName.Length - 1];
                    if (suffix.Equals("n"))
                    {
                        bricks[row, col] = new normalBrick(tmpName, new Vector2(col, row), stars[row, col] == 1);
                    }
                    else if (suffix.Equals("st"))
                    {
                        bricks[row, col] = new startBrick(tmpName, new Vector2(col, row));
                    }
                    else if (suffix.Equals("en"))
                    {
                        bricks[row, col] = new endBrick(tmpName, new Vector2(col, row));
                    }
                    else
                    {
                        bricks[row, col] = new fixedBrick(tmpName, new Vector2(col, row), stars[row, col] == 1);
                    }
                }
                row++;
            }

            sr.Close();
            fs.Close();

        }
    }
}
