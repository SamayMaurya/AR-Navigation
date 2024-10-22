using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AddWaypoint :WaypointElement, IWaypoint
{
    private VisualElement _btnTigger;
    private VisualElement _root;
    private Logger logger = new Logger(true, "WAY_POINT");

    //-----------------------------//
    //----- LIFECYCLE METHODS-----//
    //----------------------------//
    protected override void Start()
    {
        base.Start();
        _root = GetComponentInChildren<UIDocument>().rootVisualElement;
        _btnTigger= _root.Query<VisualElement>("VisElemTrigger").First();
        _btnTigger.RegisterCallback<ClickEvent>(evt=>OnTiggerButtonClick());
    }

    void Update()
    {
        
    }

    //-----------------------------//
    //-------Private Methods-------//
    //----------------------------//
    private void OnTiggerButtonClick()
    {
        Debug.Log("_tigger button clicked!");
        // Add your custom functionality here
    }
    
    //-----------------------------//
    //-------public Methods-------//
    //----------------------------//
}
