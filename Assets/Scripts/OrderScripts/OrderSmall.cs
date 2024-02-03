using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSmall : OrderBase
{
    protected override float timeLimit => 30f;

    public Image vegetableImage;
    public Sprite tomatoSprite;
    public Sprite cabbageSprite;

    public override void InitializeOrder(List<VegetableType> vegetables) 
    {
        this.vegetables = RandomizeVegetables(1);
        InitializeVegetableDeliveryStatus(this.vegetables);
        DisplayOrder();
    }

    public override void DisplayOrder()
    {
        SetVegetableImage(vegetables[0], vegetableImage, tomatoSprite, cabbageSprite);
    }

    public override void DeliverVegetable(VegetableType vegetable)
    {
        var key = (vegetable, 0);
        if (vegetableDeliveryStatus.ContainsKey(key) && !vegetableDeliveryStatus[key])
        {
            print("Deliver確認Sm");
            vegetableDeliveryStatus[key] = true;

            if (vegetableImage != null)
            {
                vegetableImage.gameObject.SetActive(false);
            }

            CheckIfOrderIsComplete();
        }
    }
}
