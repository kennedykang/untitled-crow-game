using UnityEngine;

public class SpikeReset : MonoBehaviour
{
    // Position to reset the player to
    private Vector3 resetPosition = new Vector3(-6.88f, -2.48f, 0.03658727f);

    // This function is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset player's position
            other.transform.position = resetPosition;
        }
    }
}
