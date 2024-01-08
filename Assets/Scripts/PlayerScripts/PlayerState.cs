using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PlayerItemState currentState;

    private void Start()
    {
        currentState = PlayerItemState.NotHolding;
    }

    public enum PlayerItemState
    {
        Holding,
        LimitedHolding,
        NotHolding
    }

    public PlayerItemState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
}
