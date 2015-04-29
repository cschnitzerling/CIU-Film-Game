using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static public GameManager main;

	public Player[] players;

	void Awake(){
		main = this;
		if(players.Length != 2) Debug.LogError("MUST BE TWO PLAYERS!! FUCK!");
		for(int i = 0; i < players.Length; i++){
			players[i].id = i;
		}
	}

	void Start(){
		StartCoroutine(StartCountdown());
	}

	IEnumerator StartCountdown(){
		yield return new WaitForSeconds(0.5f);

		UIManager.main.ShowBanner("3");
		yield return new WaitForSeconds(1f);
		UIManager.main.ShowBanner("2");
		yield return new WaitForSeconds(1f);
		UIManager.main.ShowBanner("1");
		yield return new WaitForSeconds(1f);
		UIManager.main.ShowBanner("Fight!");
		yield return new WaitForSeconds(0.5f);

		// Enable player controls
	}
}
