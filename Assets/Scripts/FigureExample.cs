using UnityEngine;
using System.Collections;
using System;

public class FigureExample{
    bool isRed;
    public int row;
    public int column;

    public FigureExample(bool isRed, int row, int column)
    {
        this.isRed = isRed;
        this.row = row;
        this.column = column;
    }

    public bool IsRed()
    {
        return isRed;
    }

    public bool IsPossibleMove(BoardExample board, CellExample from, CellExample to)
    {
        if (from.row == to.row &&
            from.column == to.column)
            return false;

        bool rightLeftMove = Math.Abs(from.row - to.row) + Math.Abs(from.column - to.column) == 1;
        bool diagonalMove = Math.Abs(from.row - to.row) == 1 && Math.Abs(from.column - to.column) == 1;

        if (rightLeftMove || diagonalMove && ( from.row == to.row || from.column == to.column ))
            return true;

        if (
            board[from.row - GetRowDiff(from, to), from.column - GetColumnDiff(from, to)].IsFigureOnCell() &&
            board[from.row - GetRowDiff(from, to), from.column - GetColumnDiff(from, to)].IsFigureOnCell() &&
            board[to.row + GetRowDiff(from, to), to.column + GetColumnDiff(from, to)]
            .Equals(board[from.row - GetRowDiff(from, to), from.column - GetColumnDiff(from, to)]) &&
            ( GetColumnDiff(from, to) == 0 || GetRowDiff(from, to) == 0 )
            )
            return true;

        return false;
    }

    public FigureExample Clone()
    {
        return new FigureExample(isRed, row, column);
    }

    int GetColumnDiff(CellExample from, CellExample to)
    {
        return from.column.CompareTo(to.column);
    }

    int GetRowDiff(CellExample from, CellExample to)
    {
        return from.row.CompareTo(to.row);
    }
}
