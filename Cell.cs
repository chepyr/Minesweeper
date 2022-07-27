using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Cell
{
    public static Texture2D Texture2D;
    private Rectangle Area;
    public static int Size = 30;
    public static int DistanceBetweenCells = 5;
    private int x, y;

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Area = new Rectangle(
            Field.Area.Left + DistanceBetweenCells * (x + 1) + x * Size,
            Field.Area.Top + DistanceBetweenCells * (y + 1) + y * Size,
            Size, Size
        );
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Cell.Texture2D, Area, Color.White);
    }
}