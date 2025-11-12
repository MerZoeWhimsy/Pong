using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public float speed = 10f;
    public float move;

    void Update()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) move = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) move = -1f;

        transform.Translate(0, 0, move * speed * Time.deltaTime);
    }
}

