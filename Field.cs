using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Field
{
    private int nRows, nColumns;
    private List<List<Cell>> _field;
    public static Texture2D Texture2D;
    public static Rectangle Area;
    private readonly Rectangle fieldSize;

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

    public void GenerateGameField(Cell clickedCell)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Field.Texture2D, Area, Color.White);
        foreach (var cell in _field.SelectMany(row => row))
        {
            cell.Draw(spriteBatch);
        }
    }

    public void UpdatePosition(Rectangle windowSize)
    {
        Area = new Rectangle(
            (windowSize.Width - fieldSize.Width) / 2,
            (windowSize.Height - fieldSize.Height) / 2,
            fieldSize.Width, fieldSize.Height
        );
        foreach (var cell in _field.SelectMany(row => row))
        {
            cell.UpdatePosition();
        }
    }
}