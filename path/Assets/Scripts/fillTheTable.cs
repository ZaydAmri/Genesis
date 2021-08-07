using UnityEngine;
using System.Collections;

public class fillTheTable : MonoBehaviour {

	public int columns;
	public GameObject firstObject;
	public int lines;
	public float XStep;
	public float YStep;

	private float XPosition;
	private float YPosition;


	void Awake () {
		XPosition = firstObject.transform.localPosition.x;
		YPosition = firstObject.transform.localPosition.y;

		for (int i=0; i< lines; i++) {
			for (int j=0; j< columns;j++)
			{
				int name=(i*columns)+j+1;
				GameObject obj = GameObject.Find(""+name);
				print(name);
				obj.transform.localPosition = new Vector2(XPosition,YPosition);
				XPosition +=XStep;
			}
			XPosition = -11.75f;
			YPosition +=YStep;
		}
	}
	
}
