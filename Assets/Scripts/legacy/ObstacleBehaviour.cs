using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    gameBehaviour gameBeh;

    private void Start()
    {
        gameBeh = FindObjectOfType<gameBehaviour>().GetComponent<gameBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameBeh.wallHit();
        }
    }
}
