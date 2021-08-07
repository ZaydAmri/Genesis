using UnityEngine;
using System.Collections;

public class goToLevelOne : MonoBehaviour {

	public bool runOnce;
	public float timeToWait;

	IEnumerator waitToPlaylevel()
	{
		yield return new WaitForSeconds(timeToWait);
		Application.LoadLevel ("level1");
	}
	
	void Update () {
		if (!runOnce) {
			StartCoroutine(waitToPlaylevel());
			runOnce = true;
		}
	}
}
