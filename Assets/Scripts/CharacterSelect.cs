using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {
	public GameObject[] p1selections;
	public GameObject[] p2selections;

	private int p1select = 0;
	private int p2select = 0;

	private bool p1ready = false;
	private bool p2ready = false;

	private float p1stimeout = 1f;
	private float p2stimeout = 1f;

	void Start(){
		UpdateSelection(p1selections, p1select);
		UpdateSelection(p2selections, p2select);
	}

	void Update(){
		if(p1ready && p2ready) return;

		p1stimeout -= Time.deltaTime;
		p2stimeout -= Time.deltaTime;

		var p1mf = Input.GetAxis("Horizontal0")*100f;
		var p2mf = Input.GetAxis("Horizontal1")*100f;

		int p1m = (p1mf==0f)?0:Mathf.RoundToInt(p1mf / Mathf.Abs(p1mf));
		int p2m = (p2mf==0f)?0:Mathf.RoundToInt(p2mf / Mathf.Abs(p2mf));

		if(!p1ready && p1stimeout <= 0f && p1m != 0){
			p1select = Mathf.Clamp(p1select+p1m, 0, p1selections.Length-1);
			UpdateSelection(p1selections, p1select);
			p1stimeout = 0.2f;
		}
		if(!p2ready && p2stimeout <= 0f && p2m != 0){
			p2select = Mathf.Clamp(p2select+p2m, 0, p2selections.Length-1);
			UpdateSelection(p2selections, p2select);
			p2stimeout = 0.2f;
		}

		if(p1stimeout <= 0f && Input.GetButtonDown("Attack0")) {
			p1selections[p1select].GetComponent<Animator>().SetTrigger("final");
			p1ready = true;
		}
		if(p2stimeout <= 0f && Input.GetButtonDown("Attack1")) {
			p2selections[p2select].GetComponent<Animator>().SetTrigger("final");
			p2ready = true;
		}

		if(p1ready && p2ready){
			PersistentData.main.p1sprite = p1select;
			PersistentData.main.p2sprite = p2select;
			FindObjectOfType<MainMenu>().StartGame();
		}
	}

	void UpdateSelection(GameObject[] ss, int s){
		for(int i = 0; i < ss.Length; i++){
			ss[i].GetComponent<Animator>().SetBool("selected", i == s);
		}
	}
}
