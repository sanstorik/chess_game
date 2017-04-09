
using System;
using UnityEngine;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    class Pawn : Figure
    {
        public event Action OnPawnChangeToQueenEvent;
        public Pawn(BoardCell cell, bool isWhiteFigure) : base(cell, isWhiteFigure)
        {
            OnFigureMovedEvent += (movedOnCell) =>
            {
                if ((IsWhiteFigure && movedOnCell.RowIndex == 0) ||
                      (!IsWhiteFigure && movedOnCell.RowIndex == ChessBoard.INSTANCE.BoardSize - 1))
                {
                    OnPawnChangeToQueenEvent();
                }
            };
        }

        public override bool CheckMove(BoardCell from, BoardCell to)
        {
            if (from == to)
                return false;

            bool isValidMove = false;
            int incrementOnMove = IsWhiteFigure ? -1 : 1;

            bool moveForward = to.RowIndex - from.RowIndex == incrementOnMove && to.ColumnIndex == from.ColumnIndex &&
                !to.IsFigureOnCell();
            bool moveFromStartingPosition = to.RowIndex - from.RowIndex == incrementOnMove * 2 && to.ColumnIndex == from.ColumnIndex &&
                !to.IsFigureOnCell() && !ChessBoard.INSTANCE[from.RowIndex + incrementOnMove, from.ColumnIndex].IsFigureOnCell();

            bool killingOtherFigure = to.RowIndex - from.RowIndex == incrementOnMove &&
                Mathf.Abs(to.ColumnIndex - from.ColumnIndex) == 1 && to.IsFigureOnCell();

            if (moveForward || killingOtherFigure)
                    isValidMove = true;
            else if ((from.RowIndex == 1 || from.RowIndex == ChessBoard.INSTANCE.BoardSize - 2) &&
                moveFromStartingPosition)
                    isValidMove = true;

            return isValidMove;
        }
    }
}
