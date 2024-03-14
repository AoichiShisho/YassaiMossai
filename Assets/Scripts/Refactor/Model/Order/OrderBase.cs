using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OrderBase : MonoBehaviour, IOrder
{
    public Vegetable[] vegetables;
    public float timer;
    protected abstract float timeLimit { get; }
    protected int vegetableIndex = 0;

    public Image timeBar;

    public abstract void InitializeOrder(Vegetable[] vegetables);

    public abstract void DisplayOrder();

    public abstract void DeliverVegetable(Vegetable[] deliveredvegetables);

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

    protected void SetVegetableImage(Vegetable vegetable, Image vegetableImage)
    {
        vegetableImage.sprite = vegetable.vegetableSprite;
    }

    protected Vegetable[] RandomizeVegetables(int count)
    {
        var randomType = (VegetableType)Random.Range(0, System.Enum.GetValues(typeof(VegetableType)).Length);
        Vegetable[] randomizedVegetables = new Vegetable[count];

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, vegetables.Length);
            randomizedVegetables[i] = vegetables[randomIndex];
        }

        return randomizedVegetables;
    }

    protected void CheckDeliveredVegetables(Vegetable[] deliveredvegetables)
    {
        //ここにユーザーの届けた野菜とOrderの野菜を比較
    }

}
