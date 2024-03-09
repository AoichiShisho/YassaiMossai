using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{
    public Equipment[] equipments;
    private NewPlayerState newPlayerState;
    private int currentEquipmentIndex = 0;

    private void Start()
    {
        newPlayerState = GetComponent<NewPlayerState>();
        EquipCurrentItem();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchEquipment(1);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchEquipment(-1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            newPlayerState.CurrentEquipment?.Use();
        }
    }

    private void SwitchEquipment(int direction)
    {
        currentEquipmentIndex += direction;
        if (currentEquipmentIndex >= equipments.Length) currentEquipmentIndex = 0;
        else if (currentEquipmentIndex < 0) currentEquipmentIndex = equipments.Length - 1;

        EquipCurrentItem();
    }

    private void EquipCurrentItem()
    {
        if (equipments.Length > 0)
        {
            newPlayerState.Equip(equipments[currentEquipmentIndex]);
        }
    }
}


