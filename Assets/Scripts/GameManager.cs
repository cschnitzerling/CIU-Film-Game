using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	static public GameManager main;

	public RuntimeAnimatorController[] characters;

	public Player[] players;
	private Transform campos;

	public Material overlay;
	public Gradient policeLightColors;
	private bool showLights = false;

	public Material[] backgroundMats;
	public GameObject background;
	private float screenshake = 0f;

	void Awake(){
		main = this;
		if(players.Length != 2) Debug.LogError("MUST BE TWO PLAYERS!! FUCK!");
		for(int i = 0; i < players.Length; i++){
			players[i].id = i;
			players[i].inputEnabled = false;
		}

		overlay.color = new Color(0f, 0f, 0f, 0f);
	}

	void Start(){
		var go = new GameObject("CameraPosition");
		campos = go.transform;
		campos.position = Camera.main.transform.position;

		players[0].GetComponentInChildren<Animator>().runtimeAnimatorController = characters[PersistentData.main.p1sprite];
		players[1].GetComponentInChildren<Animator>().runtimeAnimatorController = characters[PersistentData.main.p2sprite];
		
		background.GetComponent<Renderer>().material = backgroundMats[PersistentData.main.round%backgroundMats.Length];
		PersistentData.main.round++;

		StartCoroutine(StartCountdown());
	}

	void Update(){
		screenshake -= Time.deltaTime;
		var ss = Mathf.Clamp(screenshake, 0f, 1f);

		var ct = Camera.main.transform;
		ct.position = campos.position + 
			(ct.up * Mathf.Sin(Time.time*Time.time*1000f) + ct.right * Mathf.Cos(Time.time*Time.time*1000f)) * ss;

		if(showLights){
			overlay.color = policeLightColors.Evaluate(Mathf.PingPong(Time.time, 1f));
		}
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
		if(PersistentData.main.round < 3){
			UIManager.main.ShowBanner("Player " + (1-id).ToString() + "\nWon");
			RunAfterDelay(1f, () => Application.LoadLevel("main"));
		}else{
			StartCoroutine(GameCompleteSequence(id));
		}
	}

	IEnumerator GameCompleteSequence(int id){
		PersistentData.main.round = 0;
		foreach(var p in players){
			p.inputEnabled = false;
		}

		yield return new WaitForSeconds(1f);
		GetComponent<AudioSource>().Play();
		showLights = true;

		yield return new WaitForSeconds(3f);
		UIManager.main.ShowBanner("Player " + (1-id).ToString() + "\nwas arrested");

		yield return new WaitForSeconds(2f);
		Application.LoadLevel("start");
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
