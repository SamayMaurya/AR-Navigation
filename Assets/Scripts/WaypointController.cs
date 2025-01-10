using UnityEngine;

public class WaypointController : WaypointElement
{
    private Logger logger = new Logger(true, "WAYPOINT_CONTROLLER");
    public Vector3 AddWaypoint(){
        return app.model.AddWaypoint();
    }    
}
