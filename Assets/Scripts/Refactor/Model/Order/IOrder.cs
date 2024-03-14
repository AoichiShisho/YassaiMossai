using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrder
{
    void InitializeOrder(Vegetable[] vegetables);
    void DisplayOrder();
}
