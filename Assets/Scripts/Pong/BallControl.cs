using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float speed = 10f;
    public float addZ = 2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("BallControl: No Rigidbody on this GameObject.");
        }
    }

    private void Start()
    {
        if (rb == null) return;

        rb.useGravity = false;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.linearVelocity = Vector3.right * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Ball hit " + other.collider.name + " (tag: " + other.collider.tag + ")");

        if (rb == null) return;

        var playerPaddle = other.collider.GetComponentInParent<PaddleControl>();
        var aiPaddle = other.collider.GetComponentInParent<AIPaddle>();

        if (playerPaddle != null || aiPaddle != null)
        {
            Transform paddle = (aiPaddle != null ? aiPaddle.transform : playerPaddle.transform);
            float deltaZ = transform.position.z - paddle.position.z;

            Vector3 v = rb.linearVelocity;
            v.x = -v.x;
            v.z += deltaZ * addZ;

            rb.linearVelocity = v.normalized * speed;
            return;
        }

        if (other.collider.CompareTag("Wall"))
        {
            Vector3 v = rb.linearVelocity;
            v.z = -v.z;
            rb.linearVelocity = v.normalized * speed;
            return;
        }

        if (other.collider.CompareTag("LeftGoal"))
        {
            Debug.Log("Hit LeftGoal");

            Vector3 v = rb.linearVelocity;
            v.x = Mathf.Abs(v.x);
            rb.linearVelocity = v.normalized * speed;
            return;
        }

        if (other.collider.CompareTag("RightGoal"))
        {
            Debug.Log("Hit RightGoal");
            Vector3 v = rb.linearVelocity;
            v.x = -Mathf.Abs(v.x);
            rb.linearVelocity = v.normalized * speed;
            return;
        }
    }
}
