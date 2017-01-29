using UnityEngine;
using System.Collections;
using Shvetsov_Int_knowl_lab_4.Figures;
using Assets.Scripts.Board;
using UnityEngine.UI;
using DG.Tweening;

public class MoveController : MonoBehaviour {

    public Image wrongMoveImage;
    Ray ray;
    RaycastHit figure;
    int figureSortingOrder;
    FigureController figureController;

    RaycastHit cell;
    CellController cellController;
    
    bool movingFigure = false;
    Vector3 pos;
    Vector3 startingPos;

    LayerMask figureLayer;
    LayerMask cellMask;

    private void Start()
    {
        figureLayer = LayerMask.GetMask("Figures");
        cellMask = LayerMask.GetMask("Board");
    }

    void Update () {

        if (Input.GetMouseButtonDown(0) && !movingFigure)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out figure, 200f, figureLayer) && 
                figure.transform.GetComponent<FigureController>().Figure.IsWhiteFigure == GameData.isWhiteTurn)
            {
                movingFigure = true;
                startingPos = figure.transform.position;
                figureController = figure.transform.GetComponent<FigureController>();

                var spriteRenderer = figure.transform.GetComponent<SpriteRenderer>();
                figureSortingOrder = spriteRenderer.sortingOrder;
                spriteRenderer.sortingOrder += 2;

                ShowAllValidMoves();
            }
        }
        else if(Input.GetMouseButtonUp(0) && movingFigure)
        {
            movingFigure = false;
            figure.transform.GetComponent<SpriteRenderer>().sortingOrder = figureSortingOrder;
            HideAllValidMoves();

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out cell, 200f, cellMask))
            {
                cellController = cell.transform.GetComponent<CellController>();

                if (figureController.MoveFigure(cellController.Cell))
                    figure.transform.position = cell.transform.position;
                else
                    ProceedWrongMove();
            }
            else
                ProceedWrongMove();
        }

        if (movingFigure)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            figure.collider.transform.position = pos;
        }
    }

    void ShowAllValidMoves()
    {
        var workRules = figureController.GetValidRules();
        foreach(ProductiveRule rule in workRules)
        {
            Board.INSTANCE[rule.RightHandSideRule].StartGlowing();
        }
    }

    void HideAllValidMoves()
    {
        var workRules = figureController.GetWorkRules();
        foreach (ProductiveRule rule in workRules)
        {
            Board.INSTANCE[rule.RightHandSideRule].EndGlowing();
        }
    }

    void ProceedWrongMove()
    {
        figure.transform.position = startingPos;
        ShowWrongMoveAnimation();
    }

    void ShowWrongMoveAnimation()
    {
        wrongMoveImage.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(wrongMoveImage.DOFade(0.16f, 0.3f));
        sequence.Append(wrongMoveImage.DOFade(0f, 0.3f).OnComplete(() => wrongMoveImage.gameObject.SetActive(false)));
        sequence.Play();
    }
}
