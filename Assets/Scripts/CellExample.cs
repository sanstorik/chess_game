﻿using UnityEngine;
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
        figure.row = row;
        figure.column = column;
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
        if (figure != null)
            cell.figure = figure.Clone();
        else
            cell.figure = null;

        cell.isFigureOnCell = isFigureOnCell;

        return cell;
    }
}
