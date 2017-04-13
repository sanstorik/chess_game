using Assets.Scripts.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    public abstract class Figure
    {
        protected int maxCountOfRules;
        protected BoardCell currentCell;
        protected bool isWhiteFigure;

        public event Action<BoardCell> OnFigureMovedEvent;

        protected Figure(BoardCell currentCell, bool isWhiteFigure)
        {
            this.isWhiteFigure = isWhiteFigure;
            this.currentCell = currentCell;
            OnFigureMovedEvent += (cell) => GameData.ChangeTurn();
        }

        public bool MoveFigure(BoardCell to)
        {
            if (!CheckMove(to) ||
                to.IsFigureOnCell())
                return false;

            currentCell.DeleteFigureFromCell();
            to.SetFigureOnCell(this);
            currentCell = to;

            OnFigureMovedEvent(to);
            return true;
        }

        public void MoveFigureToCellIgnoringRules(BoardCell to)
        {
            currentCell.DeleteFigureFromCell();
            to.SetFigureOnCell(this);
            currentCell = to;
        }

        public abstract bool CheckMove(BoardCell from, BoardCell to);


        /// <summary>
        /// Check if move is possible from current cell
        /// </summary>
        /// <param name="to">go to cell</param>
        /// <returns></returns>
        public bool CheckMove(BoardCell to)
        {
            return CheckMove(currentCell, to);
        }


        public BoardCell CurrentCell
        {
            get { return currentCell; }
        }

        public bool IsWhiteFigure
        {
            get { return isWhiteFigure; }
        }
    }
}
