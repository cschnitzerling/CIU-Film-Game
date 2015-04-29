using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	static public UIManager main;

	public GameObject banner;
	private Animator bannerAnim;
	private TextMesh bannerText;

	void Awake(){
		main = this;
		bannerAnim = banner.GetComponent<Animator>();
		bannerText = banner.GetComponentInChildren<TextMesh>();
	}

	void Start(){

	}

	public void ShowBanner(string s){
		bannerAnim.SetTrigger("Show");
		bannerText.text = s;
	}

	public bool IsBannerPlaying(){
		return bannerAnim.GetCurrentAnimatorStateInfo(0).IsName("Shown");
	} 
}
