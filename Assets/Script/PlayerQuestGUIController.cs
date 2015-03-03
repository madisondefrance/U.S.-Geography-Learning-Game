using UnityEngine;
using System.Collections;

public class PlayerQuestGUIController : MonoBehaviour 
{
	public LandingZoneGUI landingZoneGUI;
	public PhoneGUI 	  phone;
	
	public string[] questCompletedMessages = new string[]{"Thank You!"};
	string message = "Thank You!";
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "CityLanding")
		{
			string city = other.gameObject.GetComponent<StateStruct>().capitalCityName;
			string state = other.gameObject.GetComponent<StateStruct>().stateName;
			
			landingZoneGUI.SetNames(city, state);
			landingZoneGUI.on();
			
			if(phone.containsOrder(city, state)){
				setRandomDeliveryMessage();
				landingZoneGUI.setMessage(message);
				phone.removeOrder(city, state);
				phone.completedDelivery();
				
				phone.addCash(100);
			}else{
				landingZoneGUI.setMessage(" Wrong Location!");
			}
			
			landingZoneGUI.messageBoxOn();
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "CityLanding")
		{
			landingZoneGUI.off();
		}
	}
	
	void setRandomDeliveryMessage(){
		message = questCompletedMessages[Random.Range(0, questCompletedMessages.Length)];
	}
}
