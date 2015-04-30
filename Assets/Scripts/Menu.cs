using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour {
	private Animator anim;
	private CanvasGroup group;

	public bool enabledByDefault = false;

	void Awake() {
		anim = GetComponent<Animator>();
		SetOpen(enabledByDefault);
	}

	public void SetOpen(bool open){
		anim.SetBool("open", open);
	}
}
