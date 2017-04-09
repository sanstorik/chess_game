using UnityEngine;
using System.Collections.Generic;
using Shvetsov_Int_knowl_lab_4;
using Shvetsov_Int_knowl_lab_4.Figures;

public class Board : MonoBehaviour {
    static Board instance;
    const int BOARD_SIZE = 8;

    [SerializeField]
    GameObject whiteCellPrefab;
    [SerializeField]
    GameObject blackCellPrefab;
    [SerializeField]
    Transform startingPosition;
    [SerializeField]
    Transform cellsHolder;

    public Transform figuresHolder;

    [SerializeField]
    GameObject[] whiteFiguresPrefabs;
    [SerializeField]
    GameObject[] blackFiguresPrefabs;

    CellController[,] board;

    float cellWidth;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        cellWidth = whiteCellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x - 0.24f;

        ChessBoard.CreateBoard(BOARD_SIZE);
        board = new CellController[BOARD_SIZE, BOARD_SIZE];

        for(int row = 0; row < BOARD_SIZE; row++)
        {
            for(int column = 0; column < BOARD_SIZE; column++)
            {
                    CreateCell(row,column);
            }
        }

        board[0, 0].CreateFigureOnCell(Figures.QUEEN, false);
        //MovingAlgorythms.INSTANCE.SolveTracingProblem(board[0, 0].GetFigureController());
        ReverseInferenceAlgorythm.INSTANCE.SolveTracingAlgorythm(board[0, 0].GetFigureController(), board[4,4]);

       //CreateFigures();
	}

    void CreateCell(int row, int column)
    {
        GameObject prefab;
        Vector3 cellPosition = startingPosition.position;
        cellPosition.x += column * cellWidth;
        cellPosition.y -= row * cellWidth;

        if (ChessBoard.INSTANCE[row,column].IsWhiteCell)
            prefab = whiteCellPrefab;
        else
            prefab = blackCellPrefab;

        var go = (GameObject)Instantiate(prefab, cellPosition, Quaternion.identity);
        go.transform.parent = cellsHolder;

        board[row, column] = go.GetComponent<CellController>();
        board[row, column].Cell = ChessBoard.INSTANCE[row, column];
    }

    void CreateFigures()
    {
        for(int row = 0; row < BOARD_SIZE; row++)
        {
            for(int column = 0; column < BOARD_SIZE; column++)
            {
                if (row == 1)
                    board[row, column].CreateFigureOnCell(Figures.PAWN, false);
                else if(row == BOARD_SIZE - 2)
                    board[row, column].CreateFigureOnCell(Figures.PAWN, true);
            }
            CreateMainFigures(row);
        }
    }

    void CreateMainFigures(int row)
    {
        bool isWhiteFigure = false;

        if (row == 0)
            isWhiteFigure = false;
        else if (row == BOARD_SIZE - 1)
            isWhiteFigure = true;

        if (row == 0 || row == BOARD_SIZE - 1) {
            board[row, 0].CreateFigureOnCell(Figures.ROOK, isWhiteFigure);
            board[row, BOARD_SIZE - 1].CreateFigureOnCell(Figures.ROOK, isWhiteFigure);

            board[row, 1].CreateFigureOnCell(Figures.KNIGHT, isWhiteFigure);
            board[row, BOARD_SIZE - 2].CreateFigureOnCell(Figures.KNIGHT, isWhiteFigure);

            board[row, 2].CreateFigureOnCell(Figures.BISHOP, isWhiteFigure);
            board[row, BOARD_SIZE - 3].CreateFigureOnCell(Figures.BISHOP, isWhiteFigure);

            board[row, 3].CreateFigureOnCell(Figures.QUEEN, isWhiteFigure);
            board[row, 4].CreateFigureOnCell(Figures.KING, isWhiteFigure);
        }
    }

    public GameObject GetPrefab(Figures type, bool isWhite)
    {
        if (isWhite)
            return whiteFiguresPrefabs[(int)type];
        else
            return blackFiguresPrefabs[(int)type];
    }


    public CellController this[int row, int column]
    {
        get
        {
            if (row >= 0 && column >= 0 && row < BOARD_SIZE && column < BOARD_SIZE)
                return board[row, column];
            else throw new System.Exception("Board out of range");
        }
    }

    public CellController this[BoardCell cell]
    {
        get
        {
            return board[cell.RowIndex, cell.ColumnIndex];
        }
    }

    static public Board INSTANCE
    {
        get { return instance; }
    }
}
