using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : Enemy
{

    protected override void Move() {
        //StartCoroutine(MoveCor());
    
    
    }



    IEnumerator MoveCor() {
        
        float startx = gameObject.transform.position.x;
        float startz = gameObject.transform.position.z;
        float Dis = new Vector2(startx - Target.transform.position.x, startz - Target.transform.position.z).magnitude;
        float t = 0;

        while (Dis > 0.5f) {
            float newx = Mathf.Lerp(startx, Target.transform.position.x, t);
            float newz = Mathf.Lerp(startz, Target.transform.position.z, t);
            gameObject.transform.position = new Vector3(newx, gameObject.transform.position.y, newz);
            t += Time.deltaTime * Speed;
            yield return null;

        }

        yield return null;
    
    }

}
