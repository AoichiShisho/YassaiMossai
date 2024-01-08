using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderLarge : OrderBase
{
    protected override float timeLimit => 40f;

    public List<Image> vegetableImages;
    public Sprite tomatoSprite;
    public Sprite cabbageSprite;

    public override void InitializeOrder(List<VegetableType> vegetables)
    {
        this.vegetables = RandomizeVegetables(3);
        DisplayOrder();
    }

    public override void DisplayOrder()
    {
        for (int i = 0; i < vegetables.Count; i++) {
            SetVegetableImage(vegetables[i], vegetableImages[i], tomatoSprite, cabbageSprite);
        }
    }
}