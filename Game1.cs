using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Cell cell;


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
    }

    protected override void Initialize()
    {
        cell = new Cell(0, 0, 30);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Cell.texture2D = Content.Load<Texture2D>("cell");
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
            if (cell.area.Contains(mousePosition))
            {
                Exit();
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Colors["background"]);

        cell.Draw((_spriteBatch));

        base.Draw(gameTime);
    }
    
    private void Window_ClientSizeChanged(object sender, System.EventArgs e)
    {
        Window.ClientSizeChanged -= Window_ClientSizeChanged;
        _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width < 100 ? 100 : Window.ClientBounds.Width;
        _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height < 100 ? 100 : Window.ClientBounds.Height;
        _graphics.ApplyChanges();
        Window.ClientSizeChanged += Window_ClientSizeChanged;
    }
}