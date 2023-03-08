using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Collections;

namespace Snake_Assignment02
{
    //The enumeration of the various screen states available in the game
    enum ScreenState
    {
        Title,
        Main,
        End
    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool startScreen;
        bool gameScreen;
        bool end;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        //The current screen state
        ScreenState currentScreen;
        SpriteFont fonts;
        ArrayList elementY = new ArrayList();
        ArrayList elementX = new ArrayList();
        ArrayList foodY = new ArrayList();
        ArrayList foodX = new ArrayList();
        Texture2D mainBackground, titleBackground;
        Texture2D face;
        Texture2D food;
        Texture2D snakeBody;
        String direction;
        int screenHeight;
        int screenWidth;
        int updates = 0;
        int speed = 10;
        int score = 0;
        Rectangle screenCover;
        String gameOverMsg;
        SoundEffectInstance soundInstance1;
        SoundEffectInstance soundInstance2;
        SoundEffectInstance soundInstance3;
        SoundEffect sound1;
        SoundEffect sound2;
        SoundEffect sound3;

        int stop = 0;
        int check = 150;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 550;
            graphics.PreferredBackBufferHeight = 400;
            graphics.ApplyChanges();
            screenHeight = graphics.PreferredBackBufferHeight / 15;
            screenWidth = graphics.PreferredBackBufferWidth / 15;
            Reset();

            screenCover = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            gameOverMsg = "The game is over...";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            titleBackground = Content.Load<Texture2D>("alienmetal");
            mainBackground = Content.Load<Texture2D>("greenmetal");
            face = Content.Load<Texture2D>("face");
            snakeBody = Content.Load<Texture2D>("Snake_body");
            food = Content.Load<Texture2D>("food");
            fonts = Content.Load<SpriteFont>("Font");
            sound2 = Content.Load<SoundEffect>("hyperspace_activate");
            soundInstance2 = sound2.CreateInstance();
            sound1 = Content.Load<SoundEffect>("rattlesnakerattle");
            soundInstance1 = sound1.CreateInstance();
            sound3 = Content.Load<SoundEffect>("snakehit");
            soundInstance3 = sound3.CreateInstance();
            currentScreen = ScreenState.Title;
            end = false;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            keyboardState = Keyboard.GetState();
            
            if (CheckKey(Keys.Escape))
            {
                Exit();
            }

            switch (currentScreen)
            {
                case ScreenState.Title:
                    {
                        UpdateTitleScreen();
                        break;
                    }
                case ScreenState.Main:
                    {
                        UpdateMainScreen();
                        break;
                    }
                case ScreenState.End:
                    {
                        UpdateEndScreen();
                        break;
                    }
            }
            base.Update(gameTime);
            oldKeyboardState = keyboardState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentScreen)
            {
                case ScreenState.Title:
                    {
                        DrawTitleScreen();
                        break;
                    }

                case ScreenState.Main:
                    {
                        DrawMainScreen();
                        break;
                    }
                case ScreenState.End:
                    {
                        DrawEndScreen();
                        break;
                    }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawTitleScreen()
        {
            spriteBatch.Draw(mainBackground, screenCover, Color.White);
            spriteBatch.DrawString(fonts, "Snakes - Game Development Group Assignment\n", new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(fonts, "Press Enter to Start the Game", new Vector2(10, 75), Color.Yellow);
            spriteBatch.DrawString(fonts, "Press Escape to Exit the Game", new Vector2(10, 95), Color.LightGreen);
            spriteBatch.DrawString(fonts, "How to Play: \n Move the snake seen on the screen with \n arrow keys. \n " + 
            "Use the snake to get to the symbol \n shown below, and run through as many \n of those as possible. \n " + 
            "Don't let the snake run over its own body.", new Vector2(10, 150), Color.White);
            spriteBatch.Draw(food, new Vector2(150, 350), Color.White);
        }

        private void DrawMainScreen()
        {
            //spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);
            spriteBatch.Draw(mainBackground, screenCover, Color.White);
            int i = 0;
            while (i < elementX.Count)
            {
                spriteBatch.Draw(snakeBody, new Rectangle(Convert.ToInt16(elementX[i]) * 15, Convert.ToInt16(elementY[i]) * 15, 15, 15), Color.White);
                i++;
            }

            spriteBatch.Draw(face, new Rectangle((Convert.ToInt16(elementX[0])) * 15, Convert.ToInt16(elementY[0]) * 15, 15, 15), Color.White);
            i = 0;
            while (i < foodX.Count)
            {
                spriteBatch.Draw(food, new Rectangle(Convert.ToInt16(foodX[i]) * 15, Convert.ToInt16(foodY[i]) * 15, 15, 15), Color.White);
                i++;
            }
            if(!end)
                spriteBatch.DrawString(fonts, "score:" + score.ToString(), new Vector2(150, 10), Color.White);
            else
                spriteBatch.DrawString(fonts, gameOverMsg + " \nPress SpaceBar to see your final score !", new Vector2(20, 50), Color.LightSalmon);
        }

        private void DrawEndScreen()
        {
            spriteBatch.Draw(mainBackground, screenCover, Color.White);
            spriteBatch.DrawString(fonts, "Your Final Score Is :" + score.ToString(), new Vector2(25, 50), Color.LightGreen);
            spriteBatch.DrawString(fonts, "Press SpaceBar to return to the Main Menu", new Vector2(25, 70), Color.White);
            spriteBatch.DrawString(fonts, "Press Escape to Close the Game", new Vector2(25, 90), Color.White);
        }

        private void UpdateTitleScreen()
        {
            if (CheckKey(Keys.Enter))
            {
                Reset();
                currentScreen = ScreenState.Main;
            }
        }

        private void UpdateMainScreen()
        {
            soundInstance1.Play();
            soundInstance1.Volume = 0.2f;
            if (CheckKey(Keys.Space))
            {
                currentScreen = ScreenState.End;
            }
            KeyboardState keyboardstate = Keyboard.GetState();
            if (!end)
            {
                if (keyboardstate.IsKeyDown(Keys.Left))
                {
                    if (direction != "r")
                        direction = "l";
                }
                if (keyboardstate.IsKeyDown(Keys.Right))
                {
                    if (direction != "l")
                        direction = "r";
                }

                if (keyboardstate.IsKeyDown(Keys.Up))
                {
                    if (direction != "d")
                        direction = "u";
                }
                if (keyboardstate.IsKeyDown(Keys.Down))
                {
                    if (direction != "u")
                        direction = "d";
                }
            }
            if (keyboardstate.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
                
            if (updates == speed)
            {
                if (direction == "d")
                {
                    elementX.Insert(0, elementX[0]);
                    elementX.RemoveAt(elementX.Count - 1);
                    elementY.Insert(0, Convert.ToInt16(elementY[0]) + 1);
                    elementY.RemoveAt(elementY.Count - 1);
                }
                if (direction == "u")
                {
                    elementX.Insert(0, elementX[0]);
                    elementX.RemoveAt(elementX.Count - 1);
                    elementY.Insert(0, Convert.ToInt16(elementY[0]) - 1);
                    elementY.RemoveAt(elementY.Count - 1);
                }
                if (direction == "l")
                {
                    elementX.Insert(0, Convert.ToInt16(elementX[0]) - 1);
                    elementX.RemoveAt(elementX.Count - 1);
                    elementY.Insert(0, elementY[0]);
                    elementY.RemoveAt(elementY.Count - 1);
                }
                if (direction == "r")
                {
                    elementX.Insert(0, Convert.ToInt16(elementX[0]) + 1);
                    elementX.RemoveAt(elementX.Count - 1);
                    elementY.Insert(0, elementY[0]);
                    elementY.RemoveAt(elementY.Count - 1);
                }
                updates = 0;
            }
            else
            {
                updates++;
            }
            int i = 0;
            while (i < foodX.Count)
            {
                if (foodX[i].ToString() == elementX[0].ToString() && foodY[i].ToString() == elementY[0].ToString())
                {
                    soundInstance2.Play();
                    foodX.RemoveAt(i);
                    foodY.RemoveAt(i);

                    elementX.Add(-1);
                    elementY.Add(-1);
                    score+=10;
                    Random ranFood = new Random();
                    foodX.Add(ranFood.Next(0, graphics.PreferredBackBufferWidth / 15));
                    foodY.Add(ranFood.Next(0, graphics.PreferredBackBufferHeight / 15));
                }
                i++;
            }
            i = 2;
            while (i < elementX.Count)
            {
                if (elementX[0].ToString() == elementX[i].ToString() && elementY[0].ToString() == elementY[i].ToString())
                {
                    soundInstance1.Stop();
                    soundInstance3.Volume = 0.3f;
                    direction = "";
                    end = true;
                    if (stop == check)
                        soundInstance3.Stop();
                    else
                    {
                        stop++;
                        soundInstance3.Play();
                    }
                    break;
                }
                i++;
            }
            if (Convert.ToInt16(elementX[0]) < 0)
            {
                elementX[0] = graphics.PreferredBackBufferWidth / 15;
            }
            if (Convert.ToInt16(elementX[0]) > graphics.PreferredBackBufferWidth / 15)
            {
                elementX[0] = 0;
            }
            if (Convert.ToInt16(elementY[0]) < 0)
            {
                elementY[0] = graphics.PreferredBackBufferHeight / 15;
            }
            if (Convert.ToInt16(elementY[0]) > graphics.PreferredBackBufferHeight / 15)
            {
                elementY[0] = 0;
            }
        }

        private void UpdateEndScreen()
        {
            if (CheckKey(Keys.Space))
            {
                currentScreen = ScreenState.Title;
            }
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
            oldKeyboardState.IsKeyDown(theKey);
        }

        private void Reset()
        {
            elementX.Clear();
            elementY.Clear();
            foodX.Clear();
            foodY.Clear();

            elementX.Add(2);
            elementY.Add(0);

            elementX.Add(1);
            elementY.Add(0);

            elementX.Add(0);
            elementY.Add(0);

            Random ranFood = new Random();

            foodX.Add(ranFood.Next(0, graphics.PreferredBackBufferWidth / 15));
            foodY.Add(ranFood.Next(0, graphics.PreferredBackBufferHeight / 15));

            score = 0;

            direction = "r";
        }
    }
}