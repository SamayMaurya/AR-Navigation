using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class WaypointModel : WaypointElement
{
    private Logger logger = new Logger(true, "WAYPOINT_MODEL");
    [SerializeField] private ARRaycastManager arRaycastManager;
     private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public Vector3 AddWaypoint(){
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        
        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            Vector3 position = hitPose.position;
            logger.Log($"the coordinates are = x: {position.x} y: {position.y} z: {position.z}");
            return position;
        }
        return new Vector3(0,0,0);
    }
}
