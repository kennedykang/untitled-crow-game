using UnityEngine;

public class SpikeReset : MonoBehaviour
{
    // Position to reset the player to
    private Vector3 resetPosition = new Vector3(-6.88f, -2.48f, 0.03658727f);

    // This function is called when this collider (attached to the player) enters another trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("spike"))
        {
            // Reset this object's (player's) position
            transform.position = resetPosition;
        }
    }
}
