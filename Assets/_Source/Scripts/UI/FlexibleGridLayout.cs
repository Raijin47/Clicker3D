using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : GridLayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private Vector2 _spacing;

    [SerializeField] private FitType _fitType;

    [SerializeField] private bool _fitX;
    [SerializeField] private bool _fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        cellSize = new Vector2(rectTransform.rect.width / 2, 160);


        //if (_fitType == FitType.Width || _fitType == FitType.Height || _fitType == FitType.Uniform)
        //{
        //    _fitX = true;
        //    _fitY = true;

        //    float sqrRt = Mathf.Sqrt(transform.childCount);
        //    _rows = Mathf.CeilToInt(sqrRt);
        //    _columns = Mathf.CeilToInt(sqrRt);
        //}

        //if(_fitType == FitType.Width || _fitType == FitType.FixedColumns)
        //{
        //    _rows = Mathf.CeilToInt(transform.childCount / _columns);
        //}

        //if(_fitType == FitType.Height || _fitType == FitType.FixedRows)
        //{
        //    _columns = Mathf.CeilToInt(transform.childCount / _rows);
        //}

        //float parentWidth = rectTransform.rect.width;
        //float parentHeight = rectTransform.rect.height;

        //float cellWidth = (parentWidth / _columns) - (_spacing.x / _columns * 2) - (padding.left / _columns) - (padding.right / _columns);
        //float cellHeight = (parentHeight / _rows) - (_spacing.y / _rows * 2) - (padding.top / _rows) - (padding.bottom / _rows); 

        //_cellSize.x = _fitX ? cellWidth : _cellSize.x;
        //_cellSize.y = _fitY ? cellHeight : _cellSize.y;

        //for (int i = 0; i < rectChildren.Count; i++)
        //{
        //    int rowCount = i / _columns;
        //    int columnCount = i % _columns;

        //    var item = rectChildren[i];

        //    var xPos = (_cellSize.x * columnCount) + (_spacing.x * columnCount) + padding.left;
        //    var yPos = (_cellSize.y * rowCount) + (_spacing.y * rowCount) + padding.top;

        //    SetChildAlongAxis(item, 0, xPos, _cellSize.x);
        //    SetChildAlongAxis(item, 1, yPos, _cellSize.y);
        //}
    }

    public override void SetLayoutHorizontal()
    {
        base.SetLayoutHorizontal();
        //cellSize = new Vector2(rectTransform.rect.width / 2, 160);
        //m_CellSize = new Vector2(rectTransform.rect.width / 2, 160);
    }
}
