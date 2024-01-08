using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemSelector : MonoBehaviour
{
    public Sprite[] itemSprites;
    private int selectedItemIndex = 0;
    public Image selectedItemImage;

    public Sprite[] basketSprites;

    [SerializeField] private PlayerState playerState;

    public int SelectedItemIndex
    {
        get { return selectedItemIndex; }
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedItemImage.sprite = itemSprites[selectedItemIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.CurrentState == PlayerState.PlayerItemState.NotHolding)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedItemIndex = (selectedItemIndex - 1 + itemSprites.Length) % itemSprites.Length;
                Debug.Log("selected item number is: " + selectedItemIndex);
                UpdateSelectedItem();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                selectedItemIndex = (selectedItemIndex + 1) % itemSprites.Length;
                Debug.Log("selected item number is: " + selectedItemIndex);
                UpdateSelectedItem();
            }
        }
    }

    void UpdateSelectedItem() 
    {
        if (selectedItemImage != null && itemSprites.Length > selectedItemIndex) {
            selectedItemImage.sprite = itemSprites[selectedItemIndex];
        }
    }

    public void SetBasketImage(int index)
    {
        if (basketSprites.Length > index) {
            itemSprites[1] = basketSprites[index];
            selectedItemImage.sprite = itemSprites[selectedItemIndex];
        } else {
            Debug.LogError("SetBasketImage: Index out of range.");
        }
    }
    
}
