using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private _PlayerState currentState = _PlayerState.NotHolding;

    public enum _PlayerState
    {
        Holding,
        NotHolding
    }

    public _PlayerState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
}
