using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlantCollector : MonoBehaviour
{
    public Text cabbageText;
    private int cabbageCount = 0;
    private PlayerItemSelector itemSelector;
    private float interactionRange = 5.0f;

    [SerializeField]
    private LayerMask interactionLayer;

    // Start is called before the first frame update
    void Start()
    {
        itemSelector = GetComponent<PlayerItemSelector>();
        if (itemSelector == null) {
            Debug.LogError("PlayerPlantCollector: itemSelector is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PickupCabbage();
    }

    private void PickupCabbage() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && itemSelector.SelectedItemIndex == 0) {
            Debug.Log("PlayerPlantCollector: space pressed");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);
            foreach (var hitCollider in hitColliders) {
                if (hitCollider.gameObject.tag == "RipeCabbage") {
                    Debug.Log("Found Ripe Cabbage");
                    cabbageCount++;
                    UpdateCabbageText();
                    break;
                }
            }
        }
    }

    private void UpdateCabbageText()
    {
        if (cabbageText != null) {
            cabbageText.text = cabbageCount.ToString();
        }
    }
}
