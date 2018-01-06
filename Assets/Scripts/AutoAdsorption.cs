using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAdsorption : MonoBehaviour {

    public UIGrid grid;
    private Vector3 _originPos;
    private UIScrollView _scrollView;

    private void Awake()
    {
        _originPos = transform.localPosition;
        _scrollView = GetComponent<UIScrollView>();
        _scrollView.onDragStarted = OnDragStart;
        _scrollView.onDragFinished = OnDragFinished;
    }

    private float _dragStartTime = 0;
    private Vector3 _dragStarPos;

    void OnDragStart()
    {
        _dragStartTime = Time.time;
        _dragStarPos = _scrollView.transform.localPosition;
    }

    private float _dragEndTime = 0;
    private Vector3 _dragEndPos;

    void OnDragFinished()
    {
        _dragEndTime = Time.time;
        _dragEndPos = _scrollView.transform.localPosition;
        Vector3 dragDeltaPos = _dragEndPos - _dragStarPos;
        if (_dragEndTime - _dragStartTime < 0.5f && Mathf.Abs(dragDeltaPos.y) > 50)
        {
            Vector3 deltaPos = transform.localPosition - _originPos;
            int row = (int)deltaPos.y / (int)grid.cellHeight;
            if(dragDeltaPos.y > 0)
            {
                row += 1;
            }
            Vector3 targetPos = new Vector3(deltaPos.x, row * grid.cellHeight + _originPos.y, deltaPos.z);
            SpringPanel.Begin(_scrollView.gameObject, targetPos, 20);
        }
        else
        {
            Vector3 deltaPos = transform.localPosition - _originPos;
            float percent = (Mathf.Abs((int)deltaPos.y) % (int)grid.cellHeight) / grid.cellHeight;
            int row = (int)deltaPos.y / (int)grid.cellHeight;
            Vector3 targetPos = Vector3.zero;
            if (percent > 0.4f)
            {
                if (deltaPos.y > 0)
                {
                    row += 1;
                }
                else
                {
                    row -= 1;
                }
                targetPos = new Vector3(deltaPos.x, row * grid.cellHeight + _originPos.y, deltaPos.z);
            }
            else
            {
                targetPos = new Vector3(deltaPos.x, row * grid.cellHeight + _originPos.y, deltaPos.z);
            }
            SpringPanel.Begin(_scrollView.gameObject, targetPos, 20);
        }
    }
    
}
