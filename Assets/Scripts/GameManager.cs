using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static public GameManager main;

	public RuntimeAnimatorController[] characters;

	public Player[] players;
	private Transform campos;

	private float screenshake = 0f;

	void Awake(){
		main = this;
		if(players.Length != 2) Debug.LogError("MUST BE TWO PLAYERS!! FUCK!");
		for(int i = 0; i < players.Length; i++){
			players[i].id = i;
			players[i].inputEnabled = false;
		}
	}

	void Start(){
		var go = new GameObject("CameraPosition");
		campos = go.transform;
		campos.position = Camera.main.transform.position;

		players[0].GetComponentInChildren<Animator>().runtimeAnimatorController = characters[PersistentData.main.p1sprite];
		players[1].GetComponentInChildren<Animator>().runtimeAnimatorController = characters[PersistentData.main.p2sprite];
		PersistentData.main.round++;

		StartCoroutine(StartCountdown());
	}

	void Update(){
		screenshake -= Time.deltaTime;
		var ss = Mathf.Clamp(screenshake, 0f, 1f);

		var ct = Camera.main.transform;
		ct.position = campos.position + 
			(ct.up * Mathf.Sin(Time.time*Time.time*1000f) + ct.right * Mathf.Cos(Time.time*Time.time*1000f)) * ss;
	}

	IEnumerator StartCountdown(){
		yield return new WaitForSeconds(0.5f);

		ScreenShake(1f);
		UIManager.main.ShowBanner("3");
		yield return new WaitForSeconds(1f);
		ScreenShake(1f);
		UIManager.main.ShowBanner("2");
		yield return new WaitForSeconds(1f);
		ScreenShake(1f);
		UIManager.main.ShowBanner("1");
		yield return new WaitForSeconds(1f);
		ScreenShake(1f);
		UIManager.main.ShowBanner("Fight!");
		yield return new WaitForSeconds(1f);

		foreach(var p in players){
			p.inputEnabled = true;
		}
	}

	public void OnPlayerDeath(int id){
		UIManager.main.ShowBanner("Player " + (1-id).ToString() + "\nWins");

		if(PersistentData.main.round < 3){
			RunAfterDelay(1f, () => Application.LoadLevel("main"));
		}else{
			RunAfterDelay(1f, () => Application.LoadLevel("start"));
		}
	}

	public void ScreenShake(float amt = 1f){
		screenshake = Mathf.Max(0f, screenshake) + amt;
	}

	public delegate void DelayRunDel();
	public void RunAfterDelay(float time, DelayRunDel del){
		StartCoroutine(_RunAfterDelayImpl(time, del));
	}

	private IEnumerator _RunAfterDelayImpl(float time, DelayRunDel del){
		yield return new WaitForSeconds(time);
		del();
	}
}
