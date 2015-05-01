using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject[] menus;
	private int currentMenu = 0;

	void Start(){
		FindObjectOfType<CharacterSelect>().enabled = false;
	}

	void Update(){
		if(Input.GetButtonDown("Start")){
			if(currentMenu == 0){
				SetMenu(1);
			}
		}

		if(Input.GetButtonDown("Back")){
			if(currentMenu == 1){
				SetMenu(0);
			}else{
				EndGame();
			}
		}
	}

	public void StartGame(){
		Application.LoadLevel("main");
	}

	public void EndGame(){
		Application.Quit();
	}

	public void SetMenu(int m){
		for(int i = 0; i < menus.Length; i++){
			menus[i].SendMessage("SetOpen", i == m);

			if(i == m){
				currentMenu = m;
			}
		}

		if(m == 1){
			FindObjectOfType<CharacterSelect>().enabled = true;
		}
	}
}
