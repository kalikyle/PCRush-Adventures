using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchWires : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static MatchWires hoveritem;
    public Canvas canvas;
    public GameObject wirePrefab;
    public string itemName;
    public List<string> itemNamesList = new List<string>();

    private static GameObject line;

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        line = Instantiate(wirePrefab, transform.position, Quaternion.identity, transform.parent.parent);
        UpdateLine(eventData.position);
    }

    private void UpdateLine(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        line.transform.localScale = new Vector3(direction.magnitude / canvas.scaleFactor, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveritem = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Item hoveredItem = MatchLogic.instance.itemDictionary[itemName];

        if (!this.Equals(hoveritem) && itemName.Equals(hoveritem.itemName))
        {
            UpdateLine(hoveritem.transform.position);
            MatchLogic.AddPoint();
            Destroy(hoveritem);
            Destroy(this);
            //MatchLogic.AddPoint();
        }
        else
        {
            Destroy(line);
        }
    }
  
    public void Start()
    {
       
    }

}

