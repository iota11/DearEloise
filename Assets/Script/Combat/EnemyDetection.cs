using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDetection : MonoBehaviour
{
    
    
    private Dictionary<Transform, KeyValuePair<float, float>> Targets;
    private float TimeToSafe;
    private Coroutine LoseCor;

    private void Awake()
    {
        Targets = new Dictionary<Transform, KeyValuePair<float, float>>();
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
                //float CurDis = (gameObject.transform.position - other.transform.position).magnitude;
                if (!Targets.ContainsKey(other.transform))
                {
                    Targets[other.transform] = new KeyValuePair<float, float>(0, 0);
                }
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
            if (!Targets.ContainsKey(other.transform))
            {
                Targets[other.transform] = new KeyValuePair<float, float>(0, 0);
            }
            //Targets[other.transform] = float.NegativeInfinity;
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

    public void Damage(Transform other, float attack) {
        if (LoseCor != null)
        {
            StopCoroutine(LoseCor);
        }

        //Targets[other] = 0;
        if (Targets.ContainsKey(other.transform))
        {
            Targets[other.transform] = new KeyValuePair<float, float>(Targets[other.transform].Key + attack, Targets[other.transform].Value);
        }
        else {
            Targets[other.transform] = new KeyValuePair<float, float>(attack, 0);
        }
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
        List<KeyValuePair<Transform, KeyValuePair<float, float>>> list = Targets.ToList();
        for(int i = 0; i < list.Count; i++) {
            Targets[list[i].Key] = new KeyValuePair<float, float>(Targets[list[i].Key].Key, CalcHate(list[i].Key, i==0));
        }
        // Sort the list by value in ascending order
        list.Sort((x, y) => x.Value.Value.CompareTo(y.Value.Value));
        
        return list.First().Key;
    }

    private float CalcHate(Transform Trans_in, bool CurTar) {
        float res = 0;
        if (Trans_in.gameObject.CompareTag("Lamp"))
        {
            res += 20;
        }
        else {
            res += 50;
        }
        res += Targets[Trans_in].Key;
        float CurDis = (new Vector2(transform.position.x - Trans_in.position.x, transform.position.z - Trans_in.position.z)).magnitude;
        res += (100 -CurDis);
        if (CurTar) {
            res *= 1.2f;
        }
        return res;
    }

}
