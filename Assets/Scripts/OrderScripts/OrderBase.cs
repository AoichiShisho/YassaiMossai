using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OrderBase : MonoBehaviour, IOrder
{
    protected List<VegetableType> vegetables;
    protected Dictionary<(VegetableType, int), bool> vegetableDeliveryStatus;
    public float timer;
    protected abstract float timeLimit { get; }

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

    protected void InitializeVegetableDeliveryStatus(List<VegetableType> vegetables)
    {
        vegetableDeliveryStatus = new Dictionary<(VegetableType, int), bool>();
        for (int i = 0; i < vegetables.Count; i++)
        {
            var key = (vegetables[i], i); // タプルをキーとして使用
            vegetableDeliveryStatus[key] = false;
        }
    }

    protected void UpdateTimeBar() 
    {
        if (timeBar != null) {
            timeBar.fillAmount = timer / timeLimit;
            timeBar.color = GetColorByTime(timer / timeLimit);
        }
    }

    protected Color GetColorByTime(float timeRatio)
    {
        if (timeRatio > 0.65f) {
            return Color.Lerp(Color.yellow, Color.green, (timeRatio - 0.65f) * 2);
        } else {
            return Color.Lerp(Color.red, Color.yellow, timeRatio / 0.65f);
        }
    }

    protected void DestroyOrder() 
    {
        if (transform.parent != null) {
            var parentGroup = transform.parent.GetComponent<HorizontalLayoutGroup>();
            if (parentGroup != null) {
                parentGroup.ChildDestroyed();
            }
        }

        CheckAndDestroyParent();
        Destroy(gameObject);
    }

    protected void CheckAndDestroyParent()
    {
        if (transform.parent != null && transform.parent.childCount == 1) {
            Destroy(transform.parent.gameObject);
        }
    }

    public abstract void InitializeOrder(List<VegetableType> vegetables);

    public abstract void DisplayOrder();

    protected void SetVegetableImage(VegetableType vegetable, Image vegetableImage, Sprite tomatoSprite, Sprite cabbageSprite)
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

    protected List<VegetableType> RandomizeVegetables(int count)
    {
        var randomType = (VegetableType)Random.Range(0, System.Enum.GetValues(typeof(VegetableType)).Length);
        var vegetables = new List<VegetableType>();

        for (int i = 0; i < count; i++)
        {
            vegetables.Add(randomType);
        }

        return vegetables;
    }

    public abstract void DeliverVegetable(VegetableType vegetable);

    protected void CheckIfOrderIsComplete()
    {
        Debug.Log("Checking if order is complete...");

        bool allDelivered = true;

        foreach (var pair in vegetableDeliveryStatus)
        {
            Debug.Log($"Vegetable: {pair.Key}, Delivered: {pair.Value}");
            if (!pair.Value)
            {
                allDelivered = false;
                Debug.Log($"{pair.Key} is not delivered yet.");
            }
        }

        if (allDelivered)
        {
            CompleteOrder();
        }
        else
        {
            Debug.Log("Order is not complete yet.");
        }
    }

    protected void CompleteOrder()
    {
        Debug.Log("Order is complete!");
        gameObject.SetActive(false);
    }

}
