using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets
{
    public class SectionOfShape
    {
        //прозрачность обьекта
        protected float _opacity = 1;
        //сторона поворота
        protected int _sideRotate; //1,2,3
        //секция в которую будут помещатся обьекты
        public GameObject Section = null;
        public GameObject Claster;
        public GameObject MainCube;
        public bool EnableClaster;
        public Vector3 VMaxDist;
        public SectionOfShape(int sideRotate, Vector3 centerPanelSection, bool EnableClaster)
        {
            _sideRotate = sideRotate;
            Section = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Section.transform.localScale = new Vector3(50, 50, 2);
            Section.transform.position = centerPanelSection;

            this.EnableClaster = EnableClaster;

            VMaxDist = new Vector3(
            MainCube.GetComponent<Renderer>().bounds.center.x + MainCube.GetComponent<Renderer>().transform.localScale.x * 0.5f,
            MainCube.GetComponent<Renderer>().bounds.center.y + MainCube.GetComponent<Renderer>().transform.localScale.y * 0.5f,
            MainCube.GetComponent<Renderer>().bounds.center.z + MainCube.GetComponent<Renderer>().transform.localScale.z * 0.5f
            );
        }

    }

    public class SectionOfShapeBubble : SectionOfShape
    {
        public float Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
            }
        }

        public int SideRotate
        {
            get { return _sideRotate; }
            set { _sideRotate = value; }
        }

        public SectionOfShapeBubble(
            GameObject MainCube,
            GameObject Claster,
            Vector3 centerPanelSection,
            int sideRotate,
            bool EnableClaster)
            : base(sideRotate, centerPanelSection, EnableClaster)
        {
            this.Claster = Claster;
            this.MainCube = MainCube;

            Section.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
            Section.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);

            // зделать поворот секции в соответсвии с sideRotate


            // _sideRotate = sideRotate;

            //_opacity

        }

        void CreateBubbles()
        {
            putBools();
            SetColorBubbles();


            //for (int i = 0; i < Section.transform.childCount; i++)
            //{
            //    Section.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
            //        = getColor(centerCube, Section.transform.GetChild(i).gameObject.transform.position, maxDist);
            //}
        }

        void SetColorBubbles()
        {
            float maxDist = Vector3.Distance(VMaxDist, MainCube.GetComponent<Renderer>().bounds.center);

            for (int i = 0; i < Section.transform.childCount; i++)
            {
                float dist = maxDist - Vector3.Distance(MainCube.transform.position, Section.transform.GetChild(i).gameObject.transform.position);
                dist = dist < 0 ? 0 : dist;

                float percentRED = dist / (maxDist / 100);
                float www = (100 - percentRED) / 100;

                Section.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(1, www, www, _opacity);
            }
        }


        void putBools()
        {
            Vector3 centerPanelSection = Section.transform.position;
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    Vector3 Dot = new Vector3();
                    switch (_sideRotate)
                    {
                        case 0:
                            {
                                Dot = new Vector3(
                       x + centerPanelSection.x - (50 * 0.5f),
                       y + centerPanelSection.y - (50 * 0.5f),
                       centerPanelSection.z);

                            }
                            break;
                        case 1:
                            {
                                Dot = new Vector3(
                     centerPanelSection.x,
                     x + centerPanelSection.y - (50 * 0.5f),
                     y + centerPanelSection.z - (50 * 0.5f));

                            }
                            break;
                        case 2:
                            {
                                Dot = new Vector3(
                     x + centerPanelSection.x - (50 * 0.5f),
                     centerPanelSection.y,
                     y + centerPanelSection.z - (50 * 0.5f));
                            }
                            break;
                        default:
                            break;
                    }

                    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    if (!EnableClaster)
                    {
                        //InsideFigures.InsideFigure.

                        Rigidbody cylinderRigidbody = cylinder.AddComponent<Rigidbody>();
                        cylinderRigidbody.isKinematic = true;
                        cylinderRigidbody.useGravity = false;

                        // Collider cylinderCollider = cylinder.AddComponent<SphereCollider>();

                        // asd.TriggerList
                        // Claster.GetComponent<TriggerList>()

                        //if (!Claster.GetComponent<Renderer>().bounds.Contains(Dot))
                        //    continue;
                    }





                    //cylinderRigidbody.mass = 1;


                    cylinder.transform.position = Dot;

                    //(cylinder.GetComponent<Collider>() as SphereCollider).radius = 100f;


                    // cylinder.GetComponent<Renderer>().material.color = new Color(1, 0, 0, opVal);

                    cylinder.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
                    //cylinder.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);

                    // cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, cylinder.transform.localScale.y, 0.1f);

                    cylinder.transform.parent = Section.transform;
                    //cylinder.transform.parent = panelSection.transform;
                    //cylinder.transform.localScale = new Vector3(1, 1, 1);

                }
            }
        }

    }

    //class SectionOfShapContourCircle : SectionOfShape
    //{

    //}
}
