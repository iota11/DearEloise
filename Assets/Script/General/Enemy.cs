using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float Health, Speed, AttackRange, AttackTime, Attack;
    protected Transform Target;
    private Dictionary<int, Coroutine> LightCors;
    [SerializeField]
    private EnemyState CurrentState;
    private Coroutine CurrentCor;

    public enum EnemyState
    {
        Safe,
        Guard,
        Attacking
    }
    // Start is called before the first frame update
    void Awake()
    {
        LightCors = new Dictionary<int, Coroutine>();
        //Move();
    }


    protected void OnTriggerExit(Collider other)
    {
        //damage triggertime and damage cooldown time
        if (other.CompareTag("Lamp"))
        {
            if (!(other is MeshCollider && other.hideFlags != HideFlags.HideInInspector))
            {
                StopCoroutine(LightCors[other.GetInstanceID()]);
                LightCors.Remove(other.gameObject.GetInstanceID());
            }
        }

    }

    protected void OnTriggerEnter(Collider other) {
        //damage triggertime and damage cooldown time
        if (other.CompareTag("Lamp"))
        {
            if (!(other is MeshCollider && other.hideFlags != HideFlags.HideInInspector))
            {
                Coroutine LC = StartCoroutine(LightCor(other.GetComponent<LightPa>().CoolDownTime, other.GetComponent<LightPa>().TriggerTime, other.GetComponent<LightPa>().Attack, other.transform));

                LightCors.Add(other.gameObject.GetInstanceID(), LC);
            }
        }

    }



    //damage triggertime and damage cooldown time
    protected IEnumerator LightCor(float CoolDownTime, float TriggerTime, float attack, Transform LightOther) {
        float counter = 0;
        while (true) {

            if (counter >= TriggerTime) {
                GetComponentInChildren<EnemyDetection>().Damage(LightOther, attack);
                LoseHealth(attack);
                counter = 0;
                yield return new WaitForSeconds(CoolDownTime);
            }

            counter += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }


    public void ChangeState(EnemyState ES)
    {
        if (CurrentState == ES) {
            return;
        }
        CurrentState = ES;
        if (CurrentCor != null)
        {
            StopCoroutine(CurrentCor);
        }
        switch (ES)
        {
            case EnemyState.Safe:
                GetComponent<AIPath>().canMove = true;
                CurrentCor = StartCoroutine(Wandering());

                break;
            case EnemyState.Guard:
                GetComponent<AIPath>().canMove = true;
                CurrentCor = StartCoroutine(Approaching());

                break;
            case EnemyState.Attacking:
                GetComponent<AIPath>().canMove = false;
                CurrentCor = StartCoroutine(Attacking());
                
                break;


        }
    }

    protected IEnumerator Wandering() {

        yield return null;
    }

    protected IEnumerator Approaching()
    {
        while (CurrentState == EnemyState.Guard) {
            if ((new Vector2(Target.position.x - transform.position.x, Target.position.z - transform.position.z)).magnitude <= AttackRange) {
                ChangeState(EnemyState.Attacking);
            }
            yield return null;
        }
        yield return null;
    }

    protected IEnumerator Attacking() {
        float t = 0;
        while (CurrentState == EnemyState.Guard)
        {
            if (t >= AttackTime) {
                Target.gameObject.GetComponent<LightPa>().Attacked(Attack);
                t = 0;
            }


            if ((new Vector2(Target.position.x - transform.position.x, Target.position.z - transform.position.z)).magnitude > (AttackRange +0.1f))
            {
                ChangeState(EnemyState.Guard);
            }

            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
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

    public void SetTarget(Transform Target_in)
    {
        Target = Target_in;
        GetComponent<AIDestinationSetter>().target = Target_in;
        GetComponent<AIPath>().maxSpeed = Speed;
    }
}
