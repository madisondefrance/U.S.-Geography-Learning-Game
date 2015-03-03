using UnityEngine;
using System.Collections;

public class HeavyPlane : Enemy {

	
	// Use this for initialization
	void Start () {
		base.startInit();

		speed = 30.0f;
		rotateSpeed = Random.Range(50,70);
		
		setPath(PATH_CIRCLE);
	}
}
