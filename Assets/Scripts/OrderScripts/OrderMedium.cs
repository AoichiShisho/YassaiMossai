using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMedium : OrderBase
{
    protected override float timeLimit => 40f;

    public List<Image> vegetableImages;
    public Sprite tomatoSprite;
    public Sprite cabbageSprite;

    public override void InitializeOrder(List<VegetableType> vegetables)
    {
        this.vegetables = RandomizeVegetables(2);
        InitializeVegetableDeliveryStatus(this.vegetables);
        DisplayOrder();
    }

    public override void DisplayOrder()
    {
        for (int i = 0; i < vegetables.Count; i++) {
            SetVegetableImage(vegetables[i], vegetableImages[i], tomatoSprite, cabbageSprite);
        }
    }

    public override void DeliverVegetable(VegetableType vegetable)
    {
        for (int i = 0; i < vegetables.Count; i++)
        {
            print("Deliver確認Med");
            if (vegetables[i] == vegetable && !vegetableDeliveryStatus[(vegetable, i)])
            {
                var key = (vegetable, i);
                vegetableDeliveryStatus[key] = true;

                if (vegetableImages[i] != null)
                {
                    vegetableImages[i].gameObject.SetActive(false);
                }

                CheckIfOrderIsComplete();
                break;
            }
        }
    }

}
