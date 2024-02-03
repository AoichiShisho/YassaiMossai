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
                // orderPrefab = orderSmallPrefab;
                numberOfVegetables = 2;
                break;
            case 2:
                orderPrefab = orderLargePrefab;
                // orderPrefab = orderSmallPrefab;
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

    private List<VegetableType> GetRandomVegetables(int count)
    {
        List<VegetableType> vegetables = new List<VegetableType>();

        for (int i = 0; i < count; i++)
        {
            VegetableType randomVegetable = (VegetableType)Random.Range(0, vegetableTypesCount);
            vegetables.Add(randomVegetable);
        }

        return vegetables;
    }
}
