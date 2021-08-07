using UnityEngine;
using System.Collections;

public class trailRendererManager : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponentInChildren<TrailRenderer>().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
			print("out");
		}
	}
}
