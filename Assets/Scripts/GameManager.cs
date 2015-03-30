using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static public GameManager main;

	[SerializeField] private Level[] levels;
	[SerializeField] private uint currentLevel;

	private Player player;

	void Awake(){
		main = this;
	}

	void Start(){
		levels = GameObject.Find("Levels").GetComponentsInChildren<Level>();
		currentLevel = 0;

		player = FindObjectsOfType<Player>()[0];
	}

	void Update(){
		var playerPos = player.transform.position;
		for(uint i = 0; i < levels.Length; i++){
			if(levels[i].PointInBounds(playerPos)) {
				currentLevel = i;
				break;
			}
		}

		var curLevel = GetCurrentLevel();
		if(!curLevel) return;

		var ct = Camera.main.transform;
		ct.position = playerPos;
		ct.Translate(-Vector3.forward * 8f);
		curLevel.ConstrainCamera();
	}

	Level GetCurrentLevel(){
		if(currentLevel < levels.Length) return levels[currentLevel];
		return null;
	}
}
