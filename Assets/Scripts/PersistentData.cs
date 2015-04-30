using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {
	static public PersistentData main;

	public int round = 0;

	void Awake(){
		if(main) {
			round = main.round;
			Destroy(main.gameObject);
		}

		main = this;
		DontDestroyOnLoad(this);
	}
}