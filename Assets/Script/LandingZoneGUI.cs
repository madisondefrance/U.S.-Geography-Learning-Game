using UnityEngine;
using System.Collections;
using Dustin;

public class LandingZoneGUI : MonoBehaviour 
{
	public AbstractPlayerController playerController;
	public PhoneGUI					phone;
	
	// Public Members
	public float BG_WIDTH = 300;
	public float BG_HEIGHT = 80;
	public float BUTTON_WIDTH = 120;
	public float BUTTON_HEIGHT = 35;
	
	// Private Members
	private string cityName;
	private string stateName;
	private bool isDisplayingLandingGUI;
	
	private string message;
	private bool   isDisplayingMessage;
	
	void Start()
	{
		isDisplayingLandingGUI = false;
		isDisplayingMessage = false;
	}
	
	void OnGUI () 
	{
		if(isDisplayingLandingGUI)
		{
			float boxStartX = getBoxStartX();
			float boxStartY = getBoxStartY();
				
			// Make a background box
			GUI.Box(new Rect(boxStartX, boxStartY, BG_WIDTH, BG_HEIGHT), (cityName + ", " + stateName));
			
			// Make Upgrade Button
			if(GUI.Button(new Rect((boxStartX + 10),(boxStartY + BG_HEIGHT - (10 + BUTTON_HEIGHT)),
									BUTTON_WIDTH, BUTTON_HEIGHT), "Upgrade Balloon")) 
			{
				if(phone.currentCash() >= 1000){
					phone.takeCash(1000);
					playerController.moveAcceleration += playerController.moveAcceleration * 0.15f;
					playerController.maxMoveSpeed += playerController.maxMoveSpeed * 0.15f;
					message = "Upgraded Balloon Maximum Movement Speed by 15%! Upgraded Balloon Acceleration by 15%!";
				}else if(phone.currentCash() >= 500){
					phone.takeCash(500);
					playerController.maxMoveSpeed += playerController.maxMoveSpeed * 0.10f;
					message = "Upgraded Balloon Maximum Movement Speed by 10%!";
				}else{
					message = "Not enough cash. Need at least $500 to upgrade.";
				}
				
			}
			
			// Make Take Off Button
			if(GUI.Button(new Rect((boxStartX + BG_WIDTH - (10 + BUTTON_WIDTH)),(boxStartY + BG_HEIGHT - (10 + BUTTON_HEIGHT)),
									BUTTON_WIDTH, BUTTON_HEIGHT), "Take Off")) 
			{
				isDisplayingLandingGUI = false;
				messageBoxOff();
			}
			
			if(isDisplayingMessage){
				displayMessageGUI();
			}
		}
	}
	
	float getBoxStartX(){
		return ((Screen.width / 2) - (BG_WIDTH / 2));
	}
	float getBoxStartY(){
		return ((Screen.height - BG_HEIGHT - 50) - (BG_HEIGHT / 2));
	}
	
	public void on(){
		isDisplayingLandingGUI = true;
	}
	public void off(){
		isDisplayingLandingGUI = false;
		messageBoxOff();
	}
	
	public void SetNames(string city, string state){
		cityName = city;
		stateName = state;
	}
	
	public void setMessage(string messageToDisplay){
		message = messageToDisplay;
	}
	public void messageBoxOn(){
		isDisplayingMessage = true;
	}
	public void messageBoxOff(){
		isDisplayingMessage = false;
	}
	
	void displayMessageGUI(){
		GUI.TextArea(new Rect( getBoxStartX(), Screen.height - BUTTON_HEIGHT, BG_WIDTH, BUTTON_HEIGHT), (message)); //TODO
	}

}
