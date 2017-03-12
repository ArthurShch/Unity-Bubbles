using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InsideFigure : MonoBehaviour
{
    List<Collider> TriggerList = new List<Collider>();

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //if the object is not already in the list
       
            Debug.Log("add new obj");

    }

    //called when something exits the trigger
    //void OnTriggerExit(Collider other)
    //{
    //    //if the object is in the list
    //    if (TriggerList.Contains(other))
    //    {
    //        //remove it from the list
    //        TriggerList.Remove(other);
    //        Debug.Log("del some obj");
    //    }
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
