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


using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Find_The_Path
{
    /// <summary>
    /// a ball moving automatically
    /// </summary>
    class autoMove:player
    {
        /// <summary>
        /// radius for moving the ball ( circle equation : r^2 = x^2 + y^2 ) 
        /// </summary>
        const float radiusUp = 75;
        const float radiusUpSquare = radiusUp * radiusUp;
        
        public autoMove()
        {
            this.name += "ball";
            this.movability = new movable();
        }
        
        /// <summary>
        /// move the ball depending on the direction and the position
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="brickPos"></param>
        public override void move(string from, string to , Vector2 brickPos)
        {
            if (from == "st")
            {
                if (to == "right")
                    position += new Vector2(velosity, 0);
                else if (to == "left")
                    position += new Vector2(-velosity, 0);
                else if (to == "down")
                    position += new Vector2(0, velosity);
                else
                    position += new Vector2(0, -velosity);
            }
            else if (to == "en")
            {
                if (from == "left")
                {
                    position += new Vector2(velosity, 0);
                    if ((int)position.X == brickPos.X + brick.brickWidth / 3)
                    {
                        FindThePathGame.gameState = "cheer"; 
                    }
                }
                else if (from == "right")
                {
                    position += new Vector2(-velosity, 0);
                    if ((int)position.X == brickPos.X + brick.brickWidth / 2)
                    {
                        FindThePathGame.gameState = "cheer";
                    }
                }
                else if (from == "down")
                {
                    position += new Vector2(0, -velosity);
                    if ((int)position.Y == brickPos.Y + brick.brickHeight / 2)
                    {
                        FindThePathGame.gameState = "cheer";
                    }
                }
                else
                {
                    position += new Vector2(0, velosity);
                    if ((int)position.Y == brickPos.Y + brick.brickHeight / 2 - 20 )
                    {
                        FindThePathGame.gameState = "cheer";
                    }
                }
            }
            else if (from == "left")
            {
                float xPos = (position.X - brickPos.X) + 20 ;
                if (to == "right" )
                    position += new Vector2(velosity, 0);
                else if (to == "up")
                {
                    if (xPos >= radiusUp)
                    {
                        position += new Vector2(0, -velosity);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        position += new Vector2(velosity, brickPos.Y + yPos - 20 - position.Y);
                        position.X = Math.Max(position.X, 75);
                    }
                }
                else if (to == "down")
                {
                    if (xPos >= radiusUp)
                        position += new Vector2(0, velosity);
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        position += new Vector2(velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
            }
            else if (from == "right")
            {
                float xPos = brick.brickWidth - (position.X - brickPos.X + 20);
                if (to == "left" )
                    position += new Vector2(-velosity, 0);
                else if (to == "up")
                {
                    if (xPos >= radiusUp)
                    {
                        position += new Vector2(0, -velosity);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        position += new Vector2(-velosity, brickPos.Y + yPos - 20 - position.Y);
                    }
                }
                else if (to == "down")
                {
                    if (xPos >= radiusUp)
                        position += new Vector2(0, velosity);
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        position += new Vector2(-velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
            }
            else if (from == "down")
            {
                float xPos = position.X - brickPos.X + 20;
                if (to == "up" )
                    position += new Vector2(0, -velosity);
                else if (to == "left")
                {
                    if (xPos >= radiusUp)
                    {
                        position += new Vector2(-velosity, 0);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        float newY = brickPos.Y + brick.brickHeight - yPos - 20 ;
                        position += new Vector2(-velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
                else if (to == "right")
                {
                    xPos = brick.brickWidth - position.X + brickPos.X - 20; 
                    if (xPos > radiusUp)
                        position += new Vector2(velosity, -velosity);
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        position += new Vector2(velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
            }
            else if (from == "up")
            {
                if (to == "down")
                    position += new Vector2(0, velosity);
                else if (to == "left")
                {
                    float xPos = position.X - brickPos.X + 20;
                    if (xPos >= radiusUp)
                    {
                        position += new Vector2(-velosity, 0);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        float newY = brickPos.Y + yPos - 20 ;
                        if (newY < brickPos.Y)
                            newY += 20; 
                        position += new Vector2(-velosity, newY - position.Y);
                    }
                }
                else if (to == "right")
                {
                    float xPos = brick.brickWidth - (position.X - brickPos.X + 20); 
                    if (xPos >= radiusUp)
                        position += new Vector2(velosity, velosity);
                    else
                    {
                        float yPos = (float)Math.Sqrt(radiusUpSquare - xPos * xPos);
                        float newY = brickPos.Y + yPos - 20;
                        if (newY < brickPos.Y)
                            newY += 20;
                        position += new Vector2(velosity, newY - position.Y);
                    }
                }
            }
        }
    }
}
 