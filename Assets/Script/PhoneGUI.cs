using UnityEngine;
using System.Collections.Generic;

public class PhoneGUI : MonoBehaviour {
	
	public float X_OFFSET = 8;
	public float Y_OFFSET = 8;
	public float WIDTH  = 140;
	public float HEIGHT = 200;
	
	private string[] orders;
	private const string EMPTY = "";
	private const int MAX_NUMBER_OF_ORDERS = 5;
	
	private int cash 				= 0;
	private int completedDeliveries = 0;
	
	string phoneDisplay = EMPTY;
	
	void Start () {
		orders = new string[MAX_NUMBER_OF_ORDERS];
		for(int i = 0; i < MAX_NUMBER_OF_ORDERS; ++i){
			orders[i] = EMPTY;
		}
	}
	
	void OnGUI () 
	{		
		phoneDisplay = 	" Cash: $ " + cash + "\n" +
							" Orders: \n\n";
		
		for(int i = 0; i < MAX_NUMBER_OF_ORDERS; ++i){
			phoneDisplay += "    " + orders[i] + "\n\n";
		}
		
		GUI.TextArea(new Rect(Screen.width - X_OFFSET - WIDTH, Screen.height - Y_OFFSET - HEIGHT, WIDTH, HEIGHT), phoneDisplay);
	}
	
	public bool canAddOrder(){
		for(int i = 0; i < MAX_NUMBER_OF_ORDERS; ++i){
			if(orders[i] == EMPTY){
				return true;
			}
		}
		return false;
	}
	
	public void addOrder(string city, string state)
	{
		for(int i = 0; i < MAX_NUMBER_OF_ORDERS; ++i){
			if(orders[i] == EMPTY){
				orders[i] = randomBool() ? city : state;
				return;
			}
		}
	}
	
	public void removeOrder(string city, string state)
	{
		for(int i = 0; i < MAX_NUMBER_OF_ORDERS; ++i){
			if(orders[i] == city || orders[i] == state){
				orders[i] = EMPTY;
				moveAllOrdersUpward();
				return;
			}
		}
	}
	
	public bool containsOrder(string city, string state)
	{
		for(int i = 0; i < MAX_NUMBER_OF_ORDERS; ++i){
			if(orders[i] == city || orders[i] == state){
				return true;
			}
		}
		return false;
	}
	
	public void addCash(int amount){
		cash += amount;
	}
	public void takeCash(int amount){
		cash -= amount;
	}
	public int currentCash(){
		return cash;
	}
	
	public void completedDelivery(){
		completedDeliveries++;
	}
	public int getCompletedDeliveries(){
		return completedDeliveries;
	}
	
	public int getNumberOfCurrentOrdersOnPhone(){
		int number = 0;
		for(int i = 0; i < MAX_NUMBER_OF_ORDERS; ++i){
			if(orders[i] != EMPTY){
				number++;
			}
		}
		return number;
	}
	
	public int getMaxNumberOfOrdersPossibleToDisplay(){
		return MAX_NUMBER_OF_ORDERS;
	}
	
	private bool randomBool(){
		return Random.value > .5f;
	}
	
	private void moveAllOrdersUpward(){
		for(int i = 1; i < MAX_NUMBER_OF_ORDERS; ++i){
			if(orders[i-1] == EMPTY){
				orders[i-1] = orders[i];
				orders[i] = EMPTY;
			}
		}
	}
}
