using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public Transform ball;
    public float speed = 8f;
    public float zMin = 0f;
    public float zMax = 0f;
    public float error = 3f;

    private void Update()
    {
        if (ball == null)
        {
            Debug.LogWarning("AIPaddle: ball is NOT assigned!");
            return;
        }

        float currentError = Random.Range(-error, error);
        float targetZ = ball.position.z + Random.Range(-error, error);

        Vector3 position = transform.position;
        position.z = Mathf.MoveTowards(position.z, targetZ, speed * Time.deltaTime);
        position.z = Mathf.Clamp(position.z, zMin, zMax);
        transform.position = position;
    }
}
