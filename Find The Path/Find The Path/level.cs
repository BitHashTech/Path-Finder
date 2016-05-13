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
    abstract class level
    {
        protected static int height = 4;
        protected static int width = 4;
        protected brick[,] bricks;
        protected List<string> refer;
        protected int levelNumber;
        public level()
        {
            bricks = new brick[height, width];
            refer = new List<string>();
            /// to start the indexing in refer from 1, add "EMPTY" at index 0 
            refer.Add("EMPTY");
            set_references();
        }
        /// <summary>
        /// factory function to make a level of hand made or random
        /// </summary>
        /// <param name="num">
        /// level number
        /// </param>
        /// <returns></returns>
        public static level makeLevel(int num)
        {
            if (num == -1)
            {
                return new randomLevel();
            }
            else
            {
                string fileName = "Data\\level" + num.ToString() + ".txt";
                if (File.Exists(fileName) == true)
                    return new handMadeLevel(num);
                return new randomLevel(); 
            }
        }
        /// <summary>
        /// read the bricks names from a text file and save them to refer
        /// </summary>
        void set_references()
        {
            FileStream fs = new FileStream("Data\\reference.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string file = sr.ReadToEnd();
            string[] fileLines = file.Split('\n');

            for (int i = 0; i < fileLines.Length; i++)
            {
                string[] split = fileLines[i].Split('-');
                if (split.Length > 1)
                {
                    string tmpSplit = split[1];
                    string tmpName = "";
                    for (int j = 0; j < tmpSplit.Length; j++)
                        if ((tmpSplit[j] >= 'a' && tmpSplit[j] <= 'z') || tmpSplit[j] == '_')
                            tmpName += tmpSplit[j];
                    refer.Add(tmpName);
                }
            }
            sr.Close();
            fs.Close();
  
        }

        public static string getOpposite(string direction)
        {
            if (direction == "left") return "right";
            else if (direction == "right") return "left";
            else if (direction == "down") return "up";
            else if (direction == "up") return "down";
            else return "";
        }
        public brick[,] getBricks()
        {
            return bricks;
        }
        public int getWidth()
        {
            return width;
        }
        public int getHeight()
        {
            return height;
        }
        public int getLeveLNumber()
        {
            return levelNumber;
        }
        
    }
}
