using UnityEngine;

public class Hoe : Equipment
{
    public override void Use()
    {
        RaycastHit hit;
        float maxDistance = 1.5f;
        Vector3 rayStart = transform.position + Vector3.down * 0.4f;

        // Debug用のRayを表示
        //このrayを全てのrayの標準にしたい
        //utilとかに記述したい
        //Debug.DrawRay(rayStart, transform.forward, Color.red, 2f);

        // LayerMaskを使って特定のレイヤーに反応するようにする
        LayerMask dirtLayer = LayerMask.GetMask("DirtLayer");

        if (Physics.Raycast(rayStart, transform.forward, out hit, maxDistance, dirtLayer))
        {
            Dirt dirt = hit.collider.GetComponent<Dirt>();
            if (dirt != null)
            {
                dirt.Till();
                Debug.Log("Dirt has been tilled.");
            }
            else
            {
                Debug.Log("Hit object is not Dirt.");
                Debug.Log("Hit Object: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }

}



