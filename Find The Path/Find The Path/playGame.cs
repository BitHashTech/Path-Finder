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
    /// responsible of constructing a level, moving bricks and finding path 
    /// </summary>
    class playGame 
    {
        const int maxStarsNumber = 3;
        player plyer; 
        level lvl;
        List<object> objects; 
        brick[,] bricks;
        Vector2 startPos;
        Vector2 endPos;
        public static int starsCount;
        private List<Star> stars; 
        public playGame()
        { 
        }
        /// <summary>
        /// a function that initialize a game based on a level number 
        /// it uses a static function in class level 
        /// ( factory design )
        /// </summary>
        /// <param name="lvlNum">
        /// gets a level number and based on it make the level 
        /// </param>
        ///
        public void initialize(int lvlNum)
        {
            while (true)
            {
                lvl = level.makeLevel(lvlNum);
                bricks = lvl.getBricks();

                startPos = new Vector2();
                endPos = new Vector2();
                stars = new List<Star>();
                stars.Add(new Star(new Vector2(800, 300), true));
                stars.Add(new Star(new Vector2(800, 400), true));
                stars.Add(new Star(new Vector2(800, 500), true));
                starsCount = 0;
                plyer = new autoMove();
                setPositions(); 
                
                if ( findPath() == null ) break;
            }
            updateList();
        }
        /// <summary>
        /// update the position of the player rectangle as the player moves
        /// </summary>
        public void updatePlayerCollider()
        {
            plyer.updateCollider();
        }
        /// <summary>
        /// set the positions of start brick, end brick and the player 
        /// </summary>
        private void setPositions()
        {
            foreach (entity ent in bricks)
            {
                if (ent.getName().Contains("_en") == true)
                {
                    endPos = ent.getPosition();
                }
                else if (ent.getName().Contains("_st") == true)
                {
                    startPos = ent.getPosition();
                }
            }
            plyer.setPosition(startPos);

            startPos.X = (startPos.X - FindThePathGame.frameLen) / brick.brickWidth;
            startPos.Y = (startPos.Y - FindThePathGame.frameLen) / brick.brickHeight;
            endPos.X = (endPos.X - FindThePathGame.frameLen) / brick.brickWidth;
            endPos.Y = (endPos.Y - FindThePathGame.frameLen) / brick.brickHeight;
            entity.swap(ref startPos.X, ref startPos.Y);
            entity.swap(ref endPos.X, ref endPos.Y);
 
        }
        public Rectangle getPlayerRectangle()
        {
            return plyer.getRectangle(); 
        }
        /// <summary>
        /// update the list of objects to be drown on a game 
        /// </summary>
        private void updateList()
        {
            objects = new List<object>();
            foreach (object obj in bricks)
            {
                objects.Add(obj);
                brick br = (brick)obj;
                if (br.hasStar())
                {
                    objects.Add(br.getStar());
                }
            }
            objects.Add(plyer);
            for (int counter = 0; counter < starsCount && counter < stars.Count; counter++)
            {
                if (stars[counter].getSelect().GetType().ToString().ToLower().Contains("non") == false)
                    stars[counter].changeSelectState(); 
            }
            objects.AddRange(stars);
        }
        public List<object> getList()
        {
            updateList();
            return objects; 
        }
        bool isValidPos(Vector2 pos)
        {
            return !(pos.X < 0 || pos.X >= lvl.getHeight() || pos.Y < 0 || pos.Y >= lvl.getWidth());
        }
        /// <summary>
        /// find the path if exists
        /// </summary>
        /// <returns></returns>
        public List<pair<brick, pair<string,string>>> findPath() //Brick , from , to
        {
            int height = lvl.getHeight();
            int width = lvl.getWidth();
            bool[,] vis = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    vis[i, j] = false;
                }
            }
            pair<Vector2, string> result = new pair<Vector2, string>(startPos, "st");
            List<pair<brick, pair<string, string>>> path = new List<pair<brick, pair<string, string>>>();
            while (true)
            {
                if (result == null)
                {
                    return null;
                }
                if ( isValidPos(result.first) == false )
                {
                    return null; 
                }
                if (vis[(int)result.first.X, (int)result.first.Y] == true)
                {
                    return null; 
                }
                vis[(int)result.first.X, (int)result.first.Y] = true;
                path.Add(new pair<brick,pair<string,string>>(bricks[(int)result.first.X,(int)result.first.Y],new pair<string,string>(result.second,getComplement(result.first,result.second))));

                if (result.first == endPos)
                {
                    return path;
                }
                result = goNext(result.first, result.second);

            }
        }

        /// <summary>
        /// get the other end of a brick with the first one known and its index
        /// </summary>
        /// <param name="current">
        /// index of a brick
        /// </param>
        /// <param name="direction">
        /// the first end
        /// </param>
        /// <returns></returns>
        string getComplement(Vector2 current , string direction)
        {
            string name = bricks[(int)current.X, (int)current.Y].getName();
            name = name.Split('\\')[1];
            string[] split = name.Split('_');
            if (split[0] == direction) return split[1];
            return split[0]; 
        }
        /// <summary>
        /// move from a brick to another with the current brick and its direction known
        /// </summary>
        /// <param name="current">
        /// current brick index
        /// </param>
        /// <param name="direction">
        /// current direction
        /// </param>
        /// <returns></returns>
        pair <Vector2, string> goNext(Vector2 current, string direction)
        {
            if ( isValidPos(current) == false)
            {
                return null;
            }
            string complement = getComplement(current, direction);
            string opposite = level.getOpposite(complement);
            if (complement == "left")
            {
                current.Y--; 
            }
            else if (complement == "right")
            {
                current.Y++;
            }
            else if (complement == "up")
            {
                current.X--;
            }
            else if (complement == "down")
            {
                current.X++;
            }
            else
            {
                return null;
            }
            if ( isValidPos(current) == false )
            {
                return null;
            }
            if (bricks[(int)current.X, (int)current.Y].getName().Contains(opposite) == false)
            {
                return null;
            }
            return new pair<Vector2, string>(current, opposite);
        }
        /// <summary>
        /// get a position of the first click with mouse
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 setPrevPos(int x, int y)
        {
            if (bricks[y, x] != null)
            {
                string brickName = bricks[y, x].getName();
                if (bricks[y, x].isMovable() && brickName.Contains("back") == false)
                {
                    bricks[y, x].changeSelectState();
                    bricks[y, x].changeStarSelectState();
                    return new Vector2((bricks[y, x].getPosition().X-FindThePathGame.frameLen) / brick.brickWidth, (bricks[y, x].getPosition().Y-FindThePathGame.frameLen) / brick.brickHeight);
                }
            }
            return new Vector2(-1, -1);
        }
        /// <summary>
        /// moves a brick is possible with new position and old position known
        /// </summary>
        /// <param name="x">
        /// new x position
        /// </param>
        /// <param name="y">
        /// new y position
        /// </param>
        /// <param name="prev">
        /// last position
        /// </param>
        public void brickMove(int x, int y, Vector2 prev)
        {
            bricks[(int)prev.Y, (int)prev.X].changeSelectState();
            bricks[(int)prev.Y, (int)prev.X].changeStarSelectState();
            if (Math.Abs(prev.X - x) + Math.Abs(prev.Y - y) != 1)
            {
                return; 
            }
            if (bricks[y, x] != null)
            {
                string brickName = bricks[y, x].getName();
                
                if (brickName.Contains("back_n") == true)
                {
                    bricks[y, x].move(new Vector2(prev.X * brick.brickWidth, prev.Y * brick.brickHeight));
                    bricks[y, x].updateStarPos();
                    bricks[(int)prev.Y, (int)prev.X].move(new Vector2(x * brick.brickWidth, y * brick.brickHeight));
                    bricks[(int)prev.Y, (int)prev.X].updateStarPos();
                    entity.swap(ref bricks[y, x], ref bricks[(int)prev.Y, (int)prev.X]);
                    bricks[y, x].playSoundEffect(); 
                    updateList();
                }
            }

        }
        /// <summary>
        /// move a player ( after a path has been made ) 
        /// </summary>
        /// <param name="path"></param>
        public void playerMovement(List<pair<brick, pair<string, string>>> path)
        {
            int xPosition =(int) ((int)((plyer.getPosition().X-FindThePathGame.frameLen)  / brick.brickWidth) * brick.brickWidth);
            int yPosition = (int)((int)((plyer.getPosition().Y-FindThePathGame.frameLen)  / brick.brickHeight) * brick.brickHeight);
            xPosition += (int)FindThePathGame.frameLen;
            yPosition += (int)FindThePathGame.frameLen;
            foreach (pair<brick, pair<string, string>> item in path)
            {
                if (item.first.getPosition() == new Vector2(xPosition, yPosition))
                {
                    
                    plyer.move(item.second.first, item.second.second,item.first.getPosition());
                    break;
                }

            }
        }
        public int getLevelNumber()
        {
            return lvl.getLeveLNumber();
        }
        /// <summary>
        /// make all the bricks disappears but the path 
        /// </summary>
        /// <param name="path"></param>
        public void UnloadContent(List<pair<brick, pair<string, string>>> path)//new
        {
            bool check;
            foreach (brick b in bricks)
            {
                check = false;
                foreach (pair<brick, pair<string, string>> part in path)
                {
                    if (b.getPosition().X == part.first.getPosition().X && b.getPosition().Y == part.first.getPosition().Y)
                    {
                        check = true;
                    }
                }
                if (check == false)
                {
                    if (b.hasStar())
                        b.UnloadStar();
                    b.setColor(Color.Transparent);
                }
            }
        }
    }
}
