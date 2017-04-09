using System;
using System.Collections.Generic;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    class Queen : Figure
    {
        public Queen(BoardCell cell, bool isWhiteFigure) : base(cell, isWhiteFigure)
        {
        }

        public override bool CheckMove(BoardCell from, BoardCell to)
        {
            if (from == to)
                return false;

            bool isValidMove = false;
            bool rightLeftMove = (from.RowIndex == to.RowIndex);
            bool topBottomMove = (from.ColumnIndex == to.ColumnIndex);
            bool diagonalMove = Math.Abs(to.RowIndex - from.RowIndex) == Math.Abs(to.ColumnIndex - from.ColumnIndex);

            if ((rightLeftMove || topBottomMove) && FigureMoveRules.CheckIfNothingOnTheSides(from, to))
                isValidMove = true;
            else if (diagonalMove && FigureMoveRules.CheckIfNothingOnTheDiagonal(from,to))
                isValidMove = true;

            return isValidMove;
        }
    }
}
