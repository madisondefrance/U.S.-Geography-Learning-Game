import Dustin;

class PlayerMovementController extends AbstractPlayerController{
	
	private var x : float;
	private var y : float;
	private var z : float;
	private var rotate : float;
	
	function Start () {
	 	//AbstractPlayerController.call(this);
		x = 0;
		y = 0;
		z = 0;
	}
	
	function FixedUpdate () 
	{
		applyForceZ();
	    applyForceX();
		applyForceY();
	 	adjustVelocityIfMagnitudeIsAboveSpeedLimit();
	 	performRotation();
	}
	
	function applyForceZ()
	{
	    // W or S key is being pressed. 
	    if(Input.GetAxis("Jump")){
	    	//cameraRelativeVector = Camera.mainCamera.transform.TransformDirection (Vector3.forward);
	    	//rigidbody.AddRelativeForce(cameraRelativeVector * moveAcceleration * Input.GetAxis("Vertical"), ForceMode.Acceleration);
	    	rigidbody.AddRelativeForce(Vector3.forward * super.getAcceleration() * Input.GetAxis("Jump"), ForceMode.Acceleration);
	    }else{
	    	rigidbody.velocity.z *= super.getDegeneration();
	    }
	}
	
	function applyForceX()
	{
	    // A or D keys
	    if(Input.GetAxis("Horizontal")){
			//cameraRelativeVector = Camera.mainCamera.transform.TransformDirection (Vector3.right);
			//rigidbody.AddRelativeForce(cameraRelativeVector * moveAcceleration * Input.GetAxis("Horizontal"), ForceMode.Acceleration);
			rigidbody.AddRelativeForce(Vector3.right * super.getAcceleration() * Input.GetAxis("Horizontal"), ForceMode.Acceleration);
	    }else{
	    	rigidbody.velocity.x *= super.getDegeneration();
	    }
	}
	
	function applyForceY()
	{
	 	// Q or E keys
	    if (Input.GetAxis("Vertical")){
	   		//cameraRelativeVector = Camera.mainCamera.transform.TransformDirection (Vector3.up);
			//rigidbody.AddRelativeForce(cameraRelativeVector * liftAccleration * Input.GetAxis("Jump"), ForceMode.Acceleration);
			//rigidbody.AddRelativeForce(Vector3.up * liftAccleration * -Input.GetAxis("Vertical"), ForceMode.Acceleration);
			rigidbody.AddRelativeForce(Vector3.up * super.getAcceleration() * -Input.GetAxis("Vertical"), ForceMode.Acceleration);
		}else{
	    	//rigidbody.velocity.y *= liftDegeneration;
	    	rigidbody.velocity.y *= super.getDegeneration();
	    }
	    
	    rigidbody.position.y = rigidbody.position.y > super.getMaxHeight() ? super.getMaxHeight()-1 : rigidbody.position.y;
	}
	
	function adjustVelocityIfMagnitudeIsAboveSpeedLimit(){
	 	if(rigidbody.velocity.magnitude > super.getMaxMoveSpeed()){
	 		var newVelocity = rigidbody.velocity.normalized;
			newVelocity *= super.getMaxMoveSpeed();
			rigidbody.velocity = newVelocity;
	 	}
	}
	
	function OnCollisionEnter(collision : Collision)
	{
		transform.Translate(-x, -y, -z);
		x = 0; y = 0; z = 0;	
	}
	
	function performRotation()
	{
	    rotate = 0;
	
	    if (Input.GetMouseButton(0)){
	        rotate = Input.GetAxis("Mouse X") * super.getRotationSpeed();
	    }
	    // rotate the balloon based on the x value
	    transform.Rotate(0, 0, rotate);
	}
}

//PlayerMovementController.prototype = new AbstractPlayerController();