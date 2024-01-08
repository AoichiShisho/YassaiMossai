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
    private float interactionRange = 1.0f;
    public PlantGrowth plantGrowth;

    [SerializeField]
    private LayerMask interactionLayer;
    [SerializeField]
    private LayerMask dirtLayer;

    private GameObject pickedVeggie = null;
    [SerializeField] private PlayerState playerState;


    void Start()
    {

        veggieCounts = new Dictionary<string, int>
        {
            { "RipeCabbage", 0 },
            { "RipeTomato", 0 },
        };

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (pickedVeggie == null && playerState.CurrentState == PlayerState.PlayerItemState.NotHolding)
            {
                PickupVeggie();
            }
            else
            {
                DeliverVeggie();
                DisposeVeggie();
            }
        }
    }

    private void PickupVeggie() 
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);
        Collider[] hitCollidersOfDirt = Physics.OverlapSphere(transform.position, interactionRange, dirtLayer);

        foreach (var hitCollider in hitColliders) {
            string tag = hitCollider.gameObject.tag;
            if (veggieCounts.ContainsKey(tag)) {
                Transform parentTransform = hitCollider.transform.parent;
                if (parentTransform != null)
                {
                    var plantGrowthScript = parentTransform.GetComponent<PlantGrowth>();
                    if (plantGrowthScript)
                    {
                        plantGrowthScript.StopGrowth();
                        plantGrowthScript.enabled = false;
                    }
                }
                pickedVeggie = hitCollider.gameObject;
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }
                pickedVeggie.transform.SetParent(transform);
                pickedVeggie.transform.localPosition = new Vector3(0, 0.4f, 0.3f);
                veggieCounts[tag]++;
                UpdateVeggieTexts();
                playerState.CurrentState = PlayerState.PlayerItemState.Holding;
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
                    deliveredText.text = deliveredAmount.ToString();
                }
            }

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.SetScore(deliveredAmount);
                Debug.Log("Score updated: " + ScoreManager.Instance.Score);
            }
            else
            {
                Debug.LogError("ScoreManager instance is null.");
            }

            pickedVeggie.transform.SetParent(null);
            playerState.CurrentState = PlayerState.PlayerItemState.NotHolding;
            Destroy(pickedVeggie.gameObject);

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

            pickedVeggie.transform.SetParent(null);
            playerState.CurrentState = PlayerState.PlayerItemState.NotHolding;
            Destroy(pickedVeggie.gameObject);

            UpdateVeggieTexts();
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
