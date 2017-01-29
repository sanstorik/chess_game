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
        protected List<ProductiveRule> workRules;
        protected bool isWhiteFigure;

        public event Action<BoardCell> OnFigureMovedEvent;
        public event Action OnFigureRemovedEvent;

        protected Figure(BoardCell currentCell, bool isWhiteFigure)
        {
            this.isWhiteFigure = isWhiteFigure;
            this.currentCell = currentCell;

            FigureMoveRules.InitializeRules(this);

            workRules = new List<ProductiveRule>();

            OnFigureRemovedEvent += () =>
            {
                if (isWhiteFigure)
                    GameData.whiteFiguresRemoved++;
                else
                    GameData.blackFiguresRemoved++;
            };

            OnFigureMovedEvent += (cell) =>
            {
                workRules.Clear();

                workRules = FigureMoveRules.GetMoveRules(this)
                     .Select(rule => rule)
                     .Where(rule => rule.LeftHandSideRule == CurrentCell)
                     .OrderBy(rule => rule.Priority).ToList();
            };
            OnFigureMovedEvent(currentCell);

            OnFigureMovedEvent += (cell) => GameData.ChangeTurn();
        }

        public bool MoveFigure(BoardCell to)
        {
            if (!CheckMove(to))
                return false;

            bool isSecondFigureSameColor;
            InteractWithFigureOnCell(to, out isSecondFigureSameColor);

            if (isSecondFigureSameColor)
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

        private void InteractWithFigureOnCell(BoardCell to, out bool isSecondFigureSameColor)
        {
            isSecondFigureSameColor = false;

            var figureOnCell = to.GetFigureOrDefault();
            if (figureOnCell != null)
            {
                if (figureOnCell.isWhiteFigure == isWhiteFigure)
                    isSecondFigureSameColor = true;
                else
                {
                    isSecondFigureSameColor = false;
                    Figure deletedFigure = to.DeleteFigureFromCell();
                    deletedFigure.OnFigureRemovedEvent();

                    to.SetFigureOnCell(this);
                }
            }
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

        public List<ProductiveRule> WorkRules
        {
            get { return workRules; }
        }

        public IEnumerable<ProductiveRule> GetValidMoves()
        {
            foreach (ProductiveRule rule in workRules)
            {
                var isFigureOnCell = rule.RightHandSideRule.GetFigureOrDefault();

                if ((isFigureOnCell == null || isFigureOnCell.IsWhiteFigure != IsWhiteFigure) && 
                    CheckMove(rule.RightHandSideRule))
                       yield return rule;
            }
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
