using UnityEngine;

public class Vegetable : MonoBehaviour
{
    public enum TypeEnum
    {
        Eggplant,
        Pumpkin,
        Tomato
    }

    public enum StateEnum
    {
        Unripped,
        Ripped,
        Rotten
    }

    public TypeEnum VeggieType;
    public StateEnum GrowthState;

    // このVeggieの初期設定や成長、状態変化などの処理をここに実装
}

