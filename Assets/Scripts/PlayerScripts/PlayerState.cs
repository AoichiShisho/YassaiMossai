using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PlayerItemState currentState = PlayerItemState.NotHolding;

    public enum PlayerItemState
    {
        Holding,
        NotHolding
    }

    public PlayerItemState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
}
