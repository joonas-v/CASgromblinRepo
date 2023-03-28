using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class RoomController : MonoBehaviour
{
    //initialize vars
    public int ID = 0;
    private ProceduralGenController proc;
    private bool toggled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        NavMeshBuilder.BuildNavMesh();
        proc = GameObject.Find("ProceduralGenerator").GetComponent<ProceduralGenController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name.Contains("Player") && !toggled)
        {
            proc.RoomStart(ID);
            toggled = true;
        }
    }
}
