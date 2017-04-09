using UnityEngine;
using Shvetsov_Int_knowl_lab_4.Figures;
using Shvetsov_Int_knowl_lab_4;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

public class FigureController : MonoBehaviour {

    Figure figure;

    public void CreateFigure(BoardCell cell, bool isWhiteFigure, Figures type)
    {
        transform.parent = Board.INSTANCE.figuresHolder;

        switch (type)
        {
            case Figures.QUEEN:
                figure = new Queen(cell, isWhiteFigure);
                break;
            case Figures.PAWN:
                figure = new Pawn(cell, isWhiteFigure);
                Pawn pawn = (Pawn)figure;
                pawn.OnPawnChangeToQueenEvent += () => 
                {
                    Board.INSTANCE[figure.CurrentCell].CreateFigureOnCell(Figures.QUEEN, figure.IsWhiteFigure);
                    gameObject.SetActive(false);
                };
                pawn = null;
                break;

            case Figures.KNIGHT:
                figure = new Knight(cell, isWhiteFigure);
                break;
            case Figures.KING:
                figure = new King(cell, isWhiteFigure);
                break;
            case Figures.ROOK:
                figure = new Rook(cell, isWhiteFigure);
                break;
            case Figures.BISHOP:
                figure = new Bishop(cell, isWhiteFigure);
                break;
        }

        figure.OnFigureRemovedEvent += () => gameObject.SetActive(false);
    }

    public bool MoveFigure(BoardCell to)
    {
        return figure.MoveFigure(to);
    }

    public bool MoveGameFigureToCell(CellController cell)
    {
        if (figure.CheckMove(cell.Cell))
        {
            cell.SetGameFigureOnCell(this);
            return true;
        }
        return false;
    }

    /// <summary>
    /// just move GO to the cell but without check if it's right move
    /// </summary>
    /// <param name="cell"></param>
    public void MoveGameFigureIgnoringRules(CellController cell)
    {
        cell.SetGameFigureIgnoringRules(this);
        figure.MoveFigureToCellIgnoringRules(cell.Cell);
    }

    public List<ProductiveRule> GetWorkRules()
    {
        return figure.WorkRules;
    }

    public IEnumerable<ProductiveRule> GetValidRules()
    {
        return figure.GetValidMoves();
    }

    public Figure Figure
    {
        get { return figure; }
    }
}
