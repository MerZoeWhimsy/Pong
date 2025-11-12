using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class BallControl : MonoBehaviour
{
    public float speed = 10f;
    public float addZ = 4f;
    public int playerScore = 0;
    public int aiScore = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = Vector3.right * speed;
    }

  

    void OnCollisionEnter(Collision c)
    {
    rb.linearVelocity = Vector3.Reflect(
            rb.linearVelocity,
            c.contacts[0].normal
            ).normalized * speed;

        if (c.collider.CompareTag("RightWall"))
        {
            playerScore += 1;
            Debug.Log($"Player: {playerScore} | AI: {aiScore}");
            ResetBall(-1);
        }
        else if (c.collider.CompareTag("LeftWall"))
        {
            aiScore += 1;
            Debug.Log($"Player: {playerScore} | AI: {aiScore}");
            ResetBall(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIPaddle") || other.CompareTag("Paddle"))
        {
            Vector3 v = rb.linearVelocity;
            float deltaZ = transform.position.z - other.transform.position.z;
            v.x = -v.x;
            v.z += deltaZ * addZ;
            rb.linearVelocity = v.normalized * speed;
        }
    }

    void ResetBall(int direction)
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector3.right * direction * speed;
    }
}
