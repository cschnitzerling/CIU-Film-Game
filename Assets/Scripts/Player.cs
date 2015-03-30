using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
	public float speed = 1f;

	private Transform t;
	private Rigidbody r;

	void Awake () {
		t = transform;
		r = GetComponent<Rigidbody>();
	}
	
	void Update () {
		var move = new Vector3(
			Input.GetAxis("Horizontal"),
			0f,
			Input.GetAxis("Vertical"));


		r.velocity = move * speed;
	}
}
