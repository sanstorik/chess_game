using System;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    class Knight : Figure
    {
        public Knight(BoardCell cell, bool isWhiteFigure) : base(cell, isWhiteFigure)
        {
        }

        public override bool CheckMove(BoardCell from, BoardCell to)
        {
            if (from == to)
                return false;
            bool isValidMove = false;

            bool firstVariantOfMove = Math.Abs(to.RowIndex - from.RowIndex) == 1 &&
                    Math.Abs(to.ColumnIndex - from.ColumnIndex) == 2;
            bool secondVariantOfMove = Math.Abs(to.RowIndex - from.RowIndex) == 2 &&
                    Math.Abs(to.ColumnIndex - from.ColumnIndex) == 1;

            if (firstVariantOfMove || secondVariantOfMove)
                isValidMove = true;

            return isValidMove;
        }
    }
}
