using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    public abstract void Use();

    public bool isFull { get; protected set; } = false;
}

