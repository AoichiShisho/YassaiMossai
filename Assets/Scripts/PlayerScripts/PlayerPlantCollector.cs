using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlantCollector : MonoBehaviour
{
    public Text cabbageText;
    public Text tomatoText;
    
    private Dictionary<string, int> veggieCounts;
    private PlayerItemSelector itemSelector;
    private float interactionRange = 1.0f;

    [SerializeField]
    private LayerMask interactionLayer;

    // Start is called before the first frame update
    void Start()
    {
        itemSelector = GetComponent<PlayerItemSelector>();
        if (itemSelector == null) {
            Debug.LogError("PlayerPlantCollector: itemSelector is null");
        }

        veggieCounts = new Dictionary<string, int>
        {
            { "RipeCabbage", 0 },
            { "RipeTomato", 0 },
        };
    }

    // Update is called once per frame
    void Update()
    {
        PickupCabbage();
        UpdateBasketImage();
    }

    private void UpdateBasketImage() 
    {
        bool allZero = true;
        foreach (var count in veggieCounts.Values) {
            if (count != 0) {
                allZero = false;
                break;
            }
        }

        itemSelector.SetBasketImage(allZero ? 0 : 1);
    }

    private void PickupCabbage() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && itemSelector.SelectedItemIndex == 0) {

            Debug.Log("PlayerPlantCollector: space pressed");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);

            foreach (var hitCollider in hitColliders) {
                string tag = hitCollider.gameObject.tag;
                if (veggieCounts.ContainsKey(tag)) {
                    Debug.Log("Found Ripe Veggie");
                    Destroy(hitCollider.gameObject.transform.parent.gameObject);
                    veggieCounts[tag]++;
                    UpdateVeggieTexts();
                    break;
                }
            }
        }
    }

    private void UpdateVeggieTexts()
    {
        if (cabbageText != null) {
            cabbageText.text = veggieCounts["RipeCabbage"].ToString();
        }

        if (tomatoText != null) {
            tomatoText.text = veggieCounts["RipeTomato"].ToString();
        }
    }
}
