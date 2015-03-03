using UnityEngine;
using System.Collections;


public abstract class Enemy : MonoBehaviour {

	//Speeds
	public float rotateSpeed;
	protected float speed;

	//Sounds
	//public AudioClip sEnemyExplosion;

	//Paths
	protected const int PATH_CIRCLE = 1;
	protected const int PATH_EIGHT 	= 2;
	protected const int PATH_STRAIGHT = 3;
	
	
	private int path;

	private float eightRotated;
	private bool eightPathCircle;
	
	
	private float angleToBackBounds = 0;
	private bool outOfBounds;


	//Initializer, ALL the variables must be initialized here
	protected void startInit () {
		speed = 50.0f;
		rotateSpeed = 50.0f;
		outOfBounds = false;
		
		setPath(PATH_CIRCLE);

	}

	//Init procedure for each path
	protected void startInitPath () {

		switch(path)//Depending on the chosen path
		{
		case PATH_CIRCLE:
			//Just to give a little turn
			transform.Rotate (0, 0, -20);
			
			if(Random.Range(0,1)==0)
				//Makes it follows not straight paths
				transform.Rotate (0, 0, Random.Range(-20, 20));
			break;

		case PATH_EIGHT:
			//Just to give a little turn
			//transform.Rotate (0, 0, -20);
			
			if(Random.Range(0,1)==0)
				//Makes it follows not straight paths
				transform.Rotate (0, 0, Random.Range(-20, 20));

			eightRotated = 360.0f;
			eightPathCircle = false;
			break;
			
		case PATH_STRAIGHT:
			
			break;

		default:
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		
		switch(path)
			{
			case PATH_CIRCLE:
				moveInCircle();
				break;
			case PATH_EIGHT:
				moveInEight();
				break;
			case PATH_STRAIGHT:
				moveInLine();
				break;
			default:
				break;
			}	

		
	}

	//Follows the circle path
	protected void moveInCircle () {
		//Calculating the scout speed and its rotation
		float transAmount = speed * Time.deltaTime;
		float rotateAmount = rotateSpeed * Time.deltaTime;
		
		//Unturns it
		transform.Rotate (0, 0, 20);
		//Goes forward and rotates in a circle
		transform.Translate(0,0,transAmount);//Needs to be -transAmount because the model is backwards
		transform.Rotate (0, rotateAmount, 0);
		//Turns it a little to give the impression of being turning
		transform.Rotate (0, 0, -20);
	}

	//The opposite circle path
	private void moveInAntiCircle () {
		//Calculating the scout speed and its rotation
		float transAmount = speed * Time.deltaTime;
		float rotateAmount = rotateSpeed * Time.deltaTime;
		
		//Unturns it
		transform.Rotate (0, 0, -20);
		//Goes forward and rotates in a circle
		transform.Translate(0,0,transAmount);//Needs to be -transAmount because the model is backwards
		transform.Rotate (0, -rotateAmount, 0);
		//Turns it a little to give the impression of being turning
		transform.Rotate (0, 0, 20);
	}

	//Moves in eight
	protected void moveInEight () {
		if(eightRotated < 1)
		{
			eightPathCircle = !eightPathCircle;
			eightRotated = 360.0f;
		}

		eightRotated -= rotateSpeed * Time.deltaTime;

		if(eightPathCircle)
			moveInCircle();
		else
			moveInAntiCircle();
	}
	
	protected void moveInLine()
	{
		float transAmount = speed * Time.deltaTime;
		if(outOfBounds)
		{
			returnToMapBounds();
		}
		else
		{
			transform.Translate(0,0,transAmount);
		}
		
	}
	

	//Set the enemy path type
	public void setPath (int path) {
		this.path = path;

		startInitPath();
	}

	//Get the enemy path type
	public int getPath () {
		return path;
	}
	
	//Procedure to make the player go back to the map bounds
	protected void returnToMapBounds(){
		//Calculates the translation and rotation amounts
		float transAmount = 0.5f* speed * Time.deltaTime;

		if(angleToBackBounds < 1)
			transform.Translate(0,0,transAmount);
		else{
			float returnRotateAmount = 3f* rotateSpeed * Time.deltaTime;

			if(returnRotateAmount<angleToBackBounds){
				angleToBackBounds -= returnRotateAmount;
				transform.Rotate (0, -returnRotateAmount, 0);
			}else{
				transform.Rotate(0, -returnRotateAmount, 0);
				angleToBackBounds = 0;
			}
		}
	}
	
	void OnTriggerEnter (Collider collider) {
		//if it enters the MapBounds it stop the return procedure
		if(collider.CompareTag("MapBounds"))
			outOfBounds = false;
	}
	
	void OnTriggerExit (Collider collider) {
		//if it gets out of the MapBounds it starts the return procedure
		if(collider.CompareTag("MapBounds")){
			outOfBounds = true;//to make the out of bounds procedure
			angleToBackBounds = 180;
		}
	}
	/*
	//_WaypointStartScript1 and WaypointUpdateScript1 is used if a 1 is rolled
	void WaypointStartScript1 (Transform t, float rotateSpeed) {
		
		rotateSpeed = Random.Range(50,70);
		
		//Just to give a little turn
		t.Rotate (0, 0, 20);
		
		if(Random.Range(0,1)==0)
			//Makes it follows not straight paths
			t.Rotate (0, 0, Random.Range(0, 20));
		

	}
	
	// Update is called once per frame
	void WaypointUpdateScript1 (Transform t, float speed, float rotateSpeed) {

		//Calculating the scout speed and its rotation
		float transAmount = speed * Time.deltaTime;
		float rotateAmount = rotateSpeed * Time.deltaTime;
		
		//Unturns it
		t.Rotate (0, 0, -20);
		//Goes forward and rotates in a circle
		t.Translate(0,0,-transAmount);//Needs to be -transAmount because the model is backwards
		t.Rotate (0, rotateAmount, 0);
		//Turns it a little to give the impression of being turning
		t.Rotate (0, 0, 20);
		
	}

	//_WaypointStartScript2 and WaypointUpdateScript2 is used if a 2 is rolled

	void WaypointStartScript2 (Transform t, float rotateSpeed) {}

	void WaypointUpdateScript2 (Transform t, float speed, float rotateSpeed) {}

	//_WaypointStartScript3 and WaypointUpdateScript3 is used if a 3 is rolled

	void WaypointStartScript3 (Transform t, float rotateSpeed) {}
	
	void WaypointUpdateScript3 (Transform t, float speed, float rotateSpeed) {}

	//_WaypointStartScript4 and WaypointUpdateScript1 is used if a 4 is rolled

	void WaypointStartScript4 (Transform t, float rotateSpeed) {}
	
	void WaypointUpdateScript4 (Transform t, float speed, float rotateSpeed) {}
	*/
	
}
