using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class TriggerOfBubblesSetColor : MonoBehaviour {


    public ParticleSystem part;
    public Component mainCube;
    //  public List<ParticleCollisionEvent> collisionEvents;
    List<ParticleSystem.Particle> enterInside = new List<ParticleSystem.Particle>();
    ParticleSystem.Particle[] ppppp = new ParticleSystem.Particle[1000];

    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    void OnEnable()
    {
        part = GetComponent<ParticleSystem>();
        var trigger = part.trigger;
        mainCube = trigger.GetCollider(0);
    }

    //копия механизма получения цвета
    Color getColor(Vector3 positionShare, Vector3 positionMainCube) 
    {
        float maxDist = 43.30127f;

        float dist = maxDist - Vector3.Distance(positionMainCube, positionShare);

        dist = dist < 0 ? 0 : dist;

        float percentRED = dist / (maxDist / 100);
        float www = (100 - percentRED) / 100;

        //Color result = new Color(1, www, www, Opacity);
        Color result = new Color(1 - www, 0, www, 1);
        //Color result = new Color(www, 0, 1 - www, Opacity);

        return result;
    }

    //float timer=0;
    void Update() 
    {
        //timer += Time.deltaTime;
        //if (timer > 2)
        //{
        //    timer = 0;
        //}

        int numEnter = part.GetParticles(ppppp);
        if (numEnter > 0)
        {
            int a = 2;
        }
        part.SetParticles(ppppp, numEnter);

    }

    void OnParticleTrigger()
    {
        return;
        //if (timer != 0) 
        //{
        //    return;
        //}        

        int numEnter = part.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, enterInside);
     
        //mainCube.transform.position
        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enterInside[i];
            // byte RColor = Convert.ToByte(Math.Abs(p.position.z));
            //p.position.z;

            //if (p.color.r == 255)
            //{
            //     RColor =  (byte)(p.color.r - 1);

            //}
            //  p.startColor = new Color32(RColor, 0, 0, 255);
            p.startColor = getColor(transform.position, mainCube.gameObject.transform.position);
            //p.startColor = getColor(transform.position, mainCube.transform.position);


            enterInside[i] = p;
        }

        part.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, enterInside);

    }
}
