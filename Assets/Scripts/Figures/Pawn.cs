
using System;
using UnityEngine;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    class Rook : Figure
    {
        public Rook(BoardCell cell, bool isWhiteFigure) : base(cell, isWhiteFigure)
        {
        }

        public override bool CheckMove(BoardCell from, BoardCell to)
        {
            if (from == to)
                return false;

            bool rightLeftMove = Math.Abs(from.RowIndex - to.RowIndex) + Math.Abs(from.ColumnIndex - to.ColumnIndex) == 1;
            bool diagonalMove = Math.Abs(from.RowIndex - to.RowIndex) == 1 && Math.Abs(from.ColumnIndex - to.ColumnIndex) == 1;

            if (rightLeftMove || diagonalMove && ( from.RowIndex == to.RowIndex || from.ColumnIndex == to.ColumnIndex ))
                return true;

            if (
                ChessBoard.INSTANCE[from.RowIndex - GetRowDiff(from, to), from.ColumnIndex - GetColumnDiff(from, to)] != null &&
                ChessBoard.INSTANCE[from.RowIndex - GetRowDiff(from, to), from.ColumnIndex - GetColumnDiff(from, to)].IsFigureOnCell() &&
                ChessBoard.INSTANCE[to.RowIndex + GetRowDiff(from, to), to.ColumnIndex + GetColumnDiff(from, to)]
                .Equals(ChessBoard.INSTANCE[from.RowIndex - GetRowDiff(from, to), from.ColumnIndex - GetColumnDiff(from, to)])
                && (GetColumnDiff(from, to) == 0 || GetRowDiff(from,to) == 0)
                )
                return true;

            return false;
        }

        int GetColumnDiff(BoardCell from, BoardCell to)
        {
            return from.ColumnIndex.CompareTo(to.ColumnIndex);
        }

        int GetRowDiff(BoardCell from, BoardCell to)
        {
            return from.RowIndex.CompareTo(to.RowIndex);
        }
    }
}
