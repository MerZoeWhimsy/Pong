using UnityEngine;

public class Scoring : MonoBehaviour
{
   [SerializeField] private bool isPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
           Ball ball = collision.gameObject.GetComponent<Ball>();
            if (isPlayer)
            {
                ball.aiScore++;
                ball.UpdateScoreUI();
                ball.ResetBall();
            }
            else
            {
                ball.playerScore++;
                ball.UpdateScoreUI();
                ball.ResetBall();
            }
        }
    }
}
