using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVegetableSelector : MonoBehaviour
{
    public Sprite[] vegetableSprites;
    private int selectedVegetableIndex = 0;
    public Image selectedVegetableImage;

    public int SelectedVegetableIndex
    {
        get { return selectedVegetableIndex; }
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedVegetableImage.sprite = vegetableSprites[selectedVegetableIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedVegetableIndex = (selectedVegetableIndex - 1 + vegetableSprites.Length) % vegetableSprites.Length;
            Debug.Log("selected vegetable number is: " + selectedVegetableIndex);
            UpdateSelectedItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedVegetableIndex = (selectedVegetableIndex + 1) % vegetableSprites.Length;
            Debug.Log("selected vegetable number is: " + selectedVegetableIndex);
            UpdateSelectedItem();
        }
    }

    void UpdateSelectedItem()
    {
        if (selectedVegetableImage != null && vegetableSprites.Length > selectedVegetableIndex)
        {
            selectedVegetableImage.sprite = vegetableSprites[selectedVegetableIndex];
        }
    }

}

