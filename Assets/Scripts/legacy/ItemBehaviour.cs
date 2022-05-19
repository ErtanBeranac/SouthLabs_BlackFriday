using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    gameBehaviour gameBeh;

    private void Start()
    {
        gameBeh = FindObjectOfType<gameBehaviour>().GetComponent<gameBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("udario");
            if (other.gameObject.tag == "Player")
            {
                gameBeh.collectItem();
                Destroy(this.gameObject);
            }
    }
}
