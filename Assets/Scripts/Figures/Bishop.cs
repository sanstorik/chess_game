using System;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    class Bishop : Figure
    {
        public Bishop(BoardCell cell, bool isWhiteFigure) : base(cell, isWhiteFigure)
        {
        }

        public override bool CheckMove(BoardCell from, BoardCell to)
        {
            if (from == to)
                return false;

            bool isValidMove = false;
            bool diagonalMove = Math.Abs(to.RowIndex - from.RowIndex) == Math.Abs(to.ColumnIndex - from.ColumnIndex);

            if (diagonalMove && FigureMoveRules.CheckIfNothingOnTheDiagonal(from, to))
                isValidMove = true;

            return isValidMove;
        }
    }
}
