using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
	[SerializeField] private float speed = 1f;

	[HideInInspector] public int id = 0;
	public bool movementEnabled = false;
	public float health = 1f;

	private Transform t;
	private Rigidbody r;

	void Awake () {
		t = transform;
		r = GetComponent<Rigidbody>();
	}
	
	void Update () {

		if(!movementEnabled) return;
		var move = new Vector3(
			Input.GetAxis("Horizontal"+id.ToString()),
			0f,
			Input.GetAxis("Vertical"+id.ToString()));


		r.velocity = move * speed;
	}
}
