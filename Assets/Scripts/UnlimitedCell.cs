using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimitedCell : MonoBehaviour
{
    private int _row;
    private UnlimitedScrollView _scrollView;

    public UILabel label;

    public void InitScrollView(UnlimitedScrollView scrollView)
    {
        _scrollView = scrollView;
    }

    public void SetRow(int row)
    {
        _row = row;
        label.text = row.ToString();
        transform.localPosition = new Vector3(0, -row * _scrollView.gridCellHeight, 0);
    }

   

    private void Update()
    {
        if(_scrollView.CheckIfOutOfBottomBound(_row))
        {
            _scrollView.DespawnOutOfBoundCell(gameObject);
            _scrollView.ScrollUp(); ;
        }
        else if(_scrollView.CheckIfOutOfUpBound(_row))
        {
            _scrollView.DespawnOutOfBoundCell(gameObject);
            _scrollView.ScrollDown();
        }
    }
}
