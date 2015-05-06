using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "BasementInteractive_Project-Flicker";
	private const string gameName = "DevelopersRoom";
 	bool isLAN;
 	int port;
 	public string ip;
 	string pass;

	public GameObject playerPrefab;
	public GameObject ipField;
	public GameObject portField;
	public GameObject isLANField;
	public GameObject passField;

	public void setVariables() {
		ip = ipField.GetComponent<InputField>().text;
		pass = passField.GetComponent<InputField>().text;
		isLAN = isLANField.GetComponent<Toggle>().isOn;
		string portRaw = portField.GetComponent<InputField> ().text;
		if (portRaw != "") {
			port = int.Parse (portRaw);
		} else {
			port = 25000;
		}
	}

	void OnServerInitialized()	{
    	SpawnPlayer();
	}
 
	void OnConnectedToServer() {
    	SpawnPlayer();
	}
	public void SpawnPlayer() {
    	Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
	}

	private HostData[] hostList;
 
	private void RefreshHostList() {
    	MasterServer.RequestHostList(typeName);
	}
 
	void OnMasterServerEvent(MasterServerEvent msEvent)	{
    	if (msEvent == MasterServerEvent.HostListReceived) {
        	hostList = MasterServer.PollHostList();
    	}
	}
	
	public void StartServer() {
		setVariables();
    	Network.InitializeServer(4, port, !Network.HavePublicAddress());
    	if(!isLAN) {
    		MasterServer.RegisterHost(typeName, gameName);
    	}
	}
	void JoinServer(HostData hostData){
		setVariables();
    	Network.Connect(hostData,pass);
	}
	public void JoinServer(){
		setVariables();
    	Network.Connect(ip, port, pass);
	}
	void OnGUI() {
		if (GUI.Button(new Rect(20, 500, 160, 50), "Refresh Hosts")) {
			RefreshHostList();
		}
		if (hostList != null) {
			for (int i = 0; i < hostList.Length; i++) {
				if (GUI.Button(new Rect(20, 500+ (60 * i), 160, 50), hostList[i].gameName)) {
					JoinServer(hostList[i]);
				}
			}
		}
	}
}
