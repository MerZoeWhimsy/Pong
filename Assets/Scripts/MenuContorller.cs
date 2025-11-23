using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuContorller : MonoBehaviour
{
  public void StartGame()
    {
        SceneManager.LoadScene("Pong");
    }
}
