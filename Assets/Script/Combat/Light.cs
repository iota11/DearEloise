using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public int Attack;
    [SerializeField]
    protected int MaxEnergy, EnergyConsume, Health;
    protected bool Activate;
    protected Light light;
    

    void Start(){
        light = GetComponent<Light>();
    }

    protected void StartFunction(){
        StartCoroutine(Function());


    }

    protected virtual IEnumerator Function(){
        yield return null;


    }

}
