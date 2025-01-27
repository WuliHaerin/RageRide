﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[System.Serializable]
public class Items
{
    public string description;
    public string descriptionAfterUnlock;
    public Sprite image;
    public bool unLock;
    //public Button.ButtonClickedEvent thingToDo;
    public int index;
    public int cost;
}

public class CreateScrollList : MonoBehaviour {

    public static CreateScrollList instance;

    public Items[] shipsCollection;

    public List<GameObject> ObjList = new List<GameObject>();

    public GameObject refButton;

    public Transform shipPanel;

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start ()
    {
        for (int i = 0; i < 4; i++)
        {
            shipsCollection[i].unLock = GameController.instance.players[i];
        }

        foreach (Items i in shipsCollection)
        {

            GameObject btn = Instantiate(refButton);//here we get ref to the instanciated button

            SampleButton samBtn = btn.GetComponent<SampleButton>();//ref to the script of button

            samBtn.shipIndex = i.index;
            samBtn.cost = i.cost;

            if (i.unLock == true)
            {
                samBtn.buttonCost.text = i.descriptionAfterUnlock;
            }
            else
            {
                samBtn.buttonCost.text = i.description;
            }

            samBtn.buttonIcon.sprite = i.image;

            //samBtn.button.onClick = i.thingToDo;

            btn.transform.SetParent(shipPanel);

            btn.transform.localScale = new Vector3(1f, 1f, 1f);
            ObjList.Add(btn);

        }
    }

	void Update ()
    {
        for (int i = 0; i < 4; i++)
        {
            shipsCollection[i].unLock = GameController.instance.players[i];
        }
        for(int i=0;i<ObjList.Count;i++)
        {
            SampleButton samBtn = ObjList[i].GetComponent<SampleButton>();//ref to the script of button

            samBtn.shipIndex = shipsCollection[i].index;
            samBtn.cost = shipsCollection[i].cost;

            if (shipsCollection[i].unLock == true)
            {
                samBtn.buttonCost.text = shipsCollection[i].descriptionAfterUnlock;
            }
            else
            {
                samBtn.buttonCost.text = shipsCollection[i].description;
            }

            samBtn.buttonIcon.sprite = shipsCollection[i].image;

            //samBtn.button.onClick = i.thingToDo;

            ObjList[i].transform.SetParent(shipPanel);

            ObjList[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
