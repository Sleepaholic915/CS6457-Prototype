using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public GameObject Canvas;
    public List<GameObject> Components;

    private int _idx = 0;

    void OnCollisionEnter(Collision collision)
    {
        // On collide with player
        if (collision.gameObject.tag == "Player")
        {
            // Show canvas
            Canvas.SetActive(true);
        }
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
