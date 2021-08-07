using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	public static collision current;
	public static int goodsCounter;
	public bool GameOver;
	public bool levelCleared;
	public bool isLooping;
	public GameObject explosion;
	public string levelToLoad;
	public GameObject levelConstellation;
	public GameObject mainCamera;
	public int nbrConstellation;

	private bool runOnce;
	private Animator camAnimator;
	private GameObject otherOBJ;

	void Awake()
	{
		current = this;
		goodsCounter = 0;
		camAnimator = mainCamera.GetComponent<Animator> ();
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag=="death"){
			otherOBJ = other.gameObject;
			GameOver = true;
		}
		if(other.gameObject.tag=="good"){
			other.GetComponentInChildren<ParticleSystem>().startColor = Color.cyan;
			other.enabled = false;
			goodsCounter ++;
		}
	}

	public void waitToExecuteLoop()
	{
		isLooping = true;
		transform.GetComponent<Collider2D> ().enabled = false;
		transform.GetComponent<rotationLoop> ().enabled = true;
	}

	public IEnumerator waitToExecuteEnd()
	{
		yield return new WaitForSeconds (0.3f);
		otherOBJ.SetActive (false);
		camAnimator.SetTrigger("death");
		explosion.transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
		gameObject.GetComponentInChildren<ParticleSystem>().Stop();
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
		Time.timeScale = 0.5f;
		explosion.SetActive(true);
		yield return new WaitForSeconds (1.8f);
		Application.LoadLevel (Application.loadedLevel);

	}
	IEnumerator waitToExecuteNext()
	{
		levelConstellation.SetActive (true);
		yield return new WaitForSeconds (4.5f);
		Application.LoadLevel (levelToLoad);
	}

	void Update()
	{
		if (isLooping) {
		   if(rotationLoop.current.radius < 0.1f) {
				if(!runOnce) {
			   		Application.LoadLevel (Application.loadedLevel);
					runOnce = true;
				}
			}
		}
		if (GameOver) {
			if(!runOnce)
			{
				StartCoroutine(waitToExecuteEnd());
				runOnce = true;
			}
		}
		if (goodsCounter == nbrConstellation) {
			if(!runOnce)
			{
				levelCleared = true;
				gameObject.GetComponentInChildren<ParticleSystem>().Stop();
				gameObject.transform.GetChild(1).gameObject.SetActive(false);
				StartCoroutine(waitToExecuteNext());
				runOnce = true;
			}
		}
	}

}

