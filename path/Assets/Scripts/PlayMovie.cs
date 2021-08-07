using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayMovie : MonoBehaviour {

	// Use this for initialization
	void Update () {
		((MovieTexture)GetComponent<RawImage>().material.mainTexture).Play();
		StartCoroutine (Wait(11f));

	}
	public IEnumerator Wait(float time){
		yield return new WaitForSeconds(time);
		Application.LoadLevel("menu");
	}

}
