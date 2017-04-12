using UnityEngine;
using System.Collections;

public class CellExample {
    public int column;
    public int row;

    FigureExample figure;
    bool isFigureOnCell;

    public CellExample(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public void RemoveFigure()
    {
        isFigureOnCell = false;
    }

    public void SetFigure(FigureExample figure)
    {
        this.figure = figure;
        isFigureOnCell = true;
    }

    public FigureExample GetFigureOrDefault()
    {
        return figure;
    }

    public bool IsFigureOnCell()
    {
        return isFigureOnCell;
    }

    public CellExample Clone()
    {
        var cell = new CellExample(row, column);
        cell.figure = figure;
        cell.isFigureOnCell = isFigureOnCell;

        return cell;
    }
}
