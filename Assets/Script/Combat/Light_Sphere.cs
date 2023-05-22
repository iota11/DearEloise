using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Sphere : LightPa
{

    


    override protected IEnumerator Function(){
        
        SphereCollider SC = GetComponent<SphereCollider>();
        while (CurrentState == LightState.Activate)
        {
            
            SC.enabled = true;
            light.enabled = true;
            yield return new WaitForSeconds(5f);
            
            SC.enabled = false;
            light.enabled = false;
            yield return new WaitForSeconds(2f);
        }


        yield return null;
    }

}
