using UnityEngine;
using System.Collections;

public class MatriceControl : MonoBehaviour {

	public static MatriceControl current;

	public int indexI;
	public int indexJ;
	private int playerIndexI;
	private int playerIndexJ;
	private Transform initPos;
	private int indexInitpos;
	public Transform player;
	private Vector2 startPos;
	private Vector2 targetPos;
	 int[,] Matrix=new int[30,20] ;
	public int mvtsAllowed;
	private int mvtCounter=0;
	private GameObject obj;
	int indexx=0;

	int index=0;
	int nbrpas=0;
	int[] Pas = new int[10];

	public float _counter=0;
	public float nextStepDelay=2f;
	public int speed;
	int i=0;
	private bool TestPlayerMvt=false;
	private int activeTrail=0;
	float lerping;
	//public GameObject[] path;
	//int indexPath=0;
	//arrows//
	public GameObject[] Arrows;
	public Sprite spriteRight;
	public Sprite spriteLeft;
	public Sprite spriteUp;
	public Sprite spriteDown;
	public int remainingMVT;
	public TextMesh objectText;
	public int lines;
	public int columns;
	void Awake()
	{
		Time.timeScale = 3;
		current = this;
	}

	void preventLoop () {
		if (playerIndexI == indexI && playerIndexJ == indexJ && i+1 == mvtsAllowed) {
			print("i = "+i);
			print("mvtsAllowed = "+mvtsAllowed);

			collision.current.waitToExecuteLoop();
			player.GetComponentInChildren<TrailRenderer>().enabled = true;
		}
	}

	void Start () {
		indexInitpos = ((indexI) * (columns)) + indexJ + 1;
		initPos = GameObject.Find ("" + indexInitpos).transform;
		// remplissage de la matrice
		player.position = initPos.position;
		for (int i=0; i<lines; i++) {
			for (int j=0; j<columns; j++) {
				index++;
				Matrix [i, j] = index;
			}
		}  
		// initialisation position joueur
		startPos = player.position;
		targetPos = startPos;
		// pour tester les loops
		playerIndexI = indexI;
		playerIndexJ = indexJ;
	}

	void Update () 
	{
		remainingMVT = mvtsAllowed - mvtCounter;
		TextMesh textmm=objectText.gameObject.GetComponent<TextMesh>();
		objectText.text = "moves : " + remainingMVT;
		//display Arrows//

		/////lerping script/////
		if (TestPlayerMvt) {
			player.position = Vector3.Lerp (player.position, obj.transform.position, Time.deltaTime*speed);

		}

		if (mvtCounter < mvtsAllowed && !collision.current.GameOver) {
			if (Input.GetKeyUp (KeyCode.RightArrow)) {

				mvtCounter++;
				right ();
				Pas[mvtCounter]=1;
				//ArrowsControl ();

			} 
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {

				mvtCounter++;
				left ();
				Pas[mvtCounter]=-1;
				//ArrowsControl ();


			}

			if (Input.GetKeyUp (KeyCode.UpArrow)) {

				mvtCounter++;
				Up ();
				Pas[mvtCounter]=2;
				//ArrowsControl ();

			}
			if (Input.GetKeyUp (KeyCode.DownArrow)) {


				mvtCounter++;
				Down ();
				Pas[mvtCounter]=-2;
				//ArrowsControl ();

			}
		}
		// testing loop
		else {
			mvtCounter = mvtsAllowed;
			if(!collision.current.GameOver && !collision.current.levelCleared && !collision.current.isLooping)
			{
				repeatMove();
			}
		}
		}

	void repeatMove(){

		print("i = "+i);
		print("mvtsAllowed = "+mvtsAllowed);

	//if (i >= mvtsAllowed) {
	//	i = 0;
	//}
	_counter += Time .deltaTime;
	while (i < mvtsAllowed){
		if (_counter > nextStepDelay) {
			_counter = 0;
		i++;
		preventLoop ();
		TestPlayerMvt=false;
		player.position = obj.transform.position;
		if(activeTrail>=1){
			player.GetComponentInChildren<TrailRenderer>().enabled=true;
			activeTrail=0;
		}
		else
			activeTrail++;

		}
		else {
			return;
		}
		switch (Pas[i]) {
		case 1:
			right ();

			break;
		case -1:
			left ();

			break;
		case 2:
			Up ();

			break;
		case -2:
			Down ();

			break;
		}
		}
		i = 0;

	}

	void right(){
		if (mvtCounter<=mvtsAllowed) {
			indexJ++;

		if(indexJ>columns-1)
			{	indexJ=0;
				if(!collision.current.isLooping)
					player.GetComponentInChildren<TrailRenderer>().enabled = false;
				activeTrail=0;
				obj=GameObject.Find(""+Matrix[indexI,indexJ]);
				player.position = obj.transform.position;
			}

		else{obj=GameObject.Find(""+Matrix[indexI,indexJ]);
			TestPlayerMvt=true;
			}
		}
}
	void left(){
		if (mvtCounter<=mvtsAllowed) {
			indexJ--;

			if (indexJ < 0) {
				indexJ = columns-1;
				if(!collision.current.isLooping)
					player.GetComponentInChildren<TrailRenderer>().enabled = false;
				activeTrail=0;
				obj=GameObject.Find(""+Matrix[indexI,indexJ]);
				player.position = obj.transform.position;

			}

			else{obj = GameObject.Find ("" + Matrix [indexI, indexJ]);
			TestPlayerMvt=true;

			}
		}
	}
	void Up(){
		if (mvtCounter <= mvtsAllowed) {
			indexI--;

			if (indexI < 0) {
				indexI = lines-1;
				if(!collision.current.isLooping)
					player.GetComponentInChildren<TrailRenderer>().enabled = false;
				activeTrail=0;
				obj=GameObject.Find(""+Matrix[indexI,indexJ]);
				player.position = obj.transform.position;
			}
			else{obj = GameObject.Find ("" + Matrix [indexI, indexJ]);
				TestPlayerMvt=true;}
		}
	}	

	void Down(){
		if (mvtCounter <= mvtsAllowed) {
			indexI++;

			if (indexI > lines-1) {
				indexI = 0;
				if(!collision.current.isLooping)
					player.GetComponentInChildren<TrailRenderer>().enabled = false;
				activeTrail=0;
				obj=GameObject.Find(""+Matrix[indexI,indexJ]);
				player.position = obj.transform.position;
			}
				else{obj = GameObject.Find ("" + Matrix [indexI, indexJ]);
			TestPlayerMvt=true;
			}
		}
	}
	void ArrowsControl(){
			switch (Pas [mvtCounter]) {
			case 1:
				Arrows [mvtCounter-1].gameObject.GetComponent<SpriteRenderer> ().sprite = spriteRight;
				break;
			case -1:
				Arrows [mvtCounter-1].gameObject.GetComponent<SpriteRenderer> ().sprite = spriteLeft;
			
				break;
			case 2:
				Arrows [mvtCounter-1].gameObject.GetComponent<SpriteRenderer> ().sprite = spriteUp;
			
				break;
			case -2:
				Arrows [mvtCounter-1].gameObject.GetComponent<SpriteRenderer> ().sprite = spriteDown;
			
				break;
			case 0:
				break;
			}
	}
}