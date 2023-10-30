using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlantCollector : MonoBehaviour
{
    public Text cabbageText;
    private int cabbageCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plant") {
            if (Vector3.Distance(transform.position, collision.transform.position) < 1f) {
                cabbageCount++;
                UpdateCabbageText();
            }
        }
    }

    private void UpdateCabbageText()
    {
        if (cabbageText != null) {
            cabbageText.text = "Cabbage: " + cabbageCount;
        }
    }
}
