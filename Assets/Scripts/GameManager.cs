using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static public GameManager main;

	public Player[] players;

	void Awake(){
		main = this;
	}
}
