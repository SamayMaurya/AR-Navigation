using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    public static bool logsEnabled = true;
    private bool _enabled = true;
    private string _tag;

    public Logger(bool enabled, string tag)
    {
      _enabled = enabled;
      _tag = tag;
    }

    public void Log(string message)
    {
      if (_enabled && logsEnabled)
      Debug.Log("<color=teal>" + _tag + "</color>: " + message); 
    }
}
