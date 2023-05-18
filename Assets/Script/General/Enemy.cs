using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float Health, Speed;
    protected GameObject Target;
    private float SphereCounter;
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Lamp")) {

            if (SphereCounter >= 1) {
                SphereCounter = 0;

                LoseHealth(other.GetComponent<LightPa>().Attack);

            }
            SphereCounter += Time.deltaTime;



        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lamp"))
        {
            SphereCounter = 0;
        }

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
