using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPa : MonoBehaviour
{
    public int Attack;
    [SerializeField]
    protected int MaxEnergy, EnergyConsume, Health;
    protected bool Activate;
    protected Light light;
    

    void Start(){
        light = GetComponent<Light>();
        Debug.Log(light);
        StartFunction();
    }

    protected void StartFunction(){
        
        Activate = true;
        StartCoroutine(Function());


    }

    protected virtual IEnumerator Function(){
        yield return null;


    }

}
