using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Flash : LightPa
{
    


    override protected IEnumerator Function(){
        ConeCollider SC = GetComponent<ConeCollider>();
        while (CurrentState == LightState.Activate)
        {

            SC.enabled = true;
            light.enabled = true;
            yield return new WaitForSeconds(2f);

            SC.enabled = false;
            light.enabled = false;
            yield return new WaitForSeconds(1.1f);
        }


        yield return null;
    }

}
