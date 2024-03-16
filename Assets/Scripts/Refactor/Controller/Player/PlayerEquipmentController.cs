using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{
    public Equipment[] equipments;
    private NewPlayerState newPlayerState;
    private int currentEquipmentIndex = 0;
    private int fullBasket = 2;
    private int emptyBasket = 1;

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

            //すでにfullバスケットオブジェクトの際は新しくインスタンス化されないような処理
            if (newPlayerState.CurrentEquipment.isFull && currentEquipmentIndex == fullBasket)
            {
                currentEquipmentIndex = fullBasket;
            }
            else if (newPlayerState.CurrentEquipment.isFull)
            {
                currentEquipmentIndex = fullBasket;
                EquipCurrentItem();
            }
            else if (!newPlayerState.CurrentEquipment.isFull && currentEquipmentIndex == fullBasket)
            {
                currentEquipmentIndex = emptyBasket;
                EquipCurrentItem();
            }
        }
    }

    private void SwitchEquipment(int direction)
    {
        if (currentEquipmentIndex == fullBasket)
        {
            return;
        }
        int newIndex = currentEquipmentIndex + direction;

        if (newIndex >= equipments.Length) newIndex = 0;
        else if (newIndex < 0) newIndex = equipments.Length - 1;

        if (!newPlayerState.CurrentEquipment.isFull && newIndex == fullBasket)
        {
            newIndex += direction;
            if (newIndex >= equipments.Length) newIndex = 0;
            else if (newIndex < 0) newIndex = equipments.Length - 1;
        }

        currentEquipmentIndex = newIndex;
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


