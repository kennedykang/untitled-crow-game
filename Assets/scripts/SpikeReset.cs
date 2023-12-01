using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeReset : MonoBehaviour
{
    public Animator move;

    private void Start()
    {
        move.SetBool("isDead", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("spike"))
        {
            move.SetBool("isDead", true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
