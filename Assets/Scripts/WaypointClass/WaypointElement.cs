using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointElement : BaseElement
{
    // Start is called before the first frame update
    public WaypointApplication app;
    protected virtual void Start()
    {
        app = GameObject.FindObjectOfType<WaypointApplication>();
    }
}
