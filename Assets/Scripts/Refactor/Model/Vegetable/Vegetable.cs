using System.Collections;
using UnityEngine;

public enum VegetableType
{
    Eggplant,
    Pumpkin,
    Tomato
}

public class Vegetable : MonoBehaviour
{

    public enum StateEnum
    {
        Unripped,
        Ripped,
        Rotten
    }

    public VegetableType vegetableType;
    public StateEnum GrowthState = StateEnum.Unripped;
    public GameObject[] growthStages;
    public bool isDelivered = false;
    public Sprite vegetableSprite;

    private void Start()
    {
        StartCoroutine(GrowthCycle());
    }

    private IEnumerator GrowthCycle()
    {
        for (int i = 0; i < growthStages.Length; i++)
        {

            foreach (GameObject stage in growthStages)
            {
                stage.SetActive(false);
            }
            growthStages[i].SetActive(true);

            yield return new WaitForSeconds(5f);

            if (i == growthStages.Length - 2)
            {
                GrowthState = StateEnum.Rotten;
            }
            else if (i == growthStages.Length - 3)
            {
                GrowthState = StateEnum.Ripped;
            }
            else
            {
                GrowthState = StateEnum.Unripped;
            }
        }

    }

}

