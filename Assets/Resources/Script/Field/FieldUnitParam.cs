using UnityEngine;
using System.Collections;

public class FieldUnitParam : MonoBehaviour {

	//フィールドユニットのパラメータ
	public int ID;
	public int troopCount;
	public string troopType;
	public string imagePath;
	public Vector3 position;

	public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		if (spriteRenderer != null) {
			spriteRenderer.sprite = Resources.Load ("Image/" + imagePath, typeof(Sprite))as Sprite;

		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
