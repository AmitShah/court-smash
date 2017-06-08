using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;

public class MqttNetworkManager : MonoBehaviour {
	private MqttClient client;
	// Use this for initialization
    public string streamtext;
	public TextMesh text;


    [Serializable]
    public class MyClass
    {
        public string streams;
    }

    void Start () {
		// create client instance 
		//client = new MqttClient(IPAddress.Parse("54.70.162.111"),443 , false , null ); 
		client = new MqttClient("localhost",1883,false,null);

		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		
        
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		var hc = GameObject.Find ("HitCount");
		this.text = hc.GetComponent<TextMesh>();


	}
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 

		Debug.Log(System.Text.Encoding.UTF8.GetString(e.Message));
        /*GameObject streamtext = new GameObject(System.Text.Encoding.UTF8.GetString(e.Message));
        //   GameObject.Find("DataStream").SetComponent<GUIText>(streamtext);
        Text text;
        text = GameObject.Find("DataStream").GetComponent<Text>();
        //text = GameObject.Find("DataStream").GetComponent<GUIText>();
        //text.text = System.Text.Encoding.UTF8.GetString(e.Message);
        text.text = "Works";
        */
        streamtext = System.Text.Encoding.UTF8.GetString(e.Message).ToString();


    }

    

	void OnGUI(){
	/*	if ( GUI.Button (new Rect (20,40,80,20), "Level 1")) {
			Debug.Log("sending...");
			//client.Publish("hello/world", System.Text.Encoding.UTF8.GetBytes("Sending from Unity3D!!!"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}*/
	}
	// Update is called once per frame
	void Update () {

        //   GameObject.Find("DataStream").SetComponent<GUIText>(streamtext);
        //Text text = GameObject.Find("DataStream").GetComponent<Text>();
        //text.text = streamtext;
		text.text =streamtext;

    }
}
