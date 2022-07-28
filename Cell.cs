using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Cell
{
    public static Texture2D Texture2D;
    public Rectangle Area;
    public const int Size = 30;

    public bool IsBomb = false,
        IsOpen = false,
        IsFlagged = false;

    public int NearbyBombsCount;

    public const int DistanceBetweenCells = 5,
        DistanceFromEdge = 20;

    public int x, y;

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
        UpdatePosition();
        NearbyBombsCount = 0;
    }

    public void UpdatePosition()
    {
        Area = new Rectangle(
            Field.Area.Left + (DistanceBetweenCells + Size) * x + DistanceFromEdge,
            Field.Area.Top + (DistanceBetweenCells + Size) * y + DistanceFromEdge,
            Size, Size
        );
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Cell.Texture2D, Area, Color.White);
        if (IsOpen)
        {
            spriteBatch.DrawString(Game1.font, NearbyBombsCount.ToString(),
                new Vector2(Area.X, Area.Y), Color.DarkBlue);
        }
        else if (IsFlagged)
        {
            spriteBatch.Draw(Game1.flag, Area, Color.White);
        }
        else
        {
            spriteBatch.Draw(Cell.Texture2D, Area, Color.White);
        }
    }
}