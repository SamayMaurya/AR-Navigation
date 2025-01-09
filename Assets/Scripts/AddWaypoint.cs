using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AddWaypoint : WaypointElement, IWaypoint
{
    private VisualElement _btnTrigger;
    private VisualElement _btnAddDestination;
    private VisualElement _btnCompleteMapping;
    private VisualElement _root;

    [SerializeField] private GameObject breadcrumbPrefab;

    private List<GameObject> breadcrumbs = new List<GameObject>();

    private Logger logger = new Logger(true, "WAY_POINT");

    private readonly float anchorScale = 0.02f;

    private VisualElement _openLogs;
    private ScrollView _svLogs;
    private VisualElement _visElemLogs;

    private bool isPlacingBreadcrumbs = false;

    // Set interval for breadcrumb placement to 1 meter
    private float breadcrumbInterval = 1.0f;

    private Vector3 lastBreadcrumbPosition; // Last position where a breadcrumb was placed

    private List<Destination> destinations = new List<Destination>();

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    protected override void Start()
    {
        base.Start();
        _root = GetComponentInChildren<UIDocument>().rootVisualElement;

        // Get UI elements
        _btnTrigger = _root.Query<VisualElement>("VisElemTrigger").First();
        _btnAddDestination = _root.Query<VisualElement>("AddDestination").First();
        _btnCompleteMapping = _root.Query<VisualElement>("CompleteMapping").First();
        _visElemLogs = _root.Q<VisualElement>("VisElemLogs");
        _svLogs = _root.Q<ScrollView>("SVLogs");
        _openLogs = _root.Q<VisualElement>("LogState");

        // Register callbacks
        _btnTrigger.RegisterCallback<ClickEvent>(evt => OnTriggerButtonClick());
        //_btnAddDestination.RegisterCallback<ClickEvent>(evt => OnAddDestinationButtonClick());
        _btnCompleteMapping.RegisterCallback<ClickEvent>(evt => OnCompleteMappingButtonClick());

        // Initialize lastBreadcrumbPosition to start at the camera's initial position
        lastBreadcrumbPosition = GetCurrentPosition();
    }

    void Update()
    {
        if (isPlacingBreadcrumbs)
        {
            PlaceBreadcrumbsAutomatically();
        }
    }

    private void OnTriggerButtonClick()
    {
        isPlacingBreadcrumbs = true;
        logger.Log("Breadcrumb placement started.");

        // Initialize last position when starting to place breadcrumbs
        lastBreadcrumbPosition = GetCurrentPosition();
    }

    private void OnAddDestinationButtonClick()
    {
        string destinationName = GetDestinationNameFromUI(); // A method to get input from UI
        Vector3 destinationPosition = GetCurrentPosition(); // Current AR camera position or raycast hit
        destinations.Add(new Destination(destinationName, destinationPosition));
        logger.Log($"Added destination: {destinationName} at {destinationPosition}");
    }

    private void OnCompleteMappingButtonClick()
    {
        isPlacingBreadcrumbs = false;
        logger.Log("Mapping completed.");
        DisplayMappingSummary();
    }

    private void PlaceBreadcrumbsAutomatically()
    {
        Vector3 currentPosition = GetCurrentPosition();

        // Check if the distance from the last breadcrumb position is greater than or equal to 1 meter
        if (Vector3.Distance(lastBreadcrumbPosition, currentPosition) >= breadcrumbInterval)
        {
            AddBreadCrumbs(currentPosition);
            lastBreadcrumbPosition = currentPosition; // Update last position to current position
        }
    }

    private string GetDestinationNameFromUI()
    {
        // TODO: Implement UI input field to get the destination name
        return "Default Destination";
    }

    private Vector3 GetCurrentPosition()
    {
        // TODO: Implement raycast or AR camera position retrieval
        return Camera.main.transform.position;
    }

    private void DisplayMappingSummary()
    {
        foreach (var destination in destinations)
        {
            logger.Log($"Destination: {destination.Name}, Position: {destination.Position}");
        }
    }

    public void AddBreadCrumbs(Vector3 pose)
    {
        logger.Log($"Adding breadcrumb at position = x: {pose.x}, y: {pose.y}, z: {pose.z}");
        GameObject breadcrumb = Instantiate(breadcrumbPrefab, pose, Quaternion.identity);
        breadcrumbs.Add(breadcrumb);
    }
}

// Destination class to store destination information
public class Destination
{
    public string Name { get; private set; }
    public Vector3 Position { get; private set; }

    public Destination(string name, Vector3 position)
    {
        Name = name;
        Position = position;
    }
}
