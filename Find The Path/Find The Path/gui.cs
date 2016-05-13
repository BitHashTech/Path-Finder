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
    /// responsible of loading and drawing contents 
    /// </summary>
    class gui
    {
        private AudioManager audioManager;
        private SpriteBatch spriteBatch;
        private ContentManager content; 
        private List<object> objects;
        public gui(){}
        public gui(SpriteBatch spriteBatch ,ContentManager content)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
        }
        /// <summary>
        /// update the list of things that need to be loaded and drown
        /// </summary>
        /// <param name="objects"></param>
        public void updateList(List<object> objects)
        {
            this.objects = objects;
        }
        public void loadContent()
        {
            foreach (entity ent in objects)
            {
                if (ent.getName().ToLower().Contains("star") == true)
                {
                    ent.setSoundEffect(content.Load<SoundEffect>("Sounds\\winSound"));
                }
                else
                {
                    ent.setSoundEffect(content.Load<SoundEffect>("Sounds\\click"));
                }
                ent.setTexture(content.Load<Texture2D>(ent.getName()));
                
            }
            audioManager.setSong(content.Load<Song>(audioManager.getSongPath()));
        }
        public void draw()
        {
            foreach (object obj in objects)
            {
                if (obj.GetType().ToString().Contains("circle") == true)
                {
                    circle cir = (circle)obj ; 
                    spriteBatch.Draw(cir.getImage(),cir.getPosition(),null , cir.getColor(),cir.getRotate(), cir.getCenture(), 1f,SpriteEffects.None,0); 
                }
                else
                {
                    entity ent  = (entity)obj ;
                    spriteBatch.Draw(ent.getImage(), ent.getPosition(), ent.getColor());
                }
            }
        }
        public string click(int xPos , int yPos)
        {
            Rectangle mouseClickRect = new Rectangle(xPos, yPos, 1, 1);
            foreach ( entity ent in objects ) 
            {
                if ( ent.hasCollider() ) 
                {
                    if ( mouseClickRect.Intersects(ent.getRectangle())  == true ) 
                    {
                        return ent.getName() ; 
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// check if a player hit a star 
        /// </summary>
        /// <param name="playerRectangle"></param>
        public void checkStar(Rectangle playerRectangle)
        {
            foreach (entity ent in objects)
            {
                if (ent.getName().ToLower().Contains("star") == true)
                {
                    if (ent.hasCollider())
                    {
                        if (ent.getRectangle().Intersects(playerRectangle) == true)
                        {
                            playGame.starsCount++;
                            ent.changeCollisionState();
                            ent.changeSelectState();
                            ent.playSoundEffect();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// change the sound depending on the game state 
        /// </summary>
        public void setAudio()
        {
            if (FindThePathGame.gameState.Contains("playing") == true || FindThePathGame.gameState == "reset") 
            {
                audioManager = new GamePlayBGS();
            }
            else if (FindThePathGame.gameState == "menu" || FindThePathGame.gameState == "choose" )
            {
                audioManager = new MenuBGS();
            }
            else if (FindThePathGame.gameState.Contains("loading") == true)
            {
                audioManager = new LoadingBGS();
            }
            else if (FindThePathGame.gameState == "ballMovement")
            {
                audioManager = new ballBGS();
            }
            else if (FindThePathGame.gameState == "cheer")
            {
                audioManager = new cheerBGS();// cheer should be
            } 
            else
            {
                audioManager = new MenuBGS();
            }
        }
        public Song getAudio()
        {
            return audioManager.getSong();
        }
    }
}
