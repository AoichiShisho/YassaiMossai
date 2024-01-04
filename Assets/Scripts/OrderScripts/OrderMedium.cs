using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMedium : MonoBehaviour, IOrder
{
    private List<VegetableType> vegetables;
    public List<Image> vegetableImages;
    public Sprite tomatoSprite;
    public Sprite cabbageSprite;

    private float timer;
    private float timeLimit = 30f;

    public Image timeBar;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        UpdateTimeBar();
        if (timer <= 0) {
            DestroyOrder();
        }
    }

    private void UpdateTimeBar() 
    {
        if (timeBar != null) {
            timeBar.fillAmount = timer / timeLimit;
            timeBar.color = GetColorByTime(timer / timeLimit);
        }
    }

    private Color GetColorByTime(float timeRatio)
    {
        if (timeRatio > 0.65f) {
            return Color.Lerp(Color.yellow, Color.green, (timeRatio - 0.65f) * 2);
        } else {
            return Color.Lerp(Color.red, Color.yellow, timeRatio / 0.65f);
        }
    }

    private void DestroyOrder() 
    {
        if (transform.parent != null) {
            var parentGroup = transform.parent.GetComponent<HorizontalLayoutGroup>();
            if (parentGroup != null) {
                parentGroup.ChildDestroyed();
            }
        }
        Destroy(gameObject);
    }

    private void CheckAndDestroyParent()
    {
        if (transform.parent != null && transform.parent.childCount == 1) {
            Destroy(transform.parent.gameObject);
        }
    }

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
