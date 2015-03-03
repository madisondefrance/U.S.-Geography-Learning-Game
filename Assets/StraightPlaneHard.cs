using UnityEngine;
using System.Collections;

public class StraightPlaneHard : Enemy {

	
	// Use this for initialization
	void Start () {
		
		
		speed = 40.0f;
		rotateSpeed = 50.0f;
		setPath(PATH_STRAIGHT);
		
	}
}
