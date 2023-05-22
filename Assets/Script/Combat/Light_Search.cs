using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Search : LightPa
{
    [SerializeField]
    protected float RotateSpeed;
    [SerializeField]
    protected float RotateRange;


    override protected IEnumerator Function(){
        var start = gameObject.transform.rotation;
        var end = Quaternion.Euler(gameObject.transform.eulerAngles + new Vector3(0, RotateRange, 0));
        float t = 0;
        while (CurrentState == LightState.Activate) {
            gameObject.transform.rotation = Quaternion.Slerp(start, end, t);
            t += Time.deltaTime * RotateSpeed;
            if (t >= 1) {
                t = 0;
                var tmp = start;
                start = end;
                end = tmp;
            }
            yield return null;
        
        
        }

        yield return null;
    }

}
