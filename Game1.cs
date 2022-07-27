﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private Field _field;
    
    public static int windowHeight = 400;
    public static int windowWidth = 400;

    private static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>()
    {
        ["background"] = new Color(116, 141, 166)
    };

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += Window_ClientSizeChanged;
        
        _graphics.PreferredBackBufferWidth = windowWidth;
        _graphics.PreferredBackBufferHeight = windowHeight;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _field = new Field(10, 10);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Cell.Texture2D = Content.Load<Texture2D>("cell");
        Field.Texture2D = Content.Load<Texture2D>("field");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var mouseState = Mouse.GetState();
        var mousePosition = new Point(mouseState.X, mouseState.Y);
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            // if (_cell.Area.Contains(mousePosition))
            // {
            //     Exit();
            // }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Colors["background"]);

        // _cell.Draw(_spriteBatch);
        _field.Draw(_spriteBatch);

        base.Draw(gameTime);
    }
    
    private void Window_ClientSizeChanged(object sender, System.EventArgs e)
    {
        Window.ClientSizeChanged -= Window_ClientSizeChanged;
        _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width < 200 ? 200 : Window.ClientBounds.Width;
        _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height < 200 ? 200 : Window.ClientBounds.Height;
        _graphics.ApplyChanges();
        Window.ClientSizeChanged += Window_ClientSizeChanged;
        
        _field.UpdatePosition(Window.ClientBounds);
    }
}