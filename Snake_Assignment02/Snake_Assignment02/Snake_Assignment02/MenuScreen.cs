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
using System.Text;

namespace Snake_Assignment02
{
    class MenuScreen
    {
        //    GraphicsDeviceManager graphics;
        //    string[] menuItems;
        public int selectedIndex;
        //    Color normal = Color.White;
        //    Color hilite = Color.Yellow;
        //    KeyboardState keyboardState;
        //    KeyboardState oldKeyboardState;
        //    SpriteBatch spriteBatch;
        //    SpriteFont spriteFont;
        //    Vector2 position;
        //    float width = 0f;
        //    float height = 0f;
        //    public int SelectedIndex
        //    {
        //        get { return selectedIndex; }
        //        set
        //        {
        //            selectedIndex = value;
        //            if (selectedIndex < 0)
        //                selectedIndex = 0;
        //            if (selectedIndex >= menuItems.Length)
        //                selectedIndex = menuItems.Length - 1;
        //        }
        //    }

        //    public MenuScreen()
        //    {
        //        string[] menuItems = { "Start Game", "High Scores", "Exit the Game" };
        //        MeasureMenu();
        //    }

        //    public void LoadContent(ContentManager Content)
        //    {
        //        foodTexture = Content.Load<Texture2D>("food");
        //    }

        //    public void Draw(SpriteBatch spriteBatch)
        //    {
        //        spriteBatch.Draw(foodTexture, new Rectangle(x, y, 15, 15), Color.White);
        //    }

        //    //Update
        //    public void Update(GameTime gameTime)
        //    {
        //        KeyboardState keyboardstate = Keyboard.GetState();
        //        if (keyboardstate.IsKeyDown(Keys.Space))
        //        {
        //            x = rnum1.Next(1, 600);
        //            x = (int)(x / 20) * 20 + 10;
        //            y = rnum1.Next(1, 400);
        //            y = (int)(y / 20) * 20 + 10;
        //        }
        //    }

        //    private void MeasureMenu()
        //    {
        //        height = 0;
        //        width = 0;
        //        foreach (string item in menuItems)
        //        {
        //            Vector2 size = spriteFont.MeasureString(item);
        //            if (size.X > width)
        //                width = size.X;
        //            height += spriteFont.LineSpacing + 5;
        //        }
        //        position = new Vector2(
        //        (graphics.PreferredBackBufferWidth - width) / 2,
        //        (graphics.PreferredBackBufferHeight - height) / 2);

        //    }
    }
}
