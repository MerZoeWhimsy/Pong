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

        PositionDirection pd = collision.collider.GetComponent<PositionDirection>();

        if (pd != null)
        {
            if (pd.d)
                direction.x = -direction.x;
            else 
                direction.z = -direction.z;
        }
    }

    private void SetRandomDirection()
    {
        float x;
        float z;

        /* do
         {
             x = Random.Range(-1f, 1f);
             z = Random.Range(-1f, 1f);
         }

         while (Mathf.Abs(x) < 0.5f)*/

        x = Random.Range(-0.5f, 0.5f) + 1;
        z = Random.Range(-1f, 1f);

        direction = new Vector3(x, 0, z);
        direction.Normalize();
    }
    public void ResetBall()
    {
        CenterBall();
        SetRandomDirection();
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Player: {playerScore} | AI: {aiScore}";
        }
    }
}
