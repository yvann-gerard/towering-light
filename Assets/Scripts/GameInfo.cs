using System.Collections.Generic;
using UnityEngine;

public class GameInfo : ScriptableObject
{
	public static float gameSpeed = 2f;
	public static Queue<GameObject> unusedBulletPool = new Queue<GameObject>();
	public static float fireRate = 0.5f;
}
