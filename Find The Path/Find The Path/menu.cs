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
    /// the main class that conrols the game 
    /// </summary>
    class menu
    {
        // the first mouse click to get the selected brick 
        private Vector2 _prevPos;
        private int _clickNumber = 0;
        // hold the path if found
        List<pair<brick, pair<string, string>>> path;
        // the list of objects to be sent to gui to be loaded and drown 
        private List<object> objects;
        private entity backGroundMenu;
        private entity loadingPicture;
        private entity chooseLevelPicture;
        private entity playingPicture;
        private entity creditsPicture;
        private List<chooseLevelButton> chooseLevelButtons; 
        private circle loadingCircle;
        private playButton playBtn;
        private exitButton exitBtn;
        private backButton backBtn;
        private resetButton resetBtn;
        private nextButton nextBtn; 
        private creditsButton creditsBtn;
        private settingsButton settingsBtn;
        private gui guiFunction;
        private MouseState mouseState;
        private MouseState previousMouseState;
        private playGame game; 
        private const int levelCount = 5 ; 
        public menu() { }
        public menu(SpriteBatch spriteBatch , ContentManager content )
        {
            backGroundMenu = new entity("backGroundMenu", new Vector2(0, 0), 990f, 150 * 4f + FindThePathGame.frameLen * 2); // need to be changed the height!
            loadingCircle = new circle("loadingCircle", new Vector2(850, 290), 166f, 166f);
            loadingPicture = new entity("loadingPicture", new Vector2(0, 0), 990f, 150 * 4f + FindThePathGame.frameLen * 2);
            chooseLevelPicture = new entity("chooseLevelPicture", new Vector2(0, 0), 990f, 150 * 4f + FindThePathGame.frameLen * 2);
            playingPicture = new entity("playing", new Vector2(0, 0), 990, 150 * 4f + FindThePathGame.frameLen * 2);
            creditsPicture = new entity("credits", new Vector2(0, 0), 990, 150 * 4f + FindThePathGame.frameLen * 2);
            playBtn = playButton.getPlayButton("play", new Vector2(280, 160), 300f, 100f);
            creditsBtn = creditsButton.getCreditsButton("creditsButton", new Vector2(280, 290), 300f, 100f);
            exitBtn = exitButton.getExitButton("exit", new Vector2(280, 420), 300f, 100f);
            backBtn = backButton.getBackButton("backToMenu", new Vector2(850, 50), 78f, 60f);
            resetBtn = resetButton.getResetButton("resetButton", new Vector2(700, 50), 75f, 75f);
            nextBtn = nextButton.getNextButton("nextButton", new Vector2(780, 150), 78f, 60f);
            settingsBtn = new settingsButton();
            chooseLevelButtons = new List<chooseLevelButton>(); 
            objects = new List<object>();
            updateListMenu();
            guiFunction = new gui(spriteBatch, content , objects);
            mouseState = Mouse.GetState();
            game = new playGame();
        }
        private void changeAudio()
        {
            MediaPlayer.Stop();
            guiFunction.setAudio();
        }
        /// <summary>
        /// update the objects to be drown list into the gui 
        /// </summary>
        private void reinitialize()
        {
            guiFunction.updateList(objects);
            guiFunction.loadContent();
        }
        /// <summary>
        /// update the objects to be drown for a running level ( game ) 
        /// </summary>
        private void updateListGame()
        {
            objects = new List<object>();
            objects.Add(playingPicture);
            objects.AddRange(game.getList());
            objects.Add(backBtn);
            objects.Add(resetBtn);
        }
        /// <summary>
        /// update the objects to be drown for menu
        /// </summary>
        private void updateListMenu()
        {
            objects = new List<object>();
            objects.Add(backGroundMenu);
            objects.Add(playBtn);
            objects.Add(exitBtn);
            objects.Add(creditsBtn);
        }
        /// <summary>
        /// update the objects to be drown for loading
        /// </summary>
        private void updateListLoading()
        {
            objects = new List<object>();
            objects.Add(loadingPicture);
            objects.Add(loadingCircle);
        }
        /// <summary>
        /// update the objects to be drown for credits
        /// </summary>
        private void updateListCredits()
        {
            objects = new List<object>();
            objects.Add(creditsPicture);
            objects.Add(backBtn);
        }
        /// <summary>
        /// update the objects to be drown for choosing level
        /// </summary>
        private void updateChooseLevel()
        {
            objects = new List<object>();
            objects.Add(chooseLevelPicture);
            objects.Add(new chooseLevelButton("level1", new Vector2(100, 220), 209f, 119f));
            objects.Add(new chooseLevelButton("level2", new Vector2(400, 220), 209f, 119f));
            objects.Add(new chooseLevelButton("level3", new Vector2(700, 220), 209f, 119f));
            objects.Add(new chooseLevelButton("level4", new Vector2(100, 400), 209f, 119f));
            objects.Add(new chooseLevelButton("level5", new Vector2(400, 400), 209f, 119f));
            objects.Add(new chooseLevelButton("levelR", new Vector2(700, 400), 209f, 119f));
            objects.Add(backBtn);
        }
        public void draw()
        {
            guiFunction.draw();   
        }
        /// <summary>
        /// start a game depended on a level number
        /// </summary>
        /// <param name="num">
        /// level number
        /// </param>
        private void startGame(int num)
        {
            game.initialize(num);
            updateListGame();
            FindThePathGame.gameState = "playinglevel" + num.ToString();
            changeAudio();
            reinitialize();
        }
        public void Update()
        {
            try
            {
                if (FindThePathGame.gameState.Contains("playing") == true) MediaPlayer.Volume = 0.2f;
                else MediaPlayer.Volume = 1;
                previousMouseState = mouseState;
                mouseState = Mouse.GetState();
                if (MediaPlayer.State != MediaState.Playing)
                {
                    if (FindThePathGame.gameState.Contains("loading") == true)
                    {
                        string levelNum = FindThePathGame.gameState;
                        levelNum = levelNum.Remove(0, 7);
                        FindThePathGame.gameState = "playing" + levelNum;
                        levelNum = levelNum.Remove(0, 5);
                        changeAudio();
                        if (levelNum != "R")
                        {
                            startGame(int.Parse(levelNum));
                        }
                        else
                        {
                            startGame(-1);
                        }
                    }

                    MediaPlayer.Play(guiFunction.getAudio());
                }

                if (FindThePathGame.gameState.Contains("playing") == true)
                {

                    updateListGame();
                    path = game.findPath();
                    if (path != null) // @@ 
                    {
                        FindThePathGame.gameState = "ballMovement";
                        changeAudio();
                        game.UnloadContent(path);
                        guiFunction.loadContent();
                    }
                }
                else if (FindThePathGame.gameState.Contains("loading") == true)
                {
                    loadingCircle.increaseRotate();
                    updateListLoading();
                    guiFunction.updateList(objects);
                }
                else if (FindThePathGame.gameState == "menu")
                {
                    updateListMenu();
                }
                else if (FindThePathGame.gameState == "ballMovement")
                {
                    game.playerMovement(path);
                    game.updatePlayerCollider();
                    guiFunction.checkStar(game.getPlayerRectangle());
                    updateListGame();
                    if (FindThePathGame.gameState == "cheer")
                    {
                        changeAudio();
                        objects.Add(nextBtn);
                        reinitialize();
                    }
                }
                guiFunction.updateList(objects);

                if (previousMouseState.LeftButton == ButtonState.Pressed &&
                    mouseState.LeftButton == ButtonState.Released)
                {
                    if (FindThePathGame.gameState.Contains("playing") == true)
                    {

                        //Selecting and moving brack code
                        int xPosition = ((mouseState.X - (int)FindThePathGame.frameLen) / (int)brick.brickWidth);
                        int yPosition = ((mouseState.Y - (int)FindThePathGame.frameLen) / (int)brick.brickHeight);
                        if (xPosition >= 0 && xPosition < 4 && yPosition >= 0 && yPosition < 4)
                        {
                            if (_clickNumber == 0)
                            {
                                _prevPos = game.setPrevPos(xPosition, yPosition);

                                if (_prevPos.X != -1 && _prevPos.Y != -1)
                                    _clickNumber++;
                            }
                            else if (_clickNumber > 0)
                            {
                                _clickNumber = 0;
                                game.brickMove(xPosition, yPosition, _prevPos);
                            }
                        }
                    }

                    string respone = guiFunction.click(mouseState.X, mouseState.Y);
                    if (respone != null)
                    {
                        foreach (object obj in objects)
                        {
                            if (obj.GetType().ToString().ToLower().Contains("button") == true)
                            {
                                button b = (button)obj;
                                if (b.getName() == respone)
                                {
                                    b.work();
                                    changeAudio();

                                    if (FindThePathGame.gameState == "choose")
                                    {
                                        updateChooseLevel();

                                        reinitialize();
                                    }
                                    else if (FindThePathGame.gameState.Contains("loading") == true)
                                    {

                                        updateListLoading();
                                        reinitialize();
                                        MediaPlayer.Play(guiFunction.getAudio());
                                    }
                                    else if (FindThePathGame.gameState == "menu")
                                    {
                                        updateListMenu();
                                        reinitialize();
                                    }
                                    else if (FindThePathGame.gameState == "reset")
                                    {
                                        startGame(game.getLevelNumber());
                                    }
                                    else if (FindThePathGame.gameState == "nextLevel")
                                    {

                                        if (game.getLevelNumber() + 1 > levelCount && game.getLevelNumber() != -1)
                                        {
                                            startGame(-1);
                                        }
                                        else
                                        {
                                            startGame(game.getLevelNumber() + 1);
                                        }

                                    }
                                    else if (FindThePathGame.gameState == "credits")
                                    {
                                        updateListCredits();
                                        reinitialize();
                                    }
                                }
                            }// if type is button

                        }// foreach
                    }// if pressed 
                }// mouse pressed condition
            } // try 
            catch
            {
                FindThePathGame.gameState = "exit";
            }

        }
        
    }
}
