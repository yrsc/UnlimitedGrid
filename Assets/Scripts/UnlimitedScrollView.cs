using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimitedScrollView : MonoBehaviour {

    public UIGrid grid;
    public int boundOfCell = 10;

    private Vector3 _originPos;

    private float _gridCellHeight;
    public float gridCellHeight
    {
        get
        {
            return _gridCellHeight;
        }
    }

    private int _curRow;
    public int curRow
    {
        get { return _curRow; }
    }

    private int _topRow = 0;
    private int _bottomRow = -1;

    public delegate GameObject SpawnNewCell(int row);
    public SpawnNewCell spawnNewCell = null;

    public delegate void DeSpawnCell(GameObject go);
    public DeSpawnCell deSpawnCell = null;

    public delegate void OnRowChanged(bool isDown);
    public OnRowChanged onRowChanged = null;

    private void Awake()
    {
        _originPos = transform.localPosition;
        _gridCellHeight = grid.cellHeight;
    }

    private int _lastRow = 0;

    void CalculateCurRow()
    {
        Vector3 deltaPos = transform.localPosition - _originPos;
        _curRow = (int)(deltaPos.y / _gridCellHeight);
        if (_curRow < 0)
        {
            _curRow = 0;
        }
        if (_curRow > _lastRow)
        {
            if (onRowChanged != null)
            {
                onRowChanged(true);
            }
            for (int i = _lastRow; i < _curRow; i++)
            {
                GenerateNewBottomCell();
            }
        }
        else if (_curRow < _lastRow)
        {
            if (onRowChanged != null)
            {
                onRowChanged(false);
            }
            for (int i = _curRow; i < _lastRow; i++)
            {
                GenerateNewTopCell();
            }
        }
        _lastRow = _curRow;
    }

    public bool CheckIfOutOfUpBound(int row)
    {
        return curRow - row > boundOfCell || row < 0;
    }

    public bool CheckIfOutOfBottomBound(int row)
    {
        return row - curRow > boundOfCell;
    }

    public void ScrollDown()
    {
        _topRow += 1;
    }

    public void ScrollUp()
    {
        _bottomRow -= 1;
    }

    public void GenerateNewBottomCell()
    {
        int newBottomRow = _bottomRow + 1;
        if (CheckIfOutOfBottomBound(newBottomRow))
            return;
        if(spawnNewCell != null)
        {
            GameObject go = spawnNewCell(newBottomRow);
            if(go != null)
            {
                _bottomRow = newBottomRow;
                go.transform.SetParent(grid.transform, false);
                UnlimitedCell unlimitedCell = go.GetComponent<UnlimitedCell>();
                unlimitedCell.InitScrollView(this);
                unlimitedCell.SetRow(_bottomRow);
            }           
        }      
    }

    public void GenerateNewTopCell()
    {
        int newTopRow = _topRow - 1;
        if (CheckIfOutOfUpBound(newTopRow))
            return;
        if (spawnNewCell != null)
        {
            GameObject go = spawnNewCell(newTopRow);
            if(go != null)
            {
                _topRow = newTopRow;
                go.transform.SetParent(grid.transform, false);
                UnlimitedCell unlimitedCell = go.GetComponent<UnlimitedCell>();
                unlimitedCell.InitScrollView(this);
                unlimitedCell.SetRow(_topRow);
            }          
        }          
    }

    public void DespawnOutOfBoundCell(GameObject go)
    {
        if(deSpawnCell != null)
        {
            deSpawnCell(go);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CalculateCurRow();        
    }
}
