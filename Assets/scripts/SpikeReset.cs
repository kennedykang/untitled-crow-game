using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeReset : MonoBehaviour
{
    public Animator move;
    //public Rigidbody2D rb;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("spike"))
        {
            Die();
        }
    }

    private void Die()
    {
        //rb.bodyType = RigidbodyType2D.Static;
        move.SetTrigger("died");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
