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

    private float timer;
    private float timeLimit = 20f;

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
