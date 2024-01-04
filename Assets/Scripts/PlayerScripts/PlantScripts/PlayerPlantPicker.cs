using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantPicker : MonoBehaviour
{
    public GameObject pickedCabbage = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (pickedCabbage == null) {
                PickUpVeggie();
            } else {
                DropVeggie();
            }
        }
    }

    private void PickUpVeggie()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f)) {
            if (hit.collider.tag == "Plant") {
                pickedCabbage = hit.collider.gameObject;
                pickedCabbage.transform.SetParent(transform);
                pickedCabbage.transform.localPosition = new Vector3(0, 1, 2);
            }
        }
    }

    private void DropVeggie()
    {
        if (pickedCabbage != null) {
            pickedCabbage.transform.SetParent(null);
            pickedCabbage = null;
        }
    }
}