using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public GameObject orderSmallPrefab;
    public GameObject orderMediumPrefab;
    public GameObject orderLargePrefab;
    public Transform orderGroup;

    public GameObject horizontalLayoutGroupPrefab;
    private GameObject currentHorizontalLayoutGroup;
    private int maxOrdersPerRow = 3;

    public Vegetable[] vegetables;

    [SerializeField]
    private int currentOrderCount = 0;

    private int vegetableTypesCount;

    // Start is called before the first frame update
    void Start()
    {
        vegetableTypesCount = System.Enum.GetValues(typeof(VegetableType)).Length;
        InvokeRepeating("GenerateRandomOrder", 2.0f, 10.0f);

        CreateNewOrderRow();
    }

    void CreateNewOrderRow()
    {
        currentHorizontalLayoutGroup = Instantiate(horizontalLayoutGroupPrefab, orderGroup);
        currentOrderCount = 0;
    }

    void GenerateRandomOrder()
    {
        if (currentOrderCount >= maxOrdersPerRow) {
            CreateNewOrderRow();
        }
        
        GameObject orderPrefab = null;
        int numberOfVegetables = 0;

        switch (Random.Range(0, 3)) {
            case 0:
                orderPrefab = orderSmallPrefab;
                numberOfVegetables = 1;
                break;
            case 1:
                orderPrefab = orderMediumPrefab;
                numberOfVegetables = 2;
                break;
            case 2:
                orderPrefab = orderLargePrefab;
                numberOfVegetables = 3;
                break;
        }

        if (orderPrefab != null) {
            var orderObject = Instantiate(orderPrefab, currentHorizontalLayoutGroup.transform);
            var orderScript = orderObject.GetComponent<IOrder>();
            orderScript.InitializeOrder(GetRandomVegetables(numberOfVegetables));
            currentOrderCount++;
        } else {
            Debug.LogError("Order prefab is null");
        }
    }

    private Vegetable[] GetRandomVegetables(int count)
    {
        var randomType = (VegetableType)Random.Range(0, System.Enum.GetValues(typeof(VegetableType)).Length);
        Vegetable[] randomizedVegetables = new Vegetable[count];

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, vegetables.Length);
            randomizedVegetables[i] = vegetables[randomIndex];
        }

        return randomizedVegetables;
    }
}
