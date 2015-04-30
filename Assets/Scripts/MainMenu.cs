using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject[] menus;

	public void StartGame(){
		Application.LoadLevel("main");
	}

	public void EndGame(){
		Application.Quit();
	}

	public void SetMenu(int m){
		for(int i = 0; i < menus.Length; i++){
			if(i == m){
				menus[i].SendMessage("SetOpen", true);
			}else{
				menus[i].SendMessage("SetOpen", false);
			}
		}
	}
}
