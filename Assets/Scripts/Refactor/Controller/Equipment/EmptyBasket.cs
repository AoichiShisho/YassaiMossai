using UnityEngine;

public class EmptyBasket : Equipment
{
    private void Awake()
    {
        isFull = false;
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

        if (Physics.Raycast(rayStart, -transform.forward, out hit, maxDistance, plantLayer))
        {
            Transform parentTransform = hit.collider.transform.parent;

            if (parentTransform != null)
            {
                Vegetable vegetable = parentTransform.GetComponent<Vegetable>();
                if (vegetable != null && (vegetable.GrowthState == Vegetable.StateEnum.Ripped || vegetable.GrowthState == Vegetable.StateEnum.Rotten))
                {
                    Destroy(parentTransform.gameObject);
                    isFull = true;
                    Debug.Log($"Harvested {vegetable.vegetableType} in state {vegetable.GrowthState}.");
                }
                else
                {
                    Debug.Log("Hit object's parent is not a vegetable.");
                    Debug.Log("Hit Parent Object: " + parentTransform.gameObject.name);
                }
            }
        }
        else
        {
            Debug.Log("No parent detected for the hit object.");
        }
    }
}

