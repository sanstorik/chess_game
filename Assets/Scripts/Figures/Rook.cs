
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

            bool isValidMove = false;
            bool rightLeftMove = (from.RowIndex == to.RowIndex);
            bool topBottomMove = (from.ColumnIndex == to.ColumnIndex);

            if ((rightLeftMove || topBottomMove) && FigureMoveRules.CheckIfNothingOnTheSides(from, to))
                isValidMove = true;

            return isValidMove;
        }
    }
}
