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
    {
        var bombProbability = 18;
        foreach (var cell in field.SelectMany(row => row))
        {
            if (!cell.Equals(clickedCell) &&
                (Math.Abs(cell.x - clickedCell.x) > 2 || Math.Abs(cell.y - clickedCell.y) > 2))
            {
                var random = new Random();
                cell.IsBomb = Convert.ToBoolean(Math.Round(
                    random.Next(100 - bombProbability) / 100.0
                ));
            }
        }
        CountNearbyBombs();
        OpenNearbyZeroCells(clickedCell.y, clickedCell.x);
    }

    // recursive algorithm for opening nearby zero cells
    private void OpenNearbyZeroCells(int row, int col)
    {
        OpenCell(row, col);
        
        // TODO: Make outbounds checks
        void OpenCell(int row, int col)
        {
            if (field[row][col].NearbyBombsCount == 0 & !field[row][col].IsOpen)
            {
                field[row][col].IsOpen = true;
                for (var i = -1; i <= 1; i++)
                    for (var j = -1; j <= 1; j++)
                        OpenCell(row + i, col + j);
            }
            else
            {
                field[row][col].IsOpen = true;
                return;
            }
        }
    }
    
    private void CountNearbyBombs()
    {
        foreach (var cell in field.SelectMany(row => row))
        {
            if (!cell.IsBomb)
                cell.NearbyBombsCount = CountBombs(cell.x, cell.y);
        }

        int CountBombs(int x, int y)
        {
            var count = 0;
            for (var i = -1; i <= 1; i++)
            for (var j = -1; j <= 1; j++)
                if (OutBounds(i + x, j + y))
                    continue;
                else
                    count += Convert.ToInt32(field[j + y][i + x].IsBomb);

            return count;
        }

        bool OutBounds(int x, int y)
        {
            return x < 0 || y < 0 || x >= nColumns || y >= nRows;
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