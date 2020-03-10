using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject Ennemy;
	public Transform cameraPosition;
	public GameObject bullet;

	void Awake()
	{
		//Populate a queue of bullet to avoid instantiation wile playing
		for (int i = 0; i < 200; i++)
			GameInfo.unusedBulletPool.Enqueue(Instantiate(bullet, Vector3.zero, Quaternion.identity));
		StartCoroutine(SpawnEnnemy());
	}

	IEnumerator SpawnEnnemy()
	{
		while (true)
		{
			float angle = Random.Range(-180,180);
			GameObject CurrentEnnemy = Instantiate(Ennemy, new Vector3(Mathf.Sin(angle) * 15.5f, cameraPosition.position.y + 20, Mathf.Cos(angle) * 15.5f), Quaternion.identity);
			float angleCenter = Vector2.SignedAngle(new Vector2(transform.position.x, transform.position.z),new Vector2(15.5f, 0));
			CurrentEnnemy.transform.LookAt(new Vector3(0, CurrentEnnemy.transform.position.y, 0), Vector3.up);
			CurrentEnnemy.transform.Rotate(90, 180, 0);
			yield return new WaitForSeconds(1);
		}
	}
}
