using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("in");
        if (other.CompareTag("Lamp")) {
            Debug.Log(SphereCounter);
            if (SphereCounter >= 1) {
                SphereCounter = 0;
                if (other.GetComponent<Light_Sphere>())
                {
                    LoseHealth(other.GetComponent<Light_Sphere>().Attack);
                }else if (other.GetComponent<Light_Search>())
                {
                    LoseHealth(other.GetComponent<Light_Search>().Attack);
                }
                else if (other.GetComponent<Light_Flash>())
                {
                    LoseHealth(other.GetComponent<Light_Flash>().Attack);
                }
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

    void LoseHealth(float damage) {
        Health -= damage;
        if (Health <= 0) {
            Death();
        }
    
    }

    void Death() {
        Destroy(gameObject);
    
    }

    protected virtual void Move() { 
        
    
    
    }

    public void SetTarget(GameObject Target_in)
    {
        Target = Target_in;


    }
}
