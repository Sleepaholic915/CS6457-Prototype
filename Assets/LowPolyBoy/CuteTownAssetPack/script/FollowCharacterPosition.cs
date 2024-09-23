using UnityEngine;

namespace CuteTownAssetPack
{
public class FollowCharacterPosition : MonoBehaviour
{
    public Transform character; // Reference to the character's transform
    public Vector3 offset; // Offset from the character's position

    void FixedUpdate()
    {
        if (character != null)
        {
            // Update the position of this GameObject to match the character's position with offset
            transform.position = character.position + offset;
        }
        else
        {
            Debug.LogWarning("Character transform not assigned to the FollowCharacterPosition script.");
        }
    }
}
}