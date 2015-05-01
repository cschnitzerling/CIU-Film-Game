using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {
	static public PersistentData main;

	public int round = 0;
	public int p1sprite = 0;
	public int p2sprite = 0;

	void Awake(){
		if(main) {
			round = main.round;
			p1sprite = main.p1sprite;
			p2sprite = main.p2sprite;
			Destroy(main.gameObject);
		}

		main = this;
		DontDestroyOnLoad(this);
	}
}