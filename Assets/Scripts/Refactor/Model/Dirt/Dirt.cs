using UnityEngine;

public class Dirt : MonoBehaviour
{
    public enum State
    {
        Untilled,
        Tilled
    }

    public State currentState = State.Untilled;
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        //初期の色．utilで色定義
        renderer.material.color = new Color(0.31f, 0.19f, 0.08f, 1);
    }

    public void Till()
    {
        if (currentState == State.Untilled)
        {
            currentState = State.Tilled;
            Debug.Log("Dirt has been tilled.");

            // ここで耕された土のビジュアル変更を行う
            //ここもutilで色を定義しておきたい
            renderer.material.color = new Color(0.31f, 0.08f, 0.08f, 1);
        }
        else
        {
            Debug.Log("Dirt has already been tilled.");
        }
    }
}


