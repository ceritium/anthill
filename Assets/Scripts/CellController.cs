using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour {


	public float food;
	public float pnAt;
	public float pfAt;
	public bool isNest = false;
	public float evaporationRate = 1000f;
	private GameController gameController;
	private SpriteRenderer sr;

	void Awake(){
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		sr = GetComponent<SpriteRenderer> ();
		if (Random.Range (0, 100) > 98) {
			food = 1f;
		}
	}

	public void TakeFood(){
		if (food > 0) {
			food -= 0.10f;
		}
	}

	public void StepNoFood(){
		pnAt = gameController.iteration;
		ColorCell ();
	}

	public void StepFood(){
		pfAt = gameController.iteration;
		ColorCell ();
	}

	public bool hasPN(){
		if (pnAt > 0) {
			float diff = gameController.iteration - pnAt;
			return diff < evaporationRate;
		} else {
			return false;
		}
	}

	public bool hasPF(){
		if (pfAt > 0) {
			float diff = gameController.iteration - pfAt;
			return diff < evaporationRate;
		} else {
			return false;
		}
	}

	public void ColorCell(){
		float pn = 1f;

		if (pnAt > 0){
			float diff = gameController.iteration - pnAt;
			if (diff < evaporationRate) {
				pn = diff / evaporationRate;
			}
		}

		float pf = 1f;

		if (pfAt > 0){
			float diff = gameController.iteration - pfAt;
			if (diff < evaporationRate) {
				pf = diff / evaporationRate;
			}
		}

		if (food > 0 || pn < 1 || pf < 1) {
			sr.enabled = true;
		} else {
			sr.enabled = false;
		}
			
		sr.color = new Color(pn, 1f-food, pf);
	}
}

