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
        _openLogs.RegisterCallback<ClickEvent>(evt=>SetLogState());
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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("BreadCrumb");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("BreadCrumb"))
            {
                //TODO: delete breadcrumbs
            }
            else{
                GameObject spawnedGO = Instantiate(breadcrumbPrefab, hit.point, Quaternion.identity);
                float scale = 1f / spawnedGO.transform.parent.lossyScale.x * anchorScale;
                spawnedGO.transform.localScale = new Vector3(scale, scale, scale);
                Quaternion spawnedGORotation = spawnedGO.transform.localRotation;
                Vector3 spawnedGOPosition = spawnedGO.transform.localPosition;
                AddLogs(spawnedGOPosition,spawnedGORotation);
            }
        }
    }

    private void SetLogState(){
        if(_visElemLogs.resolvedStyle.display ==DisplayStyle.Flex){
            _visElemLogs.style.display=DisplayStyle.None;
        }
        else{
            _visElemLogs.style.display=DisplayStyle.Flex;
        }
    }

    private void AddLogs(Vector3 pos,Quaternion rotation){
        Label log = new Label();
        log.style.color=Color.red;
        log.text=$"coordiates : X: {pos.x}, y: {pos.y}, z: {pos.z}";
        _svLogs.Add(log);
    }
    //-----------------------------//
    //-------public Methods-------//
    //----------------------------//
}
