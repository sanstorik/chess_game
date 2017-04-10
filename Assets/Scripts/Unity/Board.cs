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

        CreateFigures();
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
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j].CreateFigureOnCell(Figures.PAWN, true);


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
