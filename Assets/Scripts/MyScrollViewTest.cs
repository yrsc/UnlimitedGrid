using UnityEngine;
using System.Collections;

public class MyScrollViewTest : MonoBehaviour {

	private UnlimitedScrollView _scrollView;
	public int initCellNum = 15;
	void Awake()
	{
		_scrollView = GetComponent<UnlimitedScrollView>();
		if(_scrollView != null)
		{
			_scrollView.spawnNewCell = SpawnNewCell;
			_scrollView.deSpawnCell = DespawnCell;
		}
		for(int i = 0;i < initCellNum;i++)
		{
			_scrollView.GenerateNewBottomCell();	
		}
	}


	GameObject SpawnNewCell(int row)
	{
		GameObject go = Resources.Load("GridCell") as GameObject;
		return GameObject.Instantiate(go);
	}

	void DespawnCell(GameObject go)
	{
		GameObject.Destroy(go);
	}
}
