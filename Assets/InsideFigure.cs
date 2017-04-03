using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Assets
{
    public class InsideFigure : MonoBehaviour
    {
        //public List<Collider> TriggerList = new List<Collider>();
        //GameObject respawnPrefab;
        public SectionOfShape parent;
        // Use this for initialization
        void Start()
        {
            //this.GetComponent<Collider>().enabled = false;
           // respawnPrefab = GameObject.FindWithTag("CenterAquo");
        }



        void OnTriggerEnter(Collider other)
        {
            //other.tag = "inside";
            //TriggerList.Add(other);

            if (parent != null)
            {
                this.GetComponent<Renderer>().material.color = ((SectionOfShapeBubble)parent).getColorForCylinder(this.gameObject.transform.position);
            }
            
            //other.GetComponent<Renderer>().material.color = new Color(1, 0, 0,1);
            

            //  Debug.Log("add new obj");

        }
        void OnTriggerExit(Collider other)
        {
            this.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
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

}
