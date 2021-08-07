using UnityEngine;
using System.Collections;

public class playSoundOnDelay : MonoBehaviour {

	public AudioClip clip;
	public AudioSource source;
	public float timeToWait;
	public float volume;
	public bool runOnce;

	void Awake () {
		source = gameObject.GetComponent<AudioSource> ();
	}


	IEnumerator waitToPlaySound()
	{
		yield return new WaitForSeconds(timeToWait);
		source.PlayOneShot (clip,volume);
	}

	void Update () {
		if (!runOnce) {
			StartCoroutine(waitToPlaySound());
			runOnce = true;
		}
	}
}
