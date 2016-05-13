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
    /// make a random level
    /// </summary>
    class randomLevel:level
    {
        Random rand; 
        bool[,] visited;
        bool[,] stars;
        string[,] tmpLevel;
        int pathLen;
        int backCounter;
        int fixedCounter;
        int starCounter; 
        Dictionary<string, bool> takenPermutation;

        public randomLevel()
        {
            levelNumber = -1;
            rand = new Random(); 
            generate();
        }
        /// <summary>
        /// generate a random start position and call the construct of a whole level and make bricks
        /// </summary>
        private void generate()
        {
            takenPermutation = new Dictionary<string, bool>();
            visited = new bool[height, width];
            tmpLevel = new string[height, width];
            stars = new bool[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {          
                    visited[i, j] = false;
                    tmpLevel[i, j] = null;
                    stars[i, j] = false; 
                }
            pair<int, int> currentPos = new pair<int, int>();
            currentPos.first = rand.Next() % 4;
            currentPos.second = rand.Next() % 4;
            pathLen = rand.Next() % 7 + 6;
            backCounter = rand.Next() % 4 + 3;
            fixedCounter = rand.Next() % 4 + 1 ;
            starCounter = 3; 
            List<string> possible = getPossible("_st");
            shuffleList(possible);
            for (int i = 0; i < possible.Count; i++)
            {
                string[] split = possible[i].Split('_');
                bool check = isMade(currentPos.first, currentPos.second, split[0] , possible[i], 0, 1);
                if (check) break;
            }
            List<pair<int, int>> positions = new List<pair<int, int>>();
            List<pair<int, int>> possibleStarsPosition = new List<pair<int, int>>(); 
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tmpLevel[i, j] == null)
                    {
                        positions.Add(new pair<int, int>(i, j));
                    }
                    else
                    {
                        if (tmpLevel[i, j].Contains("_st") == false && tmpLevel[i, j].Contains("_en") == false && tmpLevel[i, j].Contains("empty") == false && tmpLevel[i, j].Contains("back") == false)
                        {
                            possibleStarsPosition.Add(new pair<int, int>(i, j));
                        }
                    }
                }
            }
            shuffleList(possibleStarsPosition);
            for (int i = 0; i < starCounter; i++)
            {
                stars[possibleStarsPosition[i].first, possibleStarsPosition[i].second] = true;
            }
            shuffleList(positions);
            for (int i = 0; i < positions.Count; i++)
            {
                if (backCounter > 0)
                {
                    tmpLevel[positions[i].first, positions[i].second] = "back_n";
                    backCounter--;
                }
                else
                {
                    List < string > tmp  = new List<string>();
                    for ( int j = 1 ; j < refer.Count ; j++ ) 
                    {
                        if ( refer[j].Contains("_en") == false && refer[j].Contains("_st") == false ) 
                        {
                            if ( refer[j].Contains("_s") == true ) 
                            {
                                if ( fixedCounter > 0 )
                                {
                                    tmp.Add(refer[j]);
                                    fixedCounter--;
                                }
                            }
                            else 
                            {
                                tmp.Add(refer[j]);
                            }

                        }
                    }
                    shuffleList(tmp);
                    tmpLevel[positions[i].first, positions[i].second] = tmp[0];
                }
            }


            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    string tmpName = tmpLevel[row, col];
                    string[] cutName = tmpName.Split('_');
                    String suffix = cutName[cutName.Length - 1];
                    if (suffix.Equals("n"))
                    {
                        bricks[row, col] = new normalBrick(tmpName, new Vector2(col, row), stars[row, col] == true);
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
                        bricks[row, col] = new fixedBrick(tmpName, new Vector2(col, row), stars[row, col] == true);
                    }
                }
            }
            shuffleLevel(0);
        }
        /// <summary>
        /// make a path ( construct a valid level ) 
        /// </summary>
        /// <param name="x">
        /// index of row
        /// </param>
        /// <param name="y">
        /// index of column
        /// </param>
        /// <param name="direction"></param>
        /// <param name="fullName">
        /// full name of a brick
        /// </param>
        /// <param name="fixedCtr">
        /// number of current fixed bricks 
        /// </param>
        /// <param name="len"></param>
        /// <returns></returns>
        bool isMade(int x, int y, string direction, string fullName, int fixedCtr, int len)
        {
            if (x < 0 || x >= 4 || y < 0 || y >= 4) return false;
            if (visited[x, y] == true) return false;
            
            if (len == pathLen)
            {
                tmpLevel[x, y] = fullName;
                fixedCounter -= fixedCtr;
                return true;
            }
            visited[x, y] = true;
            tmpLevel[x, y] = fullName;

            int tmpX = x;
            int tmpY = y; 
            if (direction == "left") y--;
            else if (direction == "right") y++;
            else if (direction == "up") x--;
            else if (direction == "down") x++;
            
            string opposite = getOpposite(direction);
            List<string> possible = getPossible(opposite);
            shuffleList(possible);
            bool check = false;
            for (int i = 0; i < possible.Count; i++)
            {
                string[] split = possible[i].Split('_');
                if (len == pathLen - 1)
                {
                    if (split[split.Length - 1] == "en")
                    {
                        check |= isMade(x, y, "en", possible[i], fixedCtr, len + 1);
                    }
                }
                else 
                {
                    string dir;
                    if (split[0] == opposite)
                        dir = split[1];
                    else if (split[1] == opposite)
                        dir = split[0];
                    else continue;
                    if (split[split.Length - 1] == "s" && fixedCtr < fixedCounter)
                    {
                        check |= isMade(x, y, dir, possible[i], fixedCtr + 1, len + 1);
                    }
                    else if (split[split.Length - 1] != "s" && split[split.Length - 1] != "en" && split[split.Length-1] != "st")
                    {
                        check |= isMade(x, y, dir, possible[i], fixedCtr, len + 1);
                    }
                }
                if (check)
                    return true;
            }
            tmpLevel[tmpX, tmpY] = null;
            visited[tmpX, tmpY] = false;
            return false; 
        }
        /// <summary>
        /// get possible bricks to be put depended on a direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        List<string> getPossible(string direction)
        {
            List<string> possible = new List<string>();
            foreach (string name in refer)
            {
                if (name == "EMPTY") continue;
                if (name.ToLower().Contains(direction) == true)
                {
                    possible.Add(name);
                }
            }
            return possible; 
        }
        /// <summary>
        /// Fisher–Yates shuffle algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        void shuffleList < T > (List<T> list)
        {
            int index = list.Count;
            while (index > 1)
            {
                index--;
                int index2 = rand.Next(index + 1);
                T tmp = list[index];
                list[index] = list[index2];
                list[index2] = tmp; 
            }
        }
        /// <summary>
        /// miss the right pass to be a valid playable level 
        /// </summary>
        /// <param name="ctr">
        /// counter to indicats the number of swaps
        /// </param>
        /// <returns></returns>
        bool shuffleLevel( int ctr )
        {
            string permutation = "" ;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    permutation += tmpLevel[i, j];
            if (takenPermutation.ContainsKey(permutation) == true )
                return false; 

            if (ctr > 100) return true;
            takenPermutation.Add(permutation, true); 
            List<pair< int ,int > > positions = new List<pair<int,int>>() ; 
            for ( int i = 0 ; i < 4 ; i++ ) 
                for ( int j = 0 ; j < 4 ; j++ )
                    if (tmpLevel[i, j] == "back_n")
                    {
                        positions.Add(new pair<int, int>(i, j));
                    }
            List < pair< pair < int , int > , pair < int , int > > > swaps = new List<pair<pair<int,int>,pair<int,int>>>() ; 
            foreach (pair<int, int> pos in positions)
            {
                if (pos.first > 0)
                {
                    if ( tmpLevel[pos.first-1,pos.second].Contains("_n") == true  && tmpLevel[pos.first-1,pos.second] != "back_n")  
                        swaps.Add(new pair<pair<int, int>, pair<int, int>>(pos, new pair<int, int>(pos.first - 1, pos.second)));
                }
                if (pos.first < 3)
                {
                    if (tmpLevel[pos.first +1, pos.second].Contains("_n") == true && tmpLevel[pos.first +1, pos.second] != "back_n")
                        swaps.Add(new pair<pair<int, int>, pair<int, int>>(pos, new pair<int, int>(pos.first + 1, pos.second)));
    
                }
                if (pos.second > 0)
                {
                    if (tmpLevel[pos.first , pos.second-1].Contains("_n") == true && tmpLevel[pos.first , pos.second-1] != "back_n")
                        swaps.Add(new pair<pair<int, int>, pair<int, int>>(pos, new pair<int, int>(pos.first, pos.second - 1)));
                }
                if (pos.second < 3)
                {
                    if (tmpLevel[pos.first , pos.second+1].Contains("_n") == true && tmpLevel[pos.first , pos.second+1] != "back_n")
                        swaps.Add(new pair<pair<int, int>, pair<int, int>>(pos, new pair<int, int>(pos.first, pos.second + 1)));
                }
            }
            shuffleList(swaps);
            foreach (pair<pair<int, int>, pair<int, int>> swp in swaps)
            {
                swap( swp.first,  swp.second);

                bool check = shuffleLevel(ctr + 1);

                if (check) return true;

                swap( swp.first,  swp.second);
              }
            return true ; 
        }
        /// <summary>
        /// swap two bricks
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        private void swap(pair< int , int>first, pair < int , int > second)
        {
            entity.swap(ref tmpLevel[first.first,  first.second], ref tmpLevel[ second.first,  second.second]);
            entity.swap(ref bricks[ first.first,  first.second], ref bricks[ second.first,  second.second]);
            Vector2 tmpPos = bricks[ first.first,  first.second].getPosition();
            tmpPos -= new Vector2(FindThePathGame.frameLen, FindThePathGame.frameLen);
            bricks[ first.first,  first.second].setPosition(bricks[ second.first,  second.second].getPosition() - new Vector2(FindThePathGame.frameLen, FindThePathGame.frameLen));
            bricks[ second.first,  second.second].setPosition(tmpPos);
            bricks[ second.first,  second.second].updateStarPos();
            bricks[ first.first,  first.second].updateStarPos();
            
        }
    }
}
