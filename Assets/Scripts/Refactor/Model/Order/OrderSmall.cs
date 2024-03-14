using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSmall : OrderBase
{
    protected override float timeLimit => 30f;

    public Image vegetableImage;
    private int orderNumber = 1;

    public override void InitializeOrder(Vegetable[] vegetables) 
    {
        this.vegetables = RandomizeVegetables(orderNumber);
        DisplayOrder();
    }

    public override void DisplayOrder()
    {
        SetVegetableImage(vegetables[0], vegetableImage);
    }

    public override void DeliverVegetable(Vegetable[] deliveredvegetable)
    {
        //FullBasketの中に入っているvegetable[]とOrderのvegetable[]が一致するかチェック
        //順番はどうでもよくて中に入っている野菜が一致したらOK
    }
}
