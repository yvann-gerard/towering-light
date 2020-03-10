using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float rotationSpeed;
	public Camera maincamera;
	public float speed;
	public GameObject bulletPrefab;
	private List<GameObject> bullets = new List<GameObject>();
	public float shootSpeed;
	private bool canShoot = true;
	private GameObject bulletToDestroy;

	void Update()
	{
		//Rotate the player and camera around the vertical axis
		transform.RotateAround(new Vector3(0, transform.position.y, 0), Vector3.up, -Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed);
		maincamera.transform.RotateAround(new Vector3(0, transform.position.y, 0), Vector3.up, -Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed);

		transform.Translate((GameInfo.gameSpeed * Time.deltaTime + speed * Input.GetAxisRaw("Vertical") * Time.deltaTime) * Vector3.forward);
		float playerY = Mathf.Clamp(transform.position.y, maincamera.transform.position.y-5.5f, maincamera.transform.position.y+6);
		transform.position = new Vector3(transform.position.x, playerY, transform.position.z);

		if(Input.GetButton("Fire") && canShoot)
			StartCoroutine(Shoot());
		foreach (GameObject bullet in bullets)
		{
			bullet.transform.position += transform.forward * shootSpeed * Time.deltaTime;
			if(bullet.transform.position.y >= maincamera.transform.position.y + 20)
				bulletToDestroy = bullet; //Remove the bullet after the foreach is finished
		}
		if(bulletToDestroy)
		{
			bullets.Remove(bulletToDestroy);
			Destroy(bulletToDestroy);
			bulletToDestroy = null;
		}	
	}

	IEnumerator Shoot()
	{
		canShoot = false;
		GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(transform.position + Vector3.up));
		bullets.Add(bullet);
		yield return new WaitForSeconds(1/GameInfo.fireRate);
		canShoot = true;
	}
}