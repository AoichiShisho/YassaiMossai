using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMedium : OrderBase
{
    protected override float timeLimit => 40f;

    public List<Image> vegetableImages;
    private int orderNumber = 2;

    public override void InitializeOrder(Vegetable[] vegetables)
    {
        this.vegetables = RandomizeVegetables(orderNumber);
        DisplayOrder();
    }

    public override void DisplayOrder()
    {
        for (int i = 0; i < orderNumber; i++)
        {
            SetVegetableImage(vegetables[i], vegetableImages[i]);
        }
    }

    public override void DeliverVegetable(Vegetable[] deliveredvegetable)
    {
        //FullBasketの中に入っているvegetable[]とOrderのvegetable[]が一致するかチェック
        //順番はどうでもよくて中に入っている野菜が一致したらOK
    }

}
