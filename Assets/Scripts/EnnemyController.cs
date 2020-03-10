using System.Collections;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
	public float speed;
	public float rotationSpeed;
	public AnimationCurve sinCurve;
	public Transform target;
	public float shotSpeed;
	public float screenLimit;

	private void Start() 
	{
		screenLimit = transform.position.y - 20;
		StartCoroutine(Shoot());
	}

	private void Update() 
	{
		if(transform.position != Vector3.zero)
		{
			MoveVertically();
			MoveHorizontally(sinCurve.Evaluate(Time.time));
			if(transform.position.y <= screenLimit)
				Destroy(gameObject);
		}
	}

	void MoveVertically()
	{
		transform.Translate((GameInfo.gameSpeed * Time.deltaTime + speed * Time.deltaTime) * Vector3.forward);
	}

	void MoveHorizontally(float direction)
	{
		transform.RotateAround(new Vector3(0, transform.position.y, 0), Vector3.up, direction * Time.deltaTime * rotationSpeed);
	}

	IEnumerator Shoot()
	{
		while (true)
		{
			//Get a bullet from the queue to avoid instantiation
			if(GameInfo.unusedBulletPool.Count > 0){
				GameObject currentBullet = GameInfo.unusedBulletPool.Dequeue();
				currentBullet.SetActive(true);
				currentBullet.transform.position = transform.position;
				currentBullet.transform.rotation = Quaternion.LookRotation(target.transform.position);
				if(target)
					currentBullet.GetComponent<BulletController>().Inintialize(target.position, 1);
				}
			yield return new WaitForSeconds(1/shotSpeed);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "PlayerBullet")
			Destroy(gameObject);
	}
}
