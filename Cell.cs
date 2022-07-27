using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Cell
{
    public static Texture2D texture2D;
    public Rectangle area;

    public Cell(int x, int y, int size)
    {
        area = new Rectangle(x, y, size, size);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(Cell.texture2D, area, Color.White);
        spriteBatch.End();
    }
}