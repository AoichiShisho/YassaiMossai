using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSmall : MonoBehaviour, IOrder
{
    private List<VegetableType> vegetables;
    public Image vegetableImage;
    public Sprite tomatoSprite;
    public Sprite cabbageSprite;

    public void InitializeOrder(List<VegetableType> vegetables) {
        this.vegetables = vegetables;
        DisplayOrder();
    }

    public void DisplayOrder()
    {
        if (vegetables.Count > 0) {
            SetVegetableImage(vegetables[0]);
        }
    }

    private void SetVegetableImage(VegetableType vegetable)
    {
        switch (vegetable) {
            case VegetableType.Tomato:
                vegetableImage.sprite = tomatoSprite;
                break;
            case VegetableType.Cabbage:
                vegetableImage.sprite = cabbageSprite;
                break;
        }
    }
}
