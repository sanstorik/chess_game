using Shvetsov_Int_knowl_lab_4.Figures;
using UnityEngine;

namespace Shvetsov_Int_knowl_lab_4
{
    public class BoardCell
    {
        int rowIndex;
        int columnIndex;
        Figure figureOnCell;
        bool isWhiteCell;

        public BoardCell(int rowIndex, int columnIndex, bool isWhiteCell)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.isWhiteCell = isWhiteCell;
            figureOnCell = null;
        }

        public Figure DeleteFigureFromCell()
        {
            Figure temp = figureOnCell;
            figureOnCell = null;

            return temp;
        }

        public void SetFigureOnCell(Figure figure)
        {
            figureOnCell = figure;
        }

        public Figure GetFigureOrDefault()
        {
            return figureOnCell;
        }

        public bool IsFigureOnCell()
        {
            return figureOnCell != null;
        }

        public int ColumnIndex
        {
            get { return columnIndex; }
        }

        public int RowIndex
        {
            get { return rowIndex; }
        }

        public bool IsWhiteCell
        {
            get { return isWhiteCell; }
        }

        public override bool Equals(object obj)
        {
            BoardCell cell = (BoardCell)obj;
            return (cell.rowIndex == rowIndex) && (cell.columnIndex == columnIndex);
        }

        public override int GetHashCode()
        {
            return ((rowIndex ^ rowIndex) & (columnIndex ^ ColumnIndex)) * 1000;
        }

        public override string ToString()
        {
            return "row = " + (rowIndex + 1) + " column= " + (columnIndex + 1); 
        }

        static public bool operator==(BoardCell x, BoardCell y)
        {
            return (x.rowIndex == y.rowIndex) && (x.columnIndex == y.columnIndex);
        }

        static public bool operator!= (BoardCell x, BoardCell y)
        {
            return !((x.rowIndex == y.rowIndex) && (x.columnIndex == y.columnIndex));
        }
    }
}
