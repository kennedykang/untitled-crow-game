using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the Player and the level has not been completed yet
        if (collision.gameObject.name == "player" && !levelCompleted)
        {
            levelCompleted = true;
            CompleteLevel(); // Call the method to complete the level
        }
    }

    private void CompleteLevel()
    {
        // Load the next scene based on the current scene's build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
