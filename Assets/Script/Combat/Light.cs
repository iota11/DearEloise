using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPa : MonoBehaviour
{
    public int Attack;
    [SerializeField]
    protected float MaxEnergy, EnergyConsume, Health, EnergyHealing, DepletionAmount;
    //damage triggertime and damage cooldown time
    public float CoolDownTime, TriggerTime;
    
    protected Light light;
    [SerializeField]
    private float energy;
    protected LightState CurrentState;
    protected enum LightState
    {
        Activate,
        Close,
        Depletion
    }

    void Start(){
        CurrentState = LightState.Close;
        light = GetComponent<Light>();
        ChangeState(LightState.Activate);
    }

    //player activate
    public void Interact() {
        if (CurrentState != LightState.Depletion) {
            ChangeState(LightState.Activate);
        }
    
    }

    public void Attacked(float Damage) {
        energy -= Damage;
        if (energy < 0)
        {
            ChangeState(LightState.Depletion);
        }


    }
    protected void StartFunction(){


        
        StartCoroutine(Function());
        StartCoroutine(Recession());

    }

    protected IEnumerator Recession() {

        while (CurrentState == LightState.Activate)
        {

            energy -= EnergyConsume * Time.deltaTime;
            if (energy <= 0) {
                ChangeState(LightState.Close);
            }
            yield return null;
        }

        yield return null;
    }

    protected IEnumerator Healing()
    {
        while (CurrentState == LightState.Depletion)
        {
            energy += EnergyHealing * Time.deltaTime;
            if (energy >= 0)
            {
                ChangeState(LightState.Close);
            }
            yield return null;
        }

        yield return null;
    }

    void ChangeState(LightState LS) {
        CurrentState = LS;
        switch (LS) {
            case LightState.Activate:
                
                energy = MaxEnergy;
                StartFunction();
                break;
            case LightState.Depletion:
                
                energy = DepletionAmount;
                light.enabled = false;
                StartCoroutine(Healing());
                break;
            case LightState.Close:
                
                energy = 0;
                light.enabled = false;
                break;


        }
    }

    protected virtual IEnumerator Function(){
        yield return null;


    }

}
