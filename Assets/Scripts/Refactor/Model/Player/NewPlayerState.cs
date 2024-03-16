using System.Collections.Generic;
using UnityEngine;

public class NewPlayerState : MonoBehaviour
{
    public Equipment CurrentEquipment { get; private set; }
    public List<VegetableData> HarvestedVegetablesData { get; private set; } = new List<VegetableData>();

    //[SerializeField]
    //private List<VegetableData> harvestedVegetablesData = new List<VegetableData>();
    //public List<VegetableData> HarvestedVegetablesData => harvestedVegetablesData;

    public void Equip(Equipment newEquipment)
    {
        if (CurrentEquipment != null)
        {
            Destroy(CurrentEquipment.gameObject);
        }

        Vector3 spawnPosition = transform.TransformPoint(new Vector3(0, 0.5f, 0.3f));

        CurrentEquipment = Instantiate(newEquipment, spawnPosition, Quaternion.identity, transform);
    }

    public void AddHarvestedVegetableData(VegetableData data)
    {
        HarvestedVegetablesData.Add(data);
    }
}

