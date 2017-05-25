using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


	public int worldSize;
	public int maxAnts;
	public GameObject cellPrefab;
	public GameObject antPrefab;

	public GameObject[,] cells;
	GameObject[] ants;

	// Use this for initialization
	void Start () {

		cells = new GameObject[worldSize,worldSize];
		for (int x = 0; x < worldSize; x++) {
			for (int y = 0; y < worldSize; y++) {
				cells [x, y] = Instantiate (cellPrefab, new Vector2(x-(worldSize/2),y-(worldSize/2)), Quaternion.identity) as GameObject;
			}
		}

		ants = new GameObject[maxAnts];
		for (int i = 0; i < maxAnts; i++) {
			ants[i] = Instantiate (antPrefab, Vector2.zero, Quaternion.identity) as GameObject;
		}

		// Set nest in 0,0
		CellController cellCtrl = GetCellCtrl (Vector2.zero);
		cellCtrl.isNest = true;

		// Use this coroutine instead of zillions of coroutines on CellController
		InvokeRepeating ("UpdateCells", 0f, 2f);

	}

	void UpdateCells(){
		foreach (GameObject cell in cells) {
			CellController ctrl = GetCellCtrl (cell);
			ctrl.ColorCell ();
		}
	}

	public CellController GetCellCtrl(GameObject cell){
		return cell.GetComponent<CellController> ();
	}

	public CellController GetCellCtrl(Vector2 po){
		GameObject cell = cells [(int)po.x+worldSize/2, (int)po.y+worldSize/2];
		return GetCellCtrl(cell);
	}

	public bool ValidPosition(Vector2 po){
		return Mathf.Abs (po.x*2) < worldSize && Mathf.Abs (po.y*2) < worldSize;
	}
}
