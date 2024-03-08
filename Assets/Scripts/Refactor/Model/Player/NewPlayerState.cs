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

        CurrentEquipment = Instantiate(newEquipment, transform);
    }
}

