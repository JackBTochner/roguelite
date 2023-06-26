using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBenchManager : MonoBehaviour
{
    public GameObject workBenchUI;

    public void CloseWorkBenchUI()
    {
        workBenchUI.SetActive(false);
    }
    
}