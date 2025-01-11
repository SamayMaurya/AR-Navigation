using UnityEngine;

public class WaypointController : WaypointElement
{
    private Logger logger = new Logger(true, "WAYPOINT_CONTROLLER");
    public void InitApp(){
        base.Start();
    }
    public Vector3 AddWaypoint(){
        Vector3 pose = app.model.AddWaypoint();
        return pose;
    }    
}
