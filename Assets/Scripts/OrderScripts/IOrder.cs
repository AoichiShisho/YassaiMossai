using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrder
{
    void InitializeOrder(List<VegetableType> vegetables);
    void DisplayOrder();
}
