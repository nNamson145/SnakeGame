using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    /*void Update()
    {
        
    }*/


    public TextMeshProUGUI textComponent;

    public void SetTextBox(int newText)
    {
        if (textComponent != null)
        {
            textComponent.text = "Score: " + newText;
        }
    }
}
