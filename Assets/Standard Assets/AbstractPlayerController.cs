using UnityEngine;
using System.Collections;

namespace Dustin
{
	abstract public class AbstractPlayerController : MonoBehaviour {
		
		public float maxMoveSpeed 		= 14.0f; //20
		public float moveAcceleration  	= 0.85f; //8
		public float moveDegeneration  	= 0.98f;
		//public float liftAccleration 	= 0.35f; //1
		//public float liftDegeneration  	= 0.98f;
		public float rotationSpeed  	= 6.0f;
		public float maxHeight  		= 300.0f;
		
		// Update is called once per frame
		protected virtual void FixedUpdate () {}
		
		public float getMaxMoveSpeed(){ return maxMoveSpeed; }
		public float getAcceleration(){ return moveAcceleration; }
		public float getDegeneration(){ return moveDegeneration; }
		public float getRotationSpeed(){return rotationSpeed;}
		public float getMaxHeight(){	return maxHeight;}
	}
}
