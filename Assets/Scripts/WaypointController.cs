using UnityEngine;

public class WaypointController : WaypointElement
{
    private Logger logger = new Logger(true, "WAYPOINT_CONTROLLER");
    public void AddWaypoint(){
        Vector3 pose = app.model.AddWaypoint();
        app.view.AddBreadCrumbs(pose);
    }    
}
