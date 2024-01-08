using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScript : MonoBehaviour
{
    public GameObject UnplowedDirtPrefab;
    public GameObject PlowedDirtPrefab;
    private bool isNearPlayer = false;
    public PlayerItemSelector playerItemSelector;
    private DirtState currentDirtState = DirtState.Empty;
    public PlayerPlant playerPlant;

    public enum DirtState
    {
        Empty,
        Planted
    }

    public DirtState CurrentDirtState
    {
        get { return currentDirtState; }
        set { currentDirtState = value; }
    }

    private void Update()
    {
        if(isNearPlayer && Input.GetKeyDown(KeyCode.Space) 
            && playerItemSelector.SelectedItemIndex == 0)
        {
            PlowDirt();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    void PlowDirt()
    {
        Debug.Log("Plowing dirt");
        PlowedDirtPrefab.SetActive(true);
        UnplowedDirtPrefab.SetActive(false);
    }

    public void DamageDirt()
    {
        PlowedDirtPrefab.SetActive(false);
        UnplowedDirtPrefab.SetActive(true);
        currentDirtState = DirtState.Empty;
    }

}

