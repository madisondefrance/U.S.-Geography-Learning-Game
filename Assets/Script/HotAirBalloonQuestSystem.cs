using UnityEngine;
using System.Collections;

public class HotAirBalloonQuestSystem : MonoBehaviour {
	
	// Public Members
	public float WIDTH = 300;
	public float HEIGHT = 80;
	
	public Transform 	balloon;
	public PhoneGUI 	phone;
	
	public int maxNumberOfDeliveryRequests  		= 3;
	public int minimumQuestApperanceTimeInSeconds 	= 15;
	private float lastQuestAppearanceTime 			= 0;
	
	public int NUMBER_OF_DELIVERIES_UNTIL_WIN = 50;
	
	public StateStruct[] LOCATIONS = new StateStruct[50];
	
	private int[] orders;
	private int orderIndex = 0;
	
	private bool displayWinMessage = false;
	private bool  hasWon = false;
	private float completionTime;
	
	// Use this for initialization
	void Start () {
		orders = new int[NUMBER_OF_DELIVERIES_UNTIL_WIN];
		for(int i = 0; i < NUMBER_OF_DELIVERIES_UNTIL_WIN; ++i){
			orders[i] = i;
		}
		
		int tempIndex;
		int tempVal;
		//SHUFFLE
		for(int i = 0; i < NUMBER_OF_DELIVERIES_UNTIL_WIN; ++i){
			tempIndex = Random.Range(0,NUMBER_OF_DELIVERIES_UNTIL_WIN);
			tempVal = orders[tempIndex];
			orders[tempIndex] = orders[i];
			orders[i] = tempVal;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		 addOrderIfGoodTiming();
		
		if(isWinConditionSatisfied()){
			displayWinMessage = true;
		}
	}
	
	void OnGUI () 
	{
		if(displayWinMessage){
			string message = 	" YOU WIN!\n" +
			 					" Completion time is: " + completionTime + " seconds.";
			GUI.TextArea(new Rect( ((Screen.width / 2) - (WIDTH / 2)), (Screen.height/2) - (HEIGHT/2), WIDTH, HEIGHT), (message));
		}
	}
	
	bool isWinConditionSatisfied(){
		if(!hasWon && phone.getCompletedDeliveries() == NUMBER_OF_DELIVERIES_UNTIL_WIN){
			completionTime = (Time.realtimeSinceStartup);
			hasWon = true;
		}
		return hasWon;
	}
	
	void addOrderIfGoodTiming(){
		if(orderIndex < NUMBER_OF_DELIVERIES_UNTIL_WIN
		&& phone.getNumberOfCurrentOrdersOnPhone() < maxNumberOfDeliveryRequests){
			if(Random.Range(0, (int)Mathf.Abs(balloon.transform.position.y)) == 0 
			&& (Time.realtimeSinceStartup - minimumQuestApperanceTimeInSeconds >= lastQuestAppearanceTime)){
				if(phone.getNumberOfCurrentOrdersOnPhone() == 0){
					addOrderToPhone();
				}else if( (Random.Range(0,100)) == 0 ){
					addOrderToPhone();
				}
			}
			else if(phone.getCompletedDeliveries() == 0
				 && phone.getNumberOfCurrentOrdersOnPhone() == 0
				 && (Random.Range(0,300)) == 0){
					addOrderToPhone();
			}
		}
	}
	
	void addOrderToPhone(){
		phone.addOrder(LOCATIONS[orders[orderIndex]].capitalCityName, LOCATIONS[orders[orderIndex]].stateName);
		orderIndex += 1;
		lastQuestAppearanceTime = Time.realtimeSinceStartup;
		extendPhoneOrdersListingIfGoodTimeToDoSo();
	}
	
	void extendPhoneOrdersListingIfGoodTimeToDoSo(){
		if(phone.getCompletedDeliveries() == 5 && maxNumberOfDeliveryRequests < phone.getMaxNumberOfOrdersPossibleToDisplay()){
			maxNumberOfDeliveryRequests++;
		}
		else if(phone.getCompletedDeliveries() == 10 && maxNumberOfDeliveryRequests < phone.getMaxNumberOfOrdersPossibleToDisplay()){
			maxNumberOfDeliveryRequests++;
		}
	}
}
