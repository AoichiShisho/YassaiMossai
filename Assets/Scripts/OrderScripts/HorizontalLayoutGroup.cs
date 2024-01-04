using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLayoutGroup : MonoBehaviour
{
    public void ChildDestroyed()
    {
        if (transform.childCount <= 1) {
            Destroy(gameObject);
        }
    }
}
