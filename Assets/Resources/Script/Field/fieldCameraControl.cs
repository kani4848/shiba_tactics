using UnityEngine;
using System.Collections;

public class fieldCameraControl : MonoBehaviour {

	public new Transform transform;
	public Transform playerTransform;

	public SpriteRenderer map;

	private float cameraLimitX;
	private float cameraLimitY;

	//カメラ撮影範囲をrectで設定
	public Rect stageRect;
	public Vector3 lowerLeft;
	public Vector3 upperLeft;
	public Vector3 lowerRight;
	public Vector3 upperRight;

	//カメラ撮影範囲をギズモで表示
	void OnDrawGizmos(){
		lowerLeft = new Vector3 (stageRect.xMin,stageRect.yMax,0);
		upperLeft = new Vector3 (stageRect.xMin,stageRect.yMin,0);
		lowerRight = new Vector3 (stageRect.xMax,stageRect.yMax,0);
		upperRight = new Vector3 (stageRect.xMax,stageRect.yMin,0);

		Gizmos.color = Color.red;
		Gizmos.DrawLine (lowerLeft, upperLeft);
		Gizmos.DrawLine (upperLeft, upperRight);
		Gizmos.DrawLine (upperRight, lowerRight);
		Gizmos.DrawLine (lowerRight, lowerLeft);
	}

	// Use this for initialization
	void Start () {

		playerTransform = GameObject.FindGameObjectWithTag ("player").transform;

		//rectを設定
		stageRect.xMin = map.bounds.min.x;
		stageRect.xMax = map.bounds.max.x;
		stageRect.yMin = map.bounds.min.y;
		stageRect.yMax = map.bounds.max.y;

		cameraLimitX = map.bounds.size.x / 2 - Camera.main.ViewportToWorldPoint (new Vector3(1,0,0)).x;
		cameraLimitY = map.bounds.size.y / 2 - Camera.main.ViewportToWorldPoint (new Vector3(0,1,0)).y;
	
	}

	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (playerTransform.position.x) <= cameraLimitX) {
			transform.position = new Vector3 (playerTransform.position.x, transform.position.y, transform.position.z);
		} else {
			if(playerTransform.position.x>0){
				transform.position = new Vector3 (cameraLimitX, transform.position.y, transform.position.z);
			}else{
				transform.position = new Vector3 (-cameraLimitX, transform.position.y, transform.position.z);
			}
		}


		if (Mathf.Abs (playerTransform.position.y) <= cameraLimitY) {
			transform.position = new Vector3 (transform.position.x, playerTransform.position.y, transform.position.z);
		} else {
			if(playerTransform.position.y>0){
				transform.position = new Vector3 (transform.position.x,cameraLimitY, transform.position.z);
			}else{
				transform.position = new Vector3 (transform.position.x,-cameraLimitY, transform.position.z);
			}
		}
	
	}
}
