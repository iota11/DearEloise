using Pathfinding;
using Pathfinding.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGraphAssign : MonoBehaviour
{
    // Start is called before the first frame update
    public LocalSpaceRichAI ai;
    public LocalSpaceGraph gr;
    void Start()
    {
        ai = this.GetComponent<LocalSpaceRichAI>();
        ai.graph = gr;
    }

    // Update is called once per frame
    void Update()
    {
        ai.graph = gr;
    }
}
