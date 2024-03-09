using UnityEngine;

public class PlayerPlantController : MonoBehaviour
{
    public Vegetable[] vegetables;
    private int currentVegetableIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchVegetable(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchVegetable(-1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlantVegetable();
        }
    }

    private void SwitchVegetable(int direction)
    {
        currentVegetableIndex += direction;
        if (currentVegetableIndex >= vegetables.Length) currentVegetableIndex = 0;
        else if (currentVegetableIndex < 0) currentVegetableIndex = vegetables.Length - 1;
        Debug.Log("currentVegetableIndex: " + vegetables[currentVegetableIndex]);

    }

    private void PlantVegetable()
    {
        RaycastHit hit;
        float maxDistance = 1f;
        LayerMask dirtLayer = LayerMask.GetMask("DirtLayer");
        Debug.DrawRay(transform.position, transform.forward, Color.red, 2f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, dirtLayer))
        {
            Dirt dirt = hit.collider.GetComponent<Dirt>();
            if (dirt != null && dirt.currentState == Dirt.State.Tilled)
            {
                Vector3 plantPosition = dirt.transform.position + new Vector3(0, 0.1f, 0);
                Instantiate(vegetables[currentVegetableIndex], plantPosition, Quaternion.identity);
            }
        }
    }
}

