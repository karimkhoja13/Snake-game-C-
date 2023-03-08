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


namespace Snake_Assignment02
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StartScreen : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public StartScreen(Game game,
        SpriteBatch spriteBatch,
        SpriteFont spriteFont,
        Texture2D image)
        : base(game, spriteBatch)
        {
            string[] menuItems = { "Start Game", "High Scores", "Exit the Game" };
            menuComponent = new MenuComponent(game, spriteBatch, spriteFont, menuItems);
            Components.Add(menuComponent);
            this.image = image;
            imageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, imageRectangle, Color.White);
            base.Draw(gameTime);
        }
    }
}