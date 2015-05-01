using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	static public UIManager main;

	public GameObject banner;
	private Animator bannerAnim;
	private TextMesh bannerText;

	public TextMesh roundText;

	public Transform[] healthBars;

	void Awake(){
		main = this;
		bannerAnim = banner.GetComponent<Animator>();
		bannerText = banner.GetComponentInChildren<TextMesh>();

		if(healthBars.Length != 2){
			Debug.LogError("Need two healthbars");
		}
	}

	void Update(){
		var h1 = GameManager.main.players[0].health;
		var h2 = GameManager.main.players[1].health;

		h1 = Mathf.Clamp(h1, 0f, 1f);
		h2 = Mathf.Clamp(h2, 0f, 1f);

		healthBars[0].localPosition = new Vector3(h1/2f - 0.5f, 0f, -0.1f);
		healthBars[0].localScale = new Vector3(h1, 1f, 1f);

		healthBars[1].localPosition = new Vector3(0.5f - h2/2f, 0f, -0.1f);
		healthBars[1].localScale = new Vector3(h2, 1f, 1f);

		roundText.text = PersistentData.main.round.ToString();
	}

	public void ShowBanner(string s){
		bannerAnim.SetTrigger("Show");
		bannerText.text = s;
	}

	public bool IsBannerPlaying(){
		return bannerAnim.GetCurrentAnimatorStateInfo(0).IsName("Shown");
	} 
}
