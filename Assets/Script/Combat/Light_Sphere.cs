using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Sphere : LightPa
{

    


    override protected IEnumerator Function(){
        
        SphereCollider SC = GetComponent<SphereCollider>();
        while (Activate)
        {
            Debug.Log(light);
            SC.enabled = true;
            light.enabled = true;
            yield return new WaitForSeconds(5f);
            Debug.Log("off");
            SC.enabled = false;
            light.enabled = false;
            yield return new WaitForSeconds(2f);
        }


        yield return null;
    }

}
