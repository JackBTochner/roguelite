using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public List<string> VisitedEncounters;
    public Run currentRun;
    public Run lastRun;

    public RunManagerAnchor runManagerAnchor;

    private void OnEnable()
    {
        runManagerAnchor.Provide(this);
    }
}


public class Run
{
    public List<string> VisitedEncounters;
}
