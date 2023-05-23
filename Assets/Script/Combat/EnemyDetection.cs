using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDetection : MonoBehaviour
{
    
    
    private Dictionary<Transform, float> Targets;
    private float TimeToSafe;
    private Coroutine LoseCor;

    private void Awake()
    {
        Targets = new Dictionary<Transform, float>();
    }

    void OnTriggerEnter(Collider other)
    {
        //detect light/human
        if (other.CompareTag("Lamp"))
        {
            
            if (other is MeshCollider && other.hideFlags != HideFlags.HideInInspector) {
                if (LoseCor != null) {
                    StopCoroutine(LoseCor);
                }
                float CurDis = (gameObject.transform.position - other.transform.position).magnitude;
                Targets[other.transform] = CurDis;
                transform.parent.gameObject.GetComponent<Enemy>().SetTarget(getTarget());
                transform.parent.gameObject.GetComponent<Enemy>().ChangeState(Enemy.EnemyState.Guard);
                
            }

        }

        if (other.CompareTag("Player"))
        {
            if (LoseCor != null)
            {
                StopCoroutine(LoseCor);
            }
            
            Targets[other.transform] = float.NegativeInfinity;
            transform.parent.gameObject.GetComponent<Enemy>().SetTarget(getTarget());
            transform.parent.gameObject.GetComponent<Enemy>().ChangeState(Enemy.EnemyState.Guard);

        }
    }

    void OnTriggerExit(Collider other)
    {
        //detect light/human
        if (other.CompareTag("Lamp"))
        {

            if (other is MeshCollider && other.hideFlags != HideFlags.HideInInspector)
            {
                Targets.Remove(other.transform);
                if (Targets.Count <= 0)
                {
                    LoseCor = StartCoroutine(ToSafe());
                }
                else
                {
                    transform.parent.gameObject.GetComponent<Enemy>().SetTarget(getTarget());
                }

            }
        }
        if (other.CompareTag("Player"))
        {
            Targets.Remove(other.transform);
            if (Targets.Count <= 0)
            {
                LoseCor = StartCoroutine(ToSafe());
            }
            else
            {
                transform.parent.gameObject.GetComponent<Enemy>().SetTarget(getTarget());
            }


        }
    }

    public void Damage(Transform other) {
        if (LoseCor != null)
        {
            StopCoroutine(LoseCor);
        }
        
        Targets[other] = 0;
        transform.parent.gameObject.GetComponent<Enemy>().SetTarget(getTarget());
        transform.parent.gameObject.GetComponent<Enemy>().ChangeState(Enemy.EnemyState.Guard);


    }

    IEnumerator ToSafe() {
        yield return new WaitForSeconds(TimeToSafe);
        transform.parent.gameObject.GetComponent<Enemy>().ChangeState(Enemy.EnemyState.Safe);


        yield return null;
    
    
    }



    private Transform getTarget()
    {
        // Convert the dictionary to a list of key-value pairs
        List<KeyValuePair<Transform, float>> list = Targets.ToList();

        // Sort the list by value in ascending order
        list.Sort((x, y) => x.Value.CompareTo(y.Value));

        return list.First().Key;
    }


}
