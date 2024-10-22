using System.Collections;
using UnityEngine;
using System.Linq;


public class WaypointApplication : BaseApp
{
    [SerializeField] public WaypointModel model;
    [SerializeField] public WaypointController controller;
    public IWaypoint view;

     void Awake()
    {
        Debug.Log("JobsApplication Awake");
        // Unable to expose the view ( of type IJobsView) in the inspector
        view = FindObjectsOfType<MonoBehaviour>().OfType<IWaypoint>().FirstOrDefault();
        Debug.Log("view " + view);
    }
    protected override void Start()
    { 
        base.Start();
        Debug.Log("JobsApplication Start");
        // Unable to expose the view ( of type IJobsView) in the inspector
        view = FindObjectsOfType<MonoBehaviour>().OfType<IWaypoint>().FirstOrDefault();
    }
   
}
