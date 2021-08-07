using UnityEngine;
using System.Collections;

public class retour : MonoBehaviour {

	public void retourMenu(){
		Application.LoadLevel ("menu");
	}
	public void instructions()
	{
		Application.LoadLevel ("instruction");
	}
}
