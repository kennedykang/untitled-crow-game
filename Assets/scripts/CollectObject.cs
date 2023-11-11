using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CollectObject : MonoBehaviour
{

    public TextMeshProUGUI countText;
    private int count;
    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
   }
    void Start(){
    count = 0;
    SetCountText();
    }
    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            count = count + 1;
            SetCountText();
        }
    }*/
}
