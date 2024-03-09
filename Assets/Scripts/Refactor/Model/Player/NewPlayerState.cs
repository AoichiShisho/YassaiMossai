using UnityEngine;

public class NewPlayerState : MonoBehaviour
{
    public Equipment CurrentEquipment { get; private set; }

    public void Equip(Equipment newEquipment)
    {
        if (CurrentEquipment != null)
        {
            Destroy(CurrentEquipment.gameObject);
        }

        Vector3 spawnPosition = transform.TransformPoint(new Vector3(0, 0.5f, 0.3f));

        CurrentEquipment = Instantiate(newEquipment, spawnPosition, Quaternion.identity, transform);
    }
}

