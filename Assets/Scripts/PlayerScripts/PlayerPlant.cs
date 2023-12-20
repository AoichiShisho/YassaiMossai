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
    private DirtScript dirtScript;
    private DirtScript.DirtState dirtState;
    private float interactionRange = 1.0f;
    [SerializeField]
    private LayerMask dirtLayer;

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
        if (isOnPlowedDirt && Input.GetKeyDown(KeyCode.R))
        {
            Collider[] hitCollidersOfDirt = Physics.OverlapSphere(transform.position, interactionRange, dirtLayer);
            foreach (var hitColliderOfDirt in hitCollidersOfDirt)
            {
                DirtScript dirtScript = hitColliderOfDirt.GetComponent<DirtScript>();
                if (dirtScript != null && dirtScript.CompareTag("Dirt"))
                {
                    Debug.Log(dirtScript.CurrentDirtState);
                    if (dirtScript.CurrentDirtState == DirtScript.DirtState.Empty)
                    {
                        PlantVegetable();
                    }
                    else if (dirtScript.CurrentDirtState == DirtScript.DirtState.Planted)
                    {
                        // あとで変更、植えられなかった時のUIを考える
                        Debug.Log("Can't plant, dirt is occupied.");
                    }
                    dirtScript.CurrentDirtState = DirtScript.DirtState.Planted;
                    break;
                }
            }
        }
    }

    void PlantVegetable() {
        Vector3 positionToPlant = currentDirtTransform.position + new Vector3(0, 0.1f, 0);
        Instantiate(vegetables[vegetableSelector.SelectedVegetableIndex], positionToPlant, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other) {
        if (HasPlowedChild(other.transform)) {
            isOnPlowedDirt = true;
            currentDirtTransform = other.transform;
        }
    }

    void OnTriggerExit(Collider other) {
        isOnPlowedDirt = false;
        isPlanting = false;
        currentDirtTransform = null;
    }

    private bool HasPlowedChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeInHierarchy && child.CompareTag("Plowed"))
            {
                return true;
            }
        }
        return false;
    }
}
