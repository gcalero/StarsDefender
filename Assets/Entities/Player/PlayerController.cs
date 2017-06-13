using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float stepX;
	private float minX;
	private float maxX;

	public GameObject projectile;
	public AudioClip fireSound;

	public float firingRate = 0.2f;
	public float projectileSpeed = 5f;
	public float health = 250f;



	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		float padding = GetComponent<Renderer>().bounds.size.x / 2;
		minX = leftmost.x + padding;
		maxX = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)) {
			gameObject.transform.position = new Vector3 (Mathf.Clamp (transform.position.x - stepX * Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			gameObject.transform.position = new Vector3 (Mathf.Clamp (transform.position.x + stepX * Time.deltaTime, minX, maxX), transform.position.y, transform.position.z);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("Fire", 0.00001f, firingRate);
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		}

	}

	private void Fire() {
		Vector3 startPosition = transform.position + new Vector3 (0, 1, 0);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity);
		beam.GetComponent<Rigidbody2D>().velocity = Vector3.up * projectileSpeed;
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Debug.Log ("Player collided with missile");
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0f) {
				Die();
			}
		}
	}

	void Die() {
		Destroy(gameObject);
		GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadLevel("Game Over");
	}

}
