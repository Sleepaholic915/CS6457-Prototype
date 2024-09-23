using UnityEngine;

namespace CuteTownAssetPack
{
public class FollowCharacter : MonoBehaviour
{
    public Transform character; // Reference to the character's transform
    public Vector3 offset; // Offset from the character's position
    public float followSpeed = 5f; // Speed at which the camera follows the character

    private void FixedUpdate()
    {
        if (character == null)
        {
            Debug.LogWarning("Character transform not assigned to the camera script.");
            return;
        }

        // Calculate the target position for the camera
        Vector3 targetPosition = character.position + offset;

        // Smoothly interpolate towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        
        // Apply the new position to the camera
        transform.position = smoothedPosition;

        // Ensure the camera is always looking at the character
        transform.LookAt(character);
    }
}
}