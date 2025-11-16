using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public float speed = 10f;
    public float zMin = -4f;
    public float zMax = 4f;

    private void Update()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) move = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) move = -1f;

        Vector3 position = transform.position;
        position.z += move * speed * Time.deltaTime;
        position.z = Mathf.Clamp(position.z, zMin, zMax);
        transform.position = position;
    }
}
