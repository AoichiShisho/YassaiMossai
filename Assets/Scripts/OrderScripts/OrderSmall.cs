using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSmall : OrderBase
{
    protected override float timeLimit => 20f;

    public Image vegetableImage;
    public Sprite tomatoSprite;
    public Sprite cabbageSprite;

    public override void InitializeOrder(List<VegetableType> vegetables) 
    {
        this.vegetables = RandomizeVegetables(1);
        DisplayOrder();
    }

    public override void DisplayOrder()
    {
        SetVegetableImage(vegetables[0], vegetableImage, tomatoSprite, cabbageSprite);
    }
}
