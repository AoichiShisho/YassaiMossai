using UnityEngine;

public class PlayerPlantController : MonoBehaviour
{
    public Vegetable[] vegetables;
    private int currentVeggieIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentVeggieIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentVeggieIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlantVeggie();
        }
    }

    void PlantVeggie()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            Dirt dirt = hit.collider.GetComponent<Dirt>();
            if (dirt != null && dirt.currentState == Dirt.State.Tilled)
            {
                Instantiate(vegetables[currentVeggieIndex], hit.point, Quaternion.identity);
            }
        }
    }
}

