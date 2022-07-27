using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Field
{
    private int nRows, nColumns;
    public readonly List<List<Cell>> field;
    public static Texture2D Texture2D;
    public static Rectangle Area;
    private readonly Rectangle fieldSize;

    public Field(int nRows, int nColumns)
    {
        this.nRows = nRows;
        this.nColumns = nColumns;
        field = new List<List<Cell>>();
        for (int row = 0; row < nRows; row++)
        {
            field.Add(new List<Cell>());
            for (int column = 0; column < nColumns; column++)
            {
                field[row].Add(new Cell(column, row));
            }
        }

        fieldSize = new Rectangle(
            0, 0,
            nColumns * Cell.Size + (nColumns - 1) * Cell.DistanceBetweenCells + 2 * Cell.DistanceFromEdge,
            nRows * Cell.Size + (nRows - 1) * Cell.DistanceBetweenCells + 2 * Cell.DistanceFromEdge
        );
        UpdatePosition(new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight));
    }

    public void GenerateGameField(Cell clickedCell)
    { // TODO: make a good generate algorithm
        var bombProbability = 60;
        foreach (var cell in field.SelectMany(row => row))
        {
            if (!cell.Equals(clickedCell))
            {
                var random = new Random();
                cell.IsBomb = Convert.ToBoolean(Math.Round(
                    random.Next(bombProbability * 2) / 100.0
                    ));
            }
        }
        CountNearbyBombs();
    }

    private void CountNearbyBombs()
    {
        for (var row = 0; row < nRows; row++)
        {
            for (var col = 0; col < nColumns; col++)
            {
                
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Field.Texture2D, Area, Color.White);
        foreach (var cell in field.SelectMany(row => row))
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
        foreach (var cell in field.SelectMany(row => row))
        {
            cell.UpdatePosition();
        }
    }
}