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
                PickUpCabbage();
            } else {
                DropCabbage();
            }
        }
    }

    private void PickUpCabbage()
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

    private void DropCabbage()
    {
        if (pickedCabbage != null) {
            pickedCabbage.transform.SetParent(null); // 親の関連を解除
            pickedCabbage = null;
        }
    }
}