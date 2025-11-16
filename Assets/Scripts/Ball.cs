using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
   public float speed = 10f;
    private Vector3 direction;

    public int playerScore = 0;
    public int aiScore = 0;

    public TextMeshProUGUI scoreText;


    private void Start()
    {
        CenterBall();
        SetRandomDirection();        
    }
    private void Update()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;

    }

    private void CenterBall()
    {
        //we put the ball in the center
        transform.position = new Vector3(0, transform.position.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ball hit: " + collision.collider.name);
        if (collision.collider.name == "RightWall")
        {
            playerScore++;
            UpdateScoreUI();
            ResetBall();
            return;
        }
        if (collision.collider.name == "LeftWall")
        {
            aiScore++;
            UpdateScoreUI();
            ResetBall();
            return;
        }
        if (collision.collider.name == "BottomWall" || collision.collider.name == "TopWall")
        {
            direction.z = -direction.z;
            return;
        }
        if (collision.collider.name == "RightPaddle" || collision.collider.name == "LeftPaddle")
        {
            direction.x = -direction.x;
            return;
        }
    }

    private void SetRandomDirection()
    {
        float x;
        float z;

        do
        {
            x = Random.Range(-1f, 1f);
            z = Random.Range(1f, 1f);
        }

        while (Mathf.Abs(x) < 0.5f);

        direction = new Vector3(x, 0, z);
        direction.Normalize();
    }
    private void ResetBall()
    {
        CenterBall();
        SetRandomDirection();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Player: {playerScore} | AI: {aiScore}";
        }
    }
}
