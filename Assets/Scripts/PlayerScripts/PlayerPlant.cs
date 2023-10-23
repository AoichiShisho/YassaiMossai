using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlant : MonoBehaviour
{
    public GameObject cabbagePrefab;
    private bool isOnPlowedDirt = false;
    private Transform currentDirtTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlowedDirt && Input.GetKeyDown(KeyCode.R)) {
            PlantCabbage();
        }
    }

    void PlantCabbage() {
        Vector3 positionToPlant = currentDirtTransform.position + new Vector3(0, 1, 0);
        Instantiate(cabbagePrefab, positionToPlant, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plowed")) {
            isOnPlowedDirt = true;
            currentDirtTransform = other.transform;
            Debug.Log("Detected Plowed Dirt.");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Plowed")) {
            isOnPlowedDirt = false;
            currentDirtTransform = null;
        }
    }
}
