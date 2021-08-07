using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public AudioClip buttonEffect;
	public AudioSource source;

	void Awake()
	{
		source = gameObject.GetComponent<AudioSource> ();
	}

	public void jouer(){
		Application.LoadLevel ("prolog");
	}

	public void playSound()
	{
		source.PlayOneShot (buttonEffect,1f);
	}
}
