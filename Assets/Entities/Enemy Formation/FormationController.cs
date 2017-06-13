using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float speed;
	public float width;
	public float height;
	public float spawnDelay;

	private Vector3 currentDirection;
	private float minX;
	private float maxX;

	// Use this for initialization
	void Start ()
	{
		InitBounds();
		SpawnUntilFull();
		//SpawnEnemies();

	}

	private void InitBounds() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		float paddingX = width / 2;
		minX = leftmost.x + paddingX;
		maxX = rightmost.x - paddingX;
	}

	/*public void SpawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}
	}*/

	void SpawnUntilFull ()
	{
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity);
			enemy.transform.parent = freePosition;
			freePosition = NextFreePosition();
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height, 0f));
	}

	// Update is called once per frame
	void Update ()
	{
		UpdatePosition();
		if (AllMembersDead()) {
			SpawnUntilFull();
		}
	}

	bool AllMembersDead ()
	{
		foreach (Transform childPosGameObject in transform) {
			if (childPosGameObject.childCount > 0) return false;
		}
		return true;
	}

	Transform NextFreePosition ()
	{
		foreach (Transform childPosGameObject in transform) {
			if (childPosGameObject.childCount == 0) {
				return childPosGameObject;
			}
		}
		return null;
	}

	private void UpdatePosition ()
	{
		if (currentDirection == Vector3.zero) {
			currentDirection = Vector3.right;
		}

		Vector3 currentSpeed = currentDirection * Time.deltaTime * speed;

		transform.position += currentSpeed;

		if (transform.position.x >= maxX || transform.position.x <= minX) {
			currentDirection *= -1;
		}

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

	
	}
}
