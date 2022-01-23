using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWater : MonoBehaviour
{
    public GameObject cm;
    public GameObject checkObject;
    void Start()
    {
        RenderSettings.fog = false;//Ãö³¬ Ãú
       
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == checkObject)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.5f, 0.2193f, 0.3895f, 0.81f);
            RenderSettings.fogDensity = 0.2f;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == checkObject)
        {
            RenderSettings.fog = false;
        }
    }
}
