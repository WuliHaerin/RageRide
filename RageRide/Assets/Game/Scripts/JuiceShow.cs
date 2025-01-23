using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JuiceShow : MonoBehaviour
{
    public TMP_Text textT;
    // Start is called before the first frame update
    void Start()
    {
        textT = GetComponent<TMP_Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        textT.text = "ÏÖÓÐ½ð±Ò£º" + GameController.instance.juice;
    }
}
