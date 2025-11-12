using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public Transform ball;
    public float speed = 8f;
    public float reactionDelay = 0.3f;
    public float zMin = -10f;
    public float zMax = 10f;

    private float targetZ;

     void Update()
    {
        if (ball == null) return;
         targetZ = Mathf.Lerp(targetZ,ball.position.z, Time.deltaTime / reactionDelay);
        Vector3 position = transform.position;
        position.z = Mathf.MoveTowards(position.z, targetZ, speed * Time.deltaTime);
        position.z = Mathf.Clamp(position.z, zMin, zMax);
        transform.position = position;
    }
}
