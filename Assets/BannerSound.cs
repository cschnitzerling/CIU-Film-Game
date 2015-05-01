using UnityEngine;
using System.Collections;

public class BannerSound : MonoBehaviour {
	public AudioClip s;

	public void PlaySound(){
		AudioSource.PlayClipAtPoint(s, transform.position);
	}
}
