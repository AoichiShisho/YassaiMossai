using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlant : MonoBehaviour
{
    public GameObject cabbagePrefab;
    private bool isOnPlowedDirt = false;
    private Transform currentDirtTransform;

    private bool isPlanting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlowedDirt && Input.GetKeyDown(KeyCode.R) && !isPlanting) {
            isPlanting = true;
            PlantCabbage();
        } else if (isOnPlowedDirt && Input.GetKeyDown(KeyCode.R) && isPlanting) {
            // あとで変更、植えられなかった時のUIを考える
            Debug.Log("Can't plant, dirt is occupied.");
        }
    }

    void PlantCabbage() {
        Vector3 positionToPlant = currentDirtTransform.position + new Vector3(0, 0.2f, 0);
        Instantiate(cabbagePrefab, positionToPlant, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Plowed")) {
            isOnPlowedDirt = true;
            currentDirtTransform = other.transform;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Plowed")) {
            isOnPlowedDirt = false;
            currentDirtTransform = null;
        }
    }
}
