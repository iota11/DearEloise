using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Sphere : Light
{

    


    override protected IEnumerator Function(){
        SphereCollider SC = GetComponent<SphereCollider>();
        while (Activate)
        {
            SphereCollider.enabled = true;
            light.SetActive(true);
            yield return new WaitForSeconds(5f);
            SphereCollider.enabled = false;
            light.SetActive(false);
            yield return new WaitForSeconds(2f);
        }


        yield return null;
    }

}
