using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Field _field;
    private bool _isGameFieldGenerated;

    public static int windowHeight = 800;
    public static int windowWidth = 800;
    public static Texture2D flag;
    public static SpriteFont font;
    public static readonly Dictionary<string, Color> Colors = new()
    {
        ["background"] = new Color(116, 141, 166),
        ["1"] = new Color(73, 92, 131),
        ["2"] = Color.DarkGreen,
        ["3"] = Color.IndianRed,
        ["4"] = Color.MediumPurple,
        ["5"] = Color.MediumOrchid
    };

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Minesweeper";
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += Window_ClientSizeChanged;

        _graphics.PreferredBackBufferWidth = windowWidth;
        _graphics.PreferredBackBufferHeight = windowHeight;
        _graphics.ApplyChanges();

        _isGameFieldGenerated = false;
    }

    protected override void Initialize()
    {
        _field = new Field(20, 20);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        font = Content.Load<SpriteFont>("font");

        flag = Content.Load<Texture2D>("flag");
        Cell.Texture2DCell = Content.Load<Texture2D>("cell");
        Cell.Texture2DCellOpened = Content.Load<Texture2D>("cell_opened");
        Field.Texture2D = Content.Load<Texture2D>("field");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var mouseState = Mouse.GetState();
        var mousePosition = new Point(mouseState.X, mouseState.Y);
        if (mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed)
        {
            foreach (var cell in _field.field.SelectMany(row => row))
            {
                if (cell.Area.Contains(mousePosition))
                {
                    if (!_isGameFieldGenerated)
                    {
                        _field.GenerateGameField(cell);
                        _isGameFieldGenerated = true;
                    }
                    else if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (!cell.IsOpen)
                        {
                            if (cell.IsBomb)
                                Exit();
                            cell.IsOpen = true;
                        }
                    }
                    else
                    {
                        if (!cell.IsOpen)
                            cell.IsFlagged = true;
                    }
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Colors["background"]);

        _spriteBatch.Begin();
        _field.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void Window_ClientSizeChanged(object sender, System.EventArgs e)
    {
        Window.ClientSizeChanged -= Window_ClientSizeChanged;
        _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width < 400 ? 400 : Window.ClientBounds.Width;
        _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height < 400 ? 400 : Window.ClientBounds.Height;
        _graphics.ApplyChanges();
        Window.ClientSizeChanged += Window_ClientSizeChanged;

        _field.UpdatePosition(Window.ClientBounds);
    }
}