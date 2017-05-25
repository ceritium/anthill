using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public int cameraCurrentZoom = 50;
	public int cameraZoomMin = 20;

	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = cameraCurrentZoom;		

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			cameraCurrentZoom += 1;
			Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 1);

		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			if (cameraCurrentZoom > cameraZoomMin)
			{
				cameraCurrentZoom -= 1;
				Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 1);
			}   
		}



		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Vector3 position = this.transform.position;
			position.x--;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Vector3 position = this.transform.position;
			position.x++;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Vector3 position = this.transform.position;
			position.y++;
			this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Vector3 position = this.transform.position;
			position.y--;
			this.transform.position = position;
		}
	}
}