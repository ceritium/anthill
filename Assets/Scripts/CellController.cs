using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour {


	public float food;
	public float pnAt;
	public float pfAt;
	public bool isNest = false;
	public float evaporationRate = 1000f;

	private SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		if (Random.Range (0, 100) > 98) {
			food = 1f;
		}
	}
	
	// Update is called once per frame
	// With a zillion of cells is better avoid use this method.
	// Even avoid coroutines, update cells from GameController.
	//	void Update () {		
	//	}

	public void TakeFood(){
		if (food > 0) {
			food -= 0.10f;
		}
	}

	public void StepNoFood(){
		pnAt = Time.frameCount;
		ColorCell ();
	}

	public void StepFood(){
		pfAt = Time.frameCount;
		ColorCell ();
	}

	public bool hasPN(){
		if (pnAt > 0) {
			float diff = Time.frameCount - pnAt;
			return diff < evaporationRate;
		} else {
			return false;
		}
	}

	public bool hasPF(){
		if (pfAt > 0) {
			float diff = Time.frameCount - pfAt;
			return diff < evaporationRate;
		} else {
			return false;
		}
	}

	public void ColorCell(){
		float pn = 1f;

		if (pnAt > 0){
			float diff = Time.frameCount - pnAt;
			if (diff < evaporationRate) {
				pn = diff / evaporationRate;
			}
		}


		float pf = 1f;

		if (pfAt > 0){
			float diff = Time.frameCount - pfAt;
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

