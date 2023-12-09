using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlant : MonoBehaviour
{
    public GameObject[] vegetables;
    private bool isOnPlowedDirt = false;
    private Transform currentDirtTransform;

    private bool isPlanting = false;

    private PlayerVegetableSelector vegetableSelector;

        void Start()
    {
        vegetableSelector = GetComponent<PlayerVegetableSelector>();
        if (vegetableSelector == null) {
            Debug.LogError("PlayerPlant: vegetableSelector is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlowedDirt && Input.GetKeyDown(KeyCode.R) && !isPlanting) {
            isPlanting = true;
            PlantVegetable();
        } else if (isOnPlowedDirt && Input.GetKeyDown(KeyCode.R) && isPlanting) {
            // あとで変更、植えられなかった時のUIを考える
            Debug.Log("Can't plant, dirt is occupied.");
        }
    }

    void PlantVegetable() {
        Vector3 positionToPlant = currentDirtTransform.position + new Vector3(0, 0.1f, 0);
        Instantiate(vegetables[vegetableSelector.SelectedVegetableIndex], positionToPlant, Quaternion.identity);
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
