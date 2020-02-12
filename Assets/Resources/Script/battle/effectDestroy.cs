using UnityEngine;
using System.Collections;

public class effectDestroy : MonoBehaviour {

	private float time;
	private float timeLimit;

	// Use this for initialization
	void Start () {
		timeLimit = this.gameObject.transform.Find("hit_nakami").GetComponent<ParticleSystem>().duration;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= timeLimit) {
			Destroy(this.gameObject);
		}
	
	}
}
