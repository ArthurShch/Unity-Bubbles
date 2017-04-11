﻿using System.Linq;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets
{
    public abstract class SectionOfShape
    {
        //прозрачность обьекта
        private float _opacity = 1;
        //сторона поворота
        protected int _sideRotate; //1,2,3
        //секция в которую будут помещатся обьекты
        public GameObject Section = null;
        public GameObject Claster;
        public GameObject MainCube;
        public bool EnableClaster;
        public Vector3 VMaxDist;
        public float maxDist;

        //точка начала перемещение
        float StartPoint;
        Vector3 _centerPanelSection;
        //Vector3 CenterPanelSection;
        public Vector3 CenterPanelSection
        {
            get
            {
                return _centerPanelSection;
                //return Section.transform.position; 
            }
            set
            {
                if (Section != null)
                {
                    Section.transform.position = value;                    
                }
                _centerPanelSection = value;
            }
        }
        
        public float Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OpacityChane();
            }
        }
        public int SideRotate
        {
            get { return _sideRotate; }
            set
            {
                _sideRotate = value;
                 SideRotateChange();
            }
        }
        public virtual void SetPositionOffset(float offset)
        {
            switch (SideRotate)
            {
                case 0:
                    {
                        //panelSection.transform.position = new Vector3(
                        //    startPoint - SliderOfNewSection.value,
                        //    panelSection.transform.position.y,
                        //    panelSection.transform.position.z
                        //);
                        CenterPanelSection = new Vector3(
                            CenterPanelSection.x,
                            CenterPanelSection.y,
                            StartPoint - offset
                            );

                    } break;
                case 1:
                    {
                        CenterPanelSection = new Vector3(
                            StartPoint - offset,
                            CenterPanelSection.y,
                            CenterPanelSection.z
                        );

                    } break;
                case 2:
                    {
                        CenterPanelSection = new Vector3(
                            CenterPanelSection.x,
                            StartPoint - offset,
                            CenterPanelSection.z
                        );
                    } break;
                default:
                    break;
            }
        }
        protected void SideRotateChange()
        {
            switch (SideRotate)
            {
                case 0:
                    {
                        Section.transform.rotation = Quaternion.identity;
                        CenterPanelSection = new Vector3(
                            MainCube.transform.position.x,
                            MainCube.transform.position.y,
                            CenterPanelSection.z
                            );

                        //panelSection.transform.Rotate(0, 0, 0);

                        StartPoint = MainCube.GetComponent<Renderer>().bounds.center.z
                            + MainCube.GetComponent<Renderer>().transform.localScale.z * 0.5f;

                        //         startPoint = MainCube.GetComponent<Renderer>().bounds.center.x
                        //+ MainCube.GetComponent<Renderer>().transform.localScale.x * 0.5f;
                    }
                    break;
                case 1:
                    {
                        Section.transform.rotation = Quaternion.identity;
                        Section.transform.Rotate(0, 90, 0);
                        CenterPanelSection = new Vector3(
                           CenterPanelSection.x,
                           MainCube.transform.position.y,
                           MainCube.transform.position.z
                           );

                        StartPoint = MainCube.GetComponent<Renderer>().bounds.center.x
                            + MainCube.GetComponent<Renderer>().transform.localScale.x * 0.5f;
                    }
                    break;
                case 2:
                    {
                        Section.transform.rotation = Quaternion.identity;
                        Section.transform.Rotate(90, 0, 0);


                        CenterPanelSection = new Vector3(
                           MainCube.transform.position.x,
                           CenterPanelSection.y,
                           MainCube.transform.position.z
                           );

                        StartPoint = MainCube.GetComponent<Renderer>().bounds.center.y
                           + MainCube.GetComponent<Renderer>().transform.localScale.y * 0.5f;
                    }
                    break;
            }
        }
        protected virtual void OpacityChane() { }

        public void Create()
        {
            Section = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Section.transform.localScale = new Vector3(50, 50, 2);
            //Section.transform.position = CenterPanelSection;
            SideRotateChange();
            Section.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
            Section.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        }

        public SectionOfShape(GameObject MainCube, GameObject Claster, int sideRotate, Vector3 centerPanelSection, bool EnableClaster)
        {
            this.EnableClaster = EnableClaster;
            this.Claster = Claster;
            this.MainCube = MainCube;

            _sideRotate = sideRotate;
            CenterPanelSection = centerPanelSection;
            Create();
            

            

            VMaxDist = new Vector3(
            MainCube.GetComponent<Renderer>().bounds.center.x + MainCube.GetComponent<Renderer>().transform.localScale.x * 0.5f,
            MainCube.GetComponent<Renderer>().bounds.center.y + MainCube.GetComponent<Renderer>().transform.localScale.y * 0.5f,
            MainCube.GetComponent<Renderer>().bounds.center.z + MainCube.GetComponent<Renderer>().transform.localScale.z * 0.5f
            );
        }
    }

    public class SectionOfShapeBubble : SectionOfShape
    {
        float MaxElements = 50;
        public float CountBubles = 51;
        public SectionOfShapeBubble(
            GameObject MainCube,
            GameObject Claster,
            Vector3 centerPanelSection,
            int sideRotate,
            bool EnableClaster, float Opacity)
            : base(MainCube, Claster, sideRotate, centerPanelSection, EnableClaster)
        {
            this.Opacity = Opacity;


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
                    Section.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                        = getColorForCylinder(Section.transform.GetChild(i).gameObject.transform.position);
                }
            }
            else
            {
                for (int i = 0; i < Section.transform.childCount; i++)
                {
                    if (Section.transform.GetChild(i).gameObject.GetComponent<InsideFigure>().IsInside)
                    {
                        Section.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                        = getColorForCylinder(Section.transform.GetChild(i).gameObject.transform.position);
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

                    cylinder.AddComponent<ParticleSystem>();
                    var ps = cylinder.GetComponent<ParticleSystem>();
                    var sh = ps.shape;
                    var colorOverLife = ps.colorOverLifetime;                    
                    var em = ps.emission;

                    sh.enabled = false;
                    em.rate = 20;
                    ps.startSpeed = 0;
                    ps.time = 10;
                    ps.startSize = 2;
                    ps.simulationSpace = ParticleSystemSimulationSpace.World;
                    ps.playbackSpeed = 5;
                    //colorOverLife.color = 

                    //ps.;

                    

                    

                }
            }
        }
    }

    //class SectionOfShapContourCircle : SectionOfShape
    //{

    //}
}
