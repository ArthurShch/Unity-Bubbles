using System.Linq;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEditor;

namespace Assets
{
    public class SectionOfShapeBubble : SectionOfShape
    {
        float MaxElements = 50;
        public float CountBubles = 51;
        public bool MovedSection;
        public SectionOfShapeBubble(
            GameObject MainCube,
            GameObject Claster,
            Vector3 centerPanelSection,
            int sideRotate,
            bool EnableClaster, float Opacity, bool MovedSection = false)
            : base(MainCube, Claster, sideRotate, centerPanelSection, EnableClaster)
        {
            this.Opacity = Opacity;
            this.MovedSection = MovedSection;

            maxDist = Vector3.Distance(VMaxDist, MainCube.GetComponent<Renderer>().bounds.center);

            CreateBubbles();

            // _sideRotate = sideRotate;

            //_opacity

        }

        //поворот
        //protected override void SideRotateChange() 
        //{

        //}

        protected override void OpacityChane()
        {
            for (int i = 0; i < Section.transform.childCount; i++)
            {
                GameObject sphere = Section.transform.GetChild(i).gameObject;

                Color oldColor = sphere.GetComponent<Renderer>().material.color;
                if (oldColor.r != 0)
                {
                    sphere.GetComponent<Renderer>().material.color
                    = new Color(oldColor.r, oldColor.g, oldColor.b, Opacity);
                }
            }
        }

        public void CreateBubbles()
        {
            //Create();
            putBools();
            if (EnableClaster)
            {
                SetColorBubbles();
            }

        }

        public Color getColorForCylinder(Vector3 positionShare)
        {
            float dist = maxDist - Vector3.Distance(MainCube.transform.position, positionShare);

            dist = dist < 0 ? 0 : dist;

            float percentRED = dist / (maxDist / 100);
            float www = (100 - percentRED) / 100;

            //Color result = new Color(1, www, www, Opacity);
            Color result = new Color(1 - www, 0, www, Opacity);
            //Color result = new Color(www, 0, 1 - www, Opacity);

            return result;
        }

        public override void SetPositionOffset(float offset)
        {
            base.SetPositionOffset(offset);
            //if (EnableClaster)
            //{
            SetColorBubbles();
            //}
        }

        void SetColorBubbles()
        {
            if (EnableClaster)
            {
                for (int i = 0; i < Section.transform.childCount; i++)
                {
                    Color Ncolor = getColorForCylinder(Section.transform.GetChild(i).gameObject.transform.position);
                    Section.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                        = Ncolor;


                    if (MovedSection)
                    {
                        var ps = Section.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                        var colorModule = ps.colorOverLifetime;
                        colorModule.color = Ncolor;
                     
                        //   ps.startColor = Ncolor;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Section.transform.childCount; i++)
                {
                    if (Section.transform.GetChild(i).gameObject.GetComponent<InsideFigure>().IsInside)
                    {
                        //Section.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                        //= getColorForCylinder(Section.transform.GetChild(i).gameObject.transform.position);

                        Color Ncolor = getColorForCylinder(Section.transform.GetChild(i).gameObject.transform.position);
                        Section.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                            = Ncolor;

                        if (MovedSection)
                        {
                            var ps = Section.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                            var colorModule = ps.colorOverLifetime;
                            colorModule.color = Ncolor;

                          //  ps.startColor = Ncolor;
                        }
                    }
                    else
                    {
                        if (MovedSection)
                        {
                            var ps = Section.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                            var colorModule = ps.colorOverLifetime;
                            colorModule.color = new Color(1, 0, 1, 0);
                            
                            //ps.startColor =  new Color(1,0,1,0);
                        }        
                    }
                }
            }
        }



        void putBools()
        {


            float CountElements = CountBubles - 2;

            float factor = MaxElements / CountElements;

            Vector3 centerPanelSection = Section.transform.position;
            for (float x = 0; x <= CountElements; x++)
            {
                for (float y = 0; y <= CountElements; y++)
                {
                    Vector3 Dot = new Vector3();
                    switch (_sideRotate)
                    {
                        case 0:
                            {
                                Dot = new Vector3(
                       x * factor + centerPanelSection.x - (MaxElements * 0.5f),
                       y * factor + centerPanelSection.y - (MaxElements * 0.5f),
                       centerPanelSection.z);

                            }
                            break;
                        case 1:
                            {
                                Dot = new Vector3(
                     centerPanelSection.x,
                     x * factor + centerPanelSection.y - (MaxElements * 0.5f),
                     y * factor + centerPanelSection.z - (MaxElements * 0.5f));

                            }
                            break;
                        case 2:
                            {
                                Dot = new Vector3(
                     x * factor + centerPanelSection.x - (MaxElements * 0.5f),
                     centerPanelSection.y,
                     y * factor + centerPanelSection.z - (MaxElements * 0.5f));
                            }
                            break;
                        default:
                            break;
                    }

                    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    cylinder.transform.position = Dot;
                    cylinder.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
                    cylinder.transform.parent = Section.transform;
                    if (!EnableClaster)
                    {
                        cylinder.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
                        cylinder.AddComponent<InsideFigure>().parent = this;
                    }

                    //если панель движущиеся дать ей шлейф
                    if (MovedSection)
                    {
                       


                        cylinder.AddComponent<ParticleSystem>();
                        

                        
                        var ps = cylinder.GetComponent<ParticleSystem>();
                        //var pssssssssss = cylinder.GetComponent<Rigidbody>();

                        var sh = ps.shape;
                        var trigger = ps.trigger;
                       // var colorOverLife = ps.colorOverLifetime;
                        var em = ps.emission;
                       var colorModule = ps.colorOverLifetime;

                       //var sssss = ps.GetComponent<Renderer>();


                        // trigger.enabled = true;
                        //trigger.inside = ParticleSystemOverlapAction.Callback;


                        colorModule.enabled = true;
                        sh.enabled = false;
                        em.rate = 20;
                        ps.startSpeed = 0;
                        ps.startLifetime = 1f;
                        ps.startSize = 5;

                       // ps.startColor = new Color(1, 0, 0);
                         

                        colorModule.color = new Color(0, 0, 0, 0);

                        ps.simulationSpace = ParticleSystemSimulationSpace.World;

                        //ps.playbackSpeed = 5;

                        

                        // trigger.SetCollider(0, MainCube.GetComponent<BoxCollider>());

                        //cylinder.AddComponent<TriggerOfBubblesSetColor>();
                    }
                }
            }
        }

        public SectionOfShapContourCircle RepleceType()
        {
            SectionOfShapContourCircle sdwdw = new SectionOfShapContourCircle(MainCube, Claster, CenterPanelSection, SideRotate, EnableClaster);

            return sdwdw;
        }

    }

}
