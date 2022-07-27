using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Field
{
    private int nRows, nColumns;
    private List<List<Cell>> _field;
    public static Texture2D Texture2D;
    private Rectangle area;
    private Rectangle fieldSize;

    public Field(int nRows, int nColumns)
    {
        this.nRows = nRows;
        this.nColumns = nColumns;
        _field = new List<List<Cell>>();
        for (int row = 0; row < nRows; row++)
        {
            _field.Add(new List<Cell>());
            for (int column = 0; column < nColumns; column++)
            {
                _field[row].Add(new Cell(column, row));
            }
        }

        fieldSize = new Rectangle(
            0, 0,
            nColumns * Cell.Size + (nColumns + 1) * Cell.DistanceBetweenCells,
            nRows * Cell.Size + (nRows + 1) * Cell.DistanceBetweenCells
        );
        UpdatePosition(new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(Field.Texture2D, area, Color.White);
        spriteBatch.End();
    }

    public void UpdatePosition(Rectangle windowSize)
    {
        area = new Rectangle(
            (windowSize.Width - fieldSize.Width) / 2,
            (windowSize.Height - fieldSize.Height) / 2,
            fieldSize.Width, fieldSize.Height
        );
    }
}