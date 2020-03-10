using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
			transform.position += new Vector3(0, GameInfo.gameSpeed * Time.deltaTime, 0);
    }
}
