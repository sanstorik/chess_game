using UnityEngine;
using Shvetsov_Int_knowl_lab_4.Figures;
using Shvetsov_Int_knowl_lab_4;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

public enum Figures
{
    ROOK
}

public class FigureController : MonoBehaviour {

    Figure figure;

    public void CreateFigure(BoardCell cell, bool isWhiteFigure, Figures type)
    {
        transform.parent = Board.INSTANCE.figuresHolder;

        switch (type)
        {
            case Figures.ROOK:
                figure = new Rook(cell, isWhiteFigure);
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

    public Figure Figure
    {
        get { return figure; }
    }
}
