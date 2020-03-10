using UnityEngine;

public class BulletController : MonoBehaviour
{
	private Vector3 targetPosition;
	private float speed;
	Vector3 controlPoint;
	float angle;
	bool clockwise;
	float birthTime;

	public void Inintialize(Vector3 targetPosition, float speed)
	{
		this.targetPosition = targetPosition;
		birthTime = Time.time;
		this.speed = speed;
		//get the direction to know the angle offset
		clockwise = Vector2.SignedAngle(new Vector2(transform.position.x, transform.position.z),new Vector2(targetPosition.x, targetPosition.z)) < 0;
		SetRotation();
	}

	void Update()
	{
		SetRotation();
		if(targetPosition != Vector3.zero)
			transform.position += transform.forward * speed * Time.deltaTime;
			//deactivate the bullet and add it to the queue
		if(Time.time > birthTime + 10)
		{
			GameInfo.unusedBulletPool.Enqueue(gameObject);
			gameObject.SetActive(false);
		}
	}

	void SetRotation()
	{
		angle = Vector2.SignedAngle(new Vector2(transform.position.x, transform.position.z),new Vector2(15.5f, 0));
		transform.LookAt(targetPosition);
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, clockwise ? angle - 180 : angle, transform.rotation.eulerAngles.z);
	}
}
