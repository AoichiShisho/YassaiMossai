using System.Collections.Generic;
using UnityEngine;

public class FullBasket : Equipment
{
    public List<Vegetable> harvestedVegetables = new List<Vegetable>();

    private void Awake()
    {
        isFull = true;
    }

    public override void Use()
    {
        RaycastHit hit;
        float maxDistance = 1.5f;
        Vector3 rayStart = transform.position;

        // Debug用のRayを表示
        //このrayを全てのrayの標準にしたい
        //utilとかに記述したい
        Debug.DrawRay(rayStart, -transform.forward, Color.red, 2f);

        // LayerMaskを使って特定のレイヤーに反応するようにする
        LayerMask plantLayer = LayerMask.GetMask("PlantLayer");
        LayerMask storeLayer = LayerMask.GetMask("StoreLayer");

        if (Physics.Raycast(rayStart, -transform.forward, out hit, maxDistance, plantLayer))
        {
            Transform parentTransform = hit.collider.transform.parent;

            if (parentTransform != null)
            {
                Vegetable vegetable = parentTransform.GetComponent<Vegetable>();
                if (vegetable != null && (vegetable.GrowthState == Vegetable.StateEnum.Ripped || vegetable.GrowthState == Vegetable.StateEnum.Rotten))
                {
                    harvestedVegetables.Add(vegetable);
                    Debug.Log($"Harvested vegetables count: {harvestedVegetables.Count}");
                    Debug.Log(System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this));
                    Destroy(parentTransform.gameObject);
                    Debug.Log($"Harvested {vegetable.vegetableType} in state {vegetable.GrowthState}.");
                }
                else
                {
                    Debug.Log("Hit object's parent is not a vegetable.");
                    Debug.Log("Hit Parent Object: " + parentTransform.gameObject.name);
                }
            }
        }
        else if (Physics.Raycast(rayStart, -transform.forward, out hit, maxDistance, storeLayer)) {
            OrderBase[] allOrders = FindObjectsOfType<OrderBase>();

            foreach (OrderBase order in allOrders)
            {
                // デリバーメソッドをよぶ
            }

            isFull = false;
        }
        else
        {
            Debug.Log("No parent detected for the hit object.");
        }
    }
}
