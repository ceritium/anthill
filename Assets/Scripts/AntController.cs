using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	// Use this for initialization
	Vector2[] ways;
	int oiWay;

	bool withFood;

	private GameController gameController;

	void Start () {
		ways = new Vector2[] {Vector2.up, new Vector2(1,1), Vector2.right, new Vector2(1,-1), Vector2.down, new Vector2(-1,-1), Vector2.left, new Vector2(-1,1)};
		oiWay = Random.Range (0, ways.Length);

		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();

		withFood = false;
	}
		
	void Update () {
		Vector2 po = transform.position;

		CellController cellCtrl = gameController.GetCellCtrl (po);

		if (withFood) {
			cellCtrl.StepFood();
			DetectNest ();
			LookForP ();
		} else {
			cellCtrl.StepNoFood();
			DetectFood ();
			LookForP ();
		}
	}

	bool DetectNest(){
		Vector2 po = transform.position;

		foreach (Vector2 way in ways) {
			Vector2 nPo = po + way;
			if (gameController.ValidPosition (nPo)) {
				CellController cellCtrl = gameController.GetCellCtrl (nPo);
				if (cellCtrl.isNest) {
					withFood = false;
//					Debug.Log ("no food");
					GoBack();
					break;
				}
			}
		}
		return withFood;
	}

	void LookForP(){
		Vector2 po = transform.position;

		List<int> pWays = new List<int> ();

		for (int i = -2; i <= 2; i++) {
			int iWay = ModuleWayIndex (i);

			Vector2 nPo = po + ways [iWay];
			if (gameController.ValidPosition (nPo)) {
				CellController cellCtrl = gameController.GetCellCtrl (nPo);
				if (withFood) {
					if (cellCtrl.hasPN ()) {
						pWays.Add (iWay);
					}
				} else {
					if (cellCtrl.hasPF ()) {
						pWays.Add (iWay);
					}

				}
			}
		}

		if (pWays.Count > 0) {
			int iWay = pWays [Random.Range (0, pWays.Count)];
			Vector2 nPo = po + ways [iWay];
			transform.position = nPo;
			oiWay = iWay;
		} else {
			if (withFood) {
				for (int iWay = 0; iWay < ways.Length; iWay++) {
					Vector2 nPo = po + ways [iWay];
					if (gameController.ValidPosition (nPo)) {
						CellController cellCtrl = gameController.GetCellCtrl (po + ways [iWay]);
						if (withFood) {
							if (cellCtrl.hasPN ()) {
								pWays.Add (iWay);
							}
						} else {
							if (cellCtrl.hasPF ()) {
								pWays.Add (iWay);
							}
						}
					}
				}

				if (pWays.Count > 0) {
					int iWay = pWays [Random.Range (0, pWays.Count)];
					Vector2 nPo = po + ways [iWay];
					transform.position = nPo;
					oiWay = iWay;
				} else {
					RandomMove ();
				}
			} else {
				RandomMove ();
			}
		}
	}

	void DetectFood(){
		Vector2 po = transform.position;
		foreach (Vector2 way in ways) {
			CellController cellCtrl = gameController.GetCellCtrl (po+way);
			if (cellCtrl.food > 0) {
				

				cellCtrl.TakeFood ();
				withFood = true;
				GoBack();
				break;
			}
		}
			
	}
		

	void GoBack(){
//		Debug.Log ("goback");
		oiWay = ModuleWayIndex (ways.Length/2);
	}


	void RandomMove(){
		Vector2 po = transform.position;

		int niW = Random.Range (-2, 3);
		int iWay = ModuleWayIndex (niW);
		Vector2 newPo = po + ways [iWay];

		if (gameController.ValidPosition(newPo)){
			transform.position = newPo;
		}

		oiWay = iWay;
	}
		
	int ModuleWayIndex(int index){
		return (oiWay + index + ways.Length) % ways.Length;
	}
}
