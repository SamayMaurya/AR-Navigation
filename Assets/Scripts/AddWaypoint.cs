using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AddWaypoint :WaypointElement, IWaypoint
{
    private VisualElement _btnTigger;
    private VisualElement _root;
    [SerializeField] private GameObject breadcrumbPrefab;
    private List<GameObject> breadcrumbs = new List<GameObject>();
    private Logger logger = new Logger(true, "WAY_POINT");
    private readonly float anchorScale = 0.02f;
    private VisualElement _openLogs;
    private ScrollView _svLogs;
    private VisualElement _visElemLogs;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //-----------------------------//
    //----- LIFECYCLE METHODS-----//
    //----------------------------//
    protected override void Start()
    {   
        base.Start();
        _root = GetComponentInChildren<UIDocument>().rootVisualElement;
        _btnTigger= _root.Query<VisualElement>("VisElemTrigger").First();
        _visElemLogs=_root.Q<VisualElement>("VisElemLogs");
        _svLogs=_root.Q<ScrollView>("SVLogs");
        _openLogs=_root.Q<VisualElement>("LogState");
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
       app.controller.AddWaypoint();
    }
    //-----------------------------//
    //-------public Methods-------//
    //----------------------------//

    public void AddBreadCrumbs(Vector3 pose){
        logger.Log($"the coordinates received are = x: {pose.x} y: {pose.y} z: {pose.z}");
    }
}
