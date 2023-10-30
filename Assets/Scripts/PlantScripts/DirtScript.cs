using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScript : MonoBehaviour
{
    public GameObject UnplowedDirtPrefab;
    public GameObject PlowedDirtPrefab;
    private bool isNearPlayer = false;
    public PlayerItemSelector playerItemSelector;

    private void Update()
    {
        if(isNearPlayer && Input.GetKeyDown(KeyCode.Space) 
            && playerItemSelector.SelectedItemIndex == 1)
        {
            PlowDirt();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
            // Debug.Log("Player is near");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            // Debug.Log("Player has left dirt");
        }
    }

    void PlowDirt()
    {
        Debug.Log("Plowing dirt");
        PlowedDirtPrefab.SetActive(true);
        UnplowedDirtPrefab.SetActive(false);
        
        // Instantiate(PlowedDirtPrefab, transform.position, Quaternion.identity);
    }
}

