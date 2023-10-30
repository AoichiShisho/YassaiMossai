using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public GameObject[] growthStages;
    private int currentStage = 0;
    public float growthTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=1; i<growthStages.Length; i++) {
            growthStages[i].SetActive(false);
        }
        StartCoroutine(GrowthCycle());
    }

    IEnumerator GrowthCycle()
    {
        while (currentStage < growthStages.Length - 1)
        {
            yield return new WaitForSeconds(growthTime);

            growthStages[currentStage].SetActive(false);
            currentStage++;
            growthStages[currentStage].SetActive(true);
        }
    }
}
