using UnityEngine;
using Shvetsov_Int_knowl_lab_4;
using Shvetsov_Int_knowl_lab_4.Figures;
using DG.Tweening;

public class CellController : MonoBehaviour {
    BoardCell cell;

    [SerializeField]
    ParticleSystem particleShowRightMove;

    GameObject figureOnCell = null;

    public void CreateFigureOnCell(Figures type, bool isWhiteFigure)
    {
        figureOnCell = (GameObject)Instantiate(Board.INSTANCE.GetPrefab(type,isWhiteFigure), transform.position, Quaternion.identity);

        FigureController controller = figureOnCell.GetComponent<FigureController>();
        controller.CreateFigure(cell, isWhiteFigure, type);

        cell.SetFigureOnCell(controller.Figure);
    }

    public void RemoveFigureFromCell()
    {
        figureOnCell.SetActive(false);
        figureOnCell = null;
    }
    
    public void SetGameFigureOnCell(FigureController figure)
    {
        figureOnCell = figure.gameObject;
        figureOnCell.transform.position = transform.position;

        figure.MoveFigure(cell);
    }

    /// <summary>
    /// just set the figure, but do it without any check
    /// </summary>
    /// <param name="figure"></param>
    public void SetGameFigureIgnoringRules(FigureController figure)
    {
        figureOnCell = figure.gameObject;
        figureOnCell.transform.DOMove(transform.position, 1f);
    }

    public GameObject GetFigureOrDefault()
    {
        return figureOnCell;
    }

    public FigureController GetFigureController()
    {
        return figureOnCell.GetComponent<FigureController>();
    }

    public void StartGlowing()
    {
        particleShowRightMove.Play();
    }

    public void EndGlowing()
    {
        particleShowRightMove.Stop();
    }

    public BoardCell Cell
    {
        get { return cell; }
        set { cell = value; }
    }
}
