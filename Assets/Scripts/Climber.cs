using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public GameObject Canvas;
    public List<GameObject> Components;

    private int _idx = 0;

    void OnTriggerEnter(Collision collision)
    {
        Debug.Log("Collider");
        // On collide with player
        if (collision.gameObject.tag == "Player")
        {
            // Show canvas
            Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
    }

    public void Unlock()
    {
        if (_idx < Components.Count)
        {
            Components[_idx].SetActive(true);
            _idx += 1;
        }
        Canvas.SetActive(false);
    }
}
