using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Cell
{
    public static Texture2D Texture2D;
    public Rectangle Area;
    public static int Size = 30;
    public static int DistanceBetweenCells = 5;

    public Cell(int x, int y)
    {
        Area = new Rectangle(x, y, Size, Size);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(Cell.Texture2D, Area, Color.White);
        spriteBatch.End();
    }
}