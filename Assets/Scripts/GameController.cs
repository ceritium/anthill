using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {


	public float iteration = 0;
	public int worldSize;
	public int maxAnts;
	public GameObject cellPrefab;
	public GameObject antPrefab;
	public Text antsText;
	public Text iterationText;

	public GameObject[,] cells;
	GameObject[] ants;
	private bool initiated = false;

	// Use this for initialization
	void Start () {

		// Use this coroutine instead of zillions of coroutines on CellController
		StartCoroutine("InitMap");

	}

	void Update(){
		GuiUpdate ();
	}

	void GuiUpdate(){
		if (initiated) {
			antsText.text = "Ants: " + ants.Length;
			iterationText.text = "Iteration: " + iteration;
		}
	}
		
	IEnumerator InitMap(){
		int count = 0;
		cells = new GameObject[worldSize,worldSize];
		for (int x = 0; x < worldSize; x++) {
			for (int y = 0; y < worldSize; y++) {
				cells [x, y] = Instantiate (cellPrefab, new Vector2(x-(worldSize/2),y-(worldSize/2)), Quaternion.identity) as GameObject;
				if (count > 500) {
					yield return null;
					count = 0;
				}
				count++;
			}
		}

		// Set nest in 0,0
		CellController cellCtrl = GetCellCtrl (Vector2.zero);
		cellCtrl.isNest = true;

		ants = new GameObject[maxAnts];
		for (int i = 0; i < maxAnts; i++) {
			ants[i] = Instantiate (antPrefab, Vector2.zero, Quaternion.identity) as GameObject;
		}

		StartCoroutine("UpdateCellsCoroutine");
		StartCoroutine("UpdateAntsCoroutine");
		initiated = true;
	}

	IEnumerator UpdateAntsCoroutine() {
		int count = 0;
		while(true){
			iteration++;
			foreach (GameObject ant in ants) {
				ant.GetComponent<AntController> ().Move ();
				if (count > 100) {
					yield return null;
					count = 0;
				}
				count++;
			}
			yield return null;
		}
	}

	IEnumerator UpdateCellsCoroutine() {
		int count = 0;
		while(true){
			foreach (GameObject cell in cells) {
				CellController ctrl = GetCellCtrl (cell);
				ctrl.ColorCell ();
				if (count > 100) {
					yield return null;
					count = 0;
				}
				count++;
			}
			yield return null;
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
