using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlantCollector : MonoBehaviour
{
    public Text cabbageText;
    public Text tomatoText;

    public Text deliveredText;
    private int deliveredAmount = 0;
    
    private Dictionary<string, int> veggieCounts;
    private PlayerItemSelector itemSelector;
    private float interactionRange = 1.0f;
    public PlantGrowth plantGrowth;

    [SerializeField]
    private LayerMask interactionLayer;
    [SerializeField]
    private LayerMask dirtLayer;


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
        if (Input.GetKeyDown(KeyCode.Space) && itemSelector.SelectedItemIndex == 0) {
            PickupVeggie();
            DeliverVeggie();
            DisposeVeggie();
        }
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

    private void PickupVeggie() 
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);
        Collider[] hitCollidersOfDirt = Physics.OverlapSphere(transform.position, interactionRange, dirtLayer);

        foreach (var hitCollider in hitColliders) {
            string tag = hitCollider.gameObject.tag;
            if (veggieCounts.ContainsKey(tag)) {
                Destroy(hitCollider.gameObject.transform.parent.gameObject);
                veggieCounts[tag]++;
                UpdateVeggieTexts();
                UpdateBasketImage();
                foreach (var hitColliderOfDirt in hitCollidersOfDirt)
                {
                    DirtScript dirtScript = hitColliderOfDirt.GetComponent<DirtScript>();
                    if (dirtScript != null && dirtScript.CompareTag("Dirt"))
                    {
                        Debug.Log(dirtScript.CurrentDirtState);
                        dirtScript.DamageDirt();
                        break;
                    }
                }
                break;
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
    
    private void DeliverVeggie() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        bool isNearDeliveryPoint = false;

        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.tag == "DeliveryPoint") {
                isNearDeliveryPoint = true;
                break;
            }
        }

        if (isNearDeliveryPoint && GetTotalVeggies() > 0) {
            deliveredAmount += GetTotalVeggies();

            var keys = new List<string>(veggieCounts.Keys);
            foreach (var key in keys) {
                veggieCounts[key] = 0;
            }

            UpdateVeggieTexts();

            if (deliveredText != null) {
                PlantGrowth.State state = plantGrowth.CurrentState;
                if (state != PlantGrowth.State.Death)
                {
                    deliveredText.text = "Delivered: " + deliveredAmount;
                }
            }

            UpdateBasketImage();
        }
    }

    private void DisposeVeggie() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        bool isNearDeliveryPoint = false;

        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.tag == "DisposePoint") {
                isNearDeliveryPoint = true;
                break;
            }
        }

        if (isNearDeliveryPoint && GetTotalVeggies() > 0) {
            deliveredAmount += GetTotalVeggies();

            var keys = new List<string>(veggieCounts.Keys);
            foreach (var key in keys) {
                veggieCounts[key] = 0;
            }

            UpdateVeggieTexts();
            UpdateBasketImage();
        }
    }

    private int GetTotalVeggies()
    {
        int total = 0;
        foreach (var count in veggieCounts.Values) {
            total += count;
        }

        return total;
    }
}
