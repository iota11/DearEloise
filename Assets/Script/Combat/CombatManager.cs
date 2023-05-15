using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private GameObject LightHouse, Enemy;
    [SerializeField]
    private float MaxSpawn, MinSpawn, MaxSpawnTime, MinSpawnTime;
    private bool battle;

    // Start is called before the first frame update
    void Start()
    {
        battle = true;
        StartCoroutine(GenEnemy());
    }

    IEnumerator GenEnemy() {
        while (battle) {
            float SpawnDis = Random.Range(MinSpawn, MaxSpawn);
            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            Vector2 Dir = new Vector2(x, z);
            Dir.Normalize();
            var Temp = Instantiate(Enemy, new Vector3(LightHouse.transform.position.x + Dir.x * SpawnDis, 0.5f, LightHouse.transform.position.z + Dir.y * SpawnDis), Quaternion.identity);
            Temp.GetComponent<Enemy_Combat>().SetTarget(LightHouse);
            float SpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
            yield return new WaitForSeconds(SpawnTime);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
