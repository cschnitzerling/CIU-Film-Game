using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
	[SerializeField] private float speed = 1f;

	[HideInInspector] public int id = 0;
	public bool inputEnabled = false;
	public float health = 1f;

	[SerializeField] private float attackCooldown = 0.1f;
	private float attackTimeout = 0f;

	private bool enemyInRange = false;
	private bool dead = false;

	private Transform t;
	private Rigidbody r;

	private Animator anim;

	void Awake () {
		t = transform;
		r = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
	}
	
	void Update () {
		if(dead) return;
		if(health < 0f) {
			dead = true;
			GameManager.main.OnPlayerDeath(id);
			return;
		}

		attackTimeout -= Time.deltaTime;

		if(!inputEnabled) return;
		var ids = id.ToString();

		if(attackTimeout < 0f && Input.GetButtonDown("Attack"+ids)){
			Attack();
			attackTimeout = attackCooldown;
		}

		var move = Vector3.right * Input.GetAxis("Horizontal"+ids);

		r.velocity = move * speed;

		//Animation Set Walk or Idle///////////////////////////////
		anim.SetFloat ("Float_Speed", Mathf.Abs(move.x)*speed); //
		/////////////////////////////////////////////////////////
	}

	void OnTriggerStay(Collider col){
		enemyInRange = true;
	}

	void OnTriggerExit(Collider col){
		enemyInRange = false;
	}

	void Attack(){
		//Animation Punch Trigger///////////////////
		anim.SetTrigger("Trig_Punch");			 //
		//////////////////////////////////////////

		if(!enemyInRange) return;
		print("Attack " + id.ToString());

		var otherPlayer = GameManager.main.players[1-id];
		otherPlayer.health -= 0.1f;

		AudioSource.PlayClipAtPoint(GameManager.main.punch, t.position);

		GameManager.main.ScreenShake(0.2f);
	}
}
