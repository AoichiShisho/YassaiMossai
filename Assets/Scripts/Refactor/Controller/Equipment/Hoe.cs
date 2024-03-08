using UnityEngine;

public class Hoe : Equipment
{
    public override void Use()
    {
        RaycastHit hit;
        float maxDistance = 1f;
        Vector3 rayStart = transform.position - Vector3.right * 0.4f + Vector3.down * 0.2f;

        // Debug用のRayを表示
        //このrayを全てのrayの標準にしたい
        //utilとかに記述したい
        Debug.DrawRay(rayStart, -transform.right * maxDistance, Color.red, 2f);

        // LayerMaskを使って特定のレイヤーに反応するようにする
        LayerMask dirtLayer = LayerMask.GetMask("DirtLayer");

        if (Physics.Raycast(rayStart, -transform.right * maxDistance, out hit, maxDistance, dirtLayer))
        {
            Dirt dirt = hit.collider.GetComponent<Dirt>();
            if (dirt != null)
            {
                dirt.Till();
                Debug.Log("Dirt has been tilled.");
            } else
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



