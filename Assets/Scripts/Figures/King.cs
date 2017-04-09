using System;
using Assets.Scripts.Board;
using UnityEngine;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    class King : Figure
    {
        public King(BoardCell cell, bool isWhiteFigure) : base(cell, isWhiteFigure)
        {
            OnFigureRemovedEvent += () =>
            {
                GameData.EndGame(!isWhiteFigure);
            };
        }

        public override bool CheckMove(BoardCell from, BoardCell to)
        {
            if (from == to)
                return false;

            bool isValidMove = false;
            bool rightLeftMove = Math.Abs(from.RowIndex - to.RowIndex) + Math.Abs(from.ColumnIndex - to.ColumnIndex) == 1;
            bool diagonalMove = Math.Abs(from.RowIndex - to.RowIndex) == 1 && Math.Abs(from.ColumnIndex - to.ColumnIndex) == 1;

            if (rightLeftMove || diagonalMove)
                isValidMove = true;

            return isValidMove;
        }
    }
}
