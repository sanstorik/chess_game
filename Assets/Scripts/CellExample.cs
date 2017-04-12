using UnityEngine;
using System.Collections;

public class CellExample {
    public int column;
    public int row;
    FigureExample figure;

    public CellExample(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public void RemoveFigure()
    {
        figure = null;
    }

    public void SetFigure(FigureExample figure)
    {
        this.figure = figure;
    }

    public bool IsFigureOnCell()
    {
        return figure == null;
    }
}
