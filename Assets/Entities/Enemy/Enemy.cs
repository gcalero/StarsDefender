using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 150f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound; 

	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSeconds;
	public ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0f) {
				scoreKeeper.Score(scoreValue);
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				Destroy(gameObject);
			}
		}
	}

	void Update ()
	{
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability) {
			Fire();
		}	
	}

	void Fire() {
		GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity);
		missile.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, - projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}


}
