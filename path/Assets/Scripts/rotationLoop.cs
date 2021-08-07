using UnityEngine;
using System.Collections;

public class rotationLoop : MonoBehaviour {

	public static rotationLoop current;
	public float angle = 0;
	public float radius;
	public float speed;

	private float x = 0;
	private float y = 0;
	public float rotationSpeed;
	public float radiusDecreaseSpeed;

	void Awake () {
		current = this;
	}

	void Start(){
		radius = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(transform.position.x,2)+Mathf.Pow(transform.position.y,2)));
		angle = Mathf.Acos(transform.position.x/radius);
	}

	void Update () {
		
		x = radius * Mathf.Cos(angle);
		y = radius * Mathf.Sin(angle);
		
		transform.position =new Vector2(x, y);
		
		angle += speed* Mathf.Rad2Deg * Time.deltaTime;
		speed += rotationSpeed;
		radius -= radiusDecreaseSpeed;
	}

}
