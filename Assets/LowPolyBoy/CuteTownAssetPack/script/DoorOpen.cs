using UnityEngine;
using System.Collections;

namespace CuteTownAssetPack
{
    public class DoorOpen : MonoBehaviour
    {
        public GameObject door; // Reference to the door object
        public float rotationAngle = -50f; // Rotation angle in degrees
        public float rotationSpeed = 2.5f; // Speed of door rotation
        public float closeDelay = 2f; // Delay before closing the door

        private bool isOpen = false; // Flag to track if the door is open

        private IEnumerator OpenDoorSmoothly()
        {
            Quaternion startRotation = door.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f) * startRotation;
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * rotationSpeed;
                door.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
                yield return null;
            }

            isOpen = true; // Set the flag to indicate the door is open
            //Debug.Log("Door opened.");

            // Start the coroutine to close the door after the specified delay
            StartCoroutine(CloseDoorAfterDelay());
        }

        private IEnumerator CloseDoorAfterDelay()
        {
            yield return new WaitForSeconds(closeDelay);

            // Calculate the original rotation of the door
            Quaternion originalRotation = Quaternion.Euler(0f, 0f, 0f);

            // Smoothly rotate the door back to its original rotation
            Quaternion currentRotation = door.transform.rotation;
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * rotationSpeed;
                door.transform.rotation = Quaternion.Slerp(currentRotation, originalRotation, elapsedTime);
                yield return null;
            }

            isOpen = false; // Set the flag to indicate the door is closed
            //Debug.Log("Door closed.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isOpen)
            {
                StartCoroutine(OpenDoorSmoothly());
            }
        }
    }
}
