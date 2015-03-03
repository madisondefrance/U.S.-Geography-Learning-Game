using UnityEngine;
using System.Collections;

public class StraightPlane : Enemy {

	
	// Use this for initialization
	void Start () {
		
		
		speed = 80.0f;
		rotateSpeed = 50.0f;
		setPath(PATH_STRAIGHT);
		
	}
}
