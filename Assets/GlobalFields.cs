using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace Assets
{
    public class GlobalFields : MonoBehaviour
    {
        public List<SectionOfShape> Sections = new List<SectionOfShape>();
        public SectionOfShape MovingSection;
        //private ParticleSystem ps;
        // Use this for initialization
        void Start()
        {
            //ps = GetComponent<ParticleSystem>();
            //ps.Stop(); // Cannot set duration whilst particle system is playing
            
            //var main = ps.shape;
            //main.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}