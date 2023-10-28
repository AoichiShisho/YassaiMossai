using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemSelector : MonoBehaviour
{
    public Sprite[] itemImages;
    private int selectedItemIndex = 0;
    public Image selectedItemImage;

    public int SelectedItemIndex
    {
        get { return selectedItemIndex; }
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedItemImage.sprite = itemImages[selectedItemIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            selectedItemIndex = (selectedItemIndex - 1 + itemImages.Length) % itemImages.Length;
            Debug.Log("selected item number is: " + selectedItemIndex);
            UpdateSelectedItem();
        } if (Input.GetKeyDown(KeyCode.E)) {
            selectedItemIndex = (selectedItemIndex + 1) % itemImages.Length;
            Debug.Log("selected item number is: " + selectedItemIndex);
            UpdateSelectedItem();
        }
    }

    void UpdateSelectedItem() 
    {
        if (selectedItemImage != null && itemImages.Length > selectedItemIndex) {
            selectedItemImage.sprite = itemImages[selectedItemIndex];
        }
    }

    
}
