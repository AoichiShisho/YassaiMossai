using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderLarge : MonoBehaviour, IOrder
{
    private List<VegetableType> vegetables;
    public List<Image> vegetableImages; // Imageコンポーネントのリスト
    public Sprite tomatoSprite;
    public Sprite cabbageSprite;

    public void InitializeOrder(List<VegetableType> vegetables)
    {
        this.vegetables = vegetables;
        DisplayOrder();
    }

    public void DisplayOrder()
    {
        for (int i = 0; i < vegetables.Count; i++) {
            if (i < vegetableImages.Count) {
                SetVegetableImage(vegetables[i], vegetableImages[i]);
            }
        }
    }

    private void SetVegetableImage(VegetableType vegetable, Image vegetableImage)
    {
        // 野菜に応じた画像を設定
        switch (vegetable)
        {
            case VegetableType.Tomato:
                vegetableImage.sprite = tomatoSprite;
                break;
            case VegetableType.Cabbage:
                vegetableImage.sprite = cabbageSprite;
                break;
        }
    }
}
