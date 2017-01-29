using System;
using System.Collections.Generic;
using System.Linq;

namespace Shvetsov_Int_knowl_lab_4.Figures
{
    public enum Figures
    {
        BISHOP, KING, KNIGHT, PAWN, QUEEN, ROOK
    }

    public static class FigureMoveRules
    {
        static Dictionary<Figures, HashSet<ProductiveRule>> moveRules;
        //bool for IS WHITE OR NOT
        static Dictionary<bool, HashSet<ProductiveRule>> moveRulesForPawns;
        static Figures currentFigure;
        static bool isPawnWhite;

        const int COUNT_OF_FIGURES = 6;

        static FigureMoveRules()
        {
            moveRules = new Dictionary<Figures, HashSet<ProductiveRule>>(COUNT_OF_FIGURES - 1);
            moveRulesForPawns = new Dictionary<bool, HashSet<ProductiveRule>>(2);
        }

        public static void InitializeRules(Figure figure)
        {
            currentFigure = ConvertFigureToEnum(figure);

            if (currentFigure == Figures.PAWN)
            {
                isPawnWhite = figure.IsWhiteFigure;
                if (moveRulesForPawns.ContainsKey(isPawnWhite))
                    return;

                moveRulesForPawns.Add(isPawnWhite, new HashSet<ProductiveRule>());
                CalculateAllRules(figure);
            }
            else
            {
                if (moveRules.ContainsKey(currentFigure))
                    return;

                moveRules.Add(currentFigure, new HashSet<ProductiveRule>());
                CalculateAllRules(figure);
            }
        }

        static void CalculateAllRules(Figure figure)
        {
            int boardSize = ChessBoard.INSTANCE.BoardSize;
            BoardCell cell;

            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column++)
                {
                    cell = ChessBoard.INSTANCE[row, column];
                    var movesFromCurPosition = CheckPossibleMovesFromPosition(figure, cell);
                    AddRules(movesFromCurPosition);
                }
            }
        }

        public static IEnumerable<ProductiveRule> CheckPossibleMovesFromPosition(this Figure figure, BoardCell cell)
        {
            var position = cell;

            for (int row = 0; row < ChessBoard.INSTANCE.BoardSize; row++)
            {
                for (int column = 0; column < ChessBoard.INSTANCE.BoardSize; column++)
                {
                    if (figure.CheckMove(position, ChessBoard.INSTANCE[row, column]))
                        yield return new ProductiveRule(position, ChessBoard.INSTANCE[row, column], row + column);
                }
            }
        }


        public static Figures ConvertFigureToEnum(Figure figure)
        {
            Figures type = Figures.KNIGHT;

            if (figure.GetType() == typeof(Bishop))
                type = Figures.BISHOP;
            else if (figure.GetType() == typeof(King))
                type = Figures.KING;
            else if (figure.GetType() == typeof(Knight))
                type = Figures.KNIGHT;
            else if (figure.GetType() == typeof(Pawn))
                type = Figures.PAWN;
            else if (figure.GetType() == typeof(Queen))
                type = Figures.QUEEN;
            else if (figure.GetType() == typeof(Rook))
                type = Figures.ROOK;

            return type;
        }

        public static HashSet<ProductiveRule> GetMoveRules(Figure figure)
        {
            Figures type = ConvertFigureToEnum(figure);
            if(type != Figures.PAWN)
                return GetMoveRules(type);
            else
            {
                bool whitePawn = figure.IsWhiteFigure;
                return moveRulesForPawns[whitePawn];
            }
        }

        public static HashSet<ProductiveRule> GetMoveRules(Figures figure)
        {
            if (moveRules.ContainsKey(figure))
                return moveRules[figure];
            else
                throw new Exception("no such figure in hashset");
        }

        public static IEnumerable<ProductiveRule> GetMoveRulesFromPosition(Figure figure, BoardCell cell)
        {
            var moves = GetMoveRules(figure);

            foreach(var rule in moves)
            {
                if (rule.LeftHandSideRule == cell)
                    yield return rule;
            }
        }

        static void AddRules(IEnumerable<ProductiveRule> rules)
        {
            if (currentFigure != Figures.PAWN)
            {
                foreach (var rule in rules)
                    moveRules[currentFigure].Add(rule);
            }
            else
                foreach (var rule in rules)
                    moveRulesForPawns[isPawnWhite].Add(rule);
        }

        public static bool CheckIfNothingOnTheSides(BoardCell from, BoardCell to)
        {
            int incrementRow = to.RowIndex.CompareTo(from.RowIndex);
            int incrementColumn = to.ColumnIndex.CompareTo(from.ColumnIndex);

            return CheckMoveBlockDiagonal(from, to, incrementRow, incrementColumn);

        }

        static bool CheckMoveBlockDiagonal(BoardCell from, BoardCell to, int incrementRow, int incrementColumn)
        {
            int length = Math.Abs(incrementRow) > Math.Abs(incrementColumn) ? 
                Math.Abs(from.RowIndex - to.RowIndex) : Math.Abs(from.ColumnIndex - to.ColumnIndex);

            for (int i = 1; i < length; i++)
            {
                if (ChessBoard.INSTANCE[from.RowIndex + i * incrementRow, from.ColumnIndex + i * incrementColumn].IsFigureOnCell())
                    return false;
            }

            return true;
        }

        static public bool CheckIfNothingOnTheDiagonal(BoardCell from, BoardCell to)
        {
            int incrementRow = from.RowIndex > to.RowIndex ? -1 : 1;
            int incrementColumn = from.ColumnIndex > to.ColumnIndex ? -1 : 1;

            return CheckMoveBlockDiagonal(from, to, incrementRow, incrementColumn);
        }
    }
}
