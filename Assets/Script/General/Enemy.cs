using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float Health, Speed;
    protected GameObject Target;
    private Dictionary<int, Coroutine> LightCors;
    // Start is called before the first frame update
    void Awake()
    {
        LightCors = new Dictionary<int, Coroutine>();
        Move();
    }




    protected void OnTriggerEnter(Collider other) {
        //damage triggertime and damage cooldown time
        if (other.CompareTag("Lamp"))
        {
            Coroutine LC = StartCoroutine(LightCor(other.GetComponent<LightPa>().CoolDownTime, other.GetComponent<LightPa>().TriggerTime, other.GetComponent<LightPa>().Attack));
            
            LightCors.Add(other.gameObject.GetInstanceID(), LC);
        }

    }

    protected void OnTriggerExit(Collider other)
    {
        //damage triggertime and damage cooldown time
        if (other.CompareTag("Lamp"))
        {
            StopCoroutine(LightCors[other.GetInstanceID()]);
            LightCors.Remove(other.gameObject.GetInstanceID());
        }

    }

    //damage triggertime and damage cooldown time
    protected IEnumerator LightCor(float CoolDownTime, float TriggerTime, float attack) {
        float counter = 0;
        while (true) {
            if (counter >= TriggerTime) {
                LoseHealth(attack);
            }
            if (counter >= CoolDownTime + TriggerTime)
            {
                counter = 0;
            }
            counter += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }



    public virtual void Found() { 
    
    
    }

    protected void Attack() { 
    
    }

    protected void Impact() { 
    
    
    }

    void LoseHealth(float damage) {
        Health -= damage;
        if (Health <= 0) {
            Death();
        }
    
    }

    protected void Death() {
        Destroy(gameObject);
    
    }

    protected virtual void Move() { 
        
    
    
    }

    public void SetData(GameObject Target_in)
    {
        Target = Target_in;
        GetComponent<AIDestinationSetter>().target = Target_in.transform;
        GetComponent<AIPath>().maxSpeed = Speed;
    }
}
