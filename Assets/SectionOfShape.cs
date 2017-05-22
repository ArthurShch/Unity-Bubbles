using System.Linq;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEditor;

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

        ////очистка созданных парктикулей
        //public virtual void ClearPS() { }

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
            Create();
            CenterPanelSection = centerPanelSection;
            

            VMaxDist = new Vector3(
            MainCube.GetComponent<Renderer>().bounds.center.x + MainCube.GetComponent<Renderer>().transform.localScale.x * 0.5f,
            MainCube.GetComponent<Renderer>().bounds.center.y + MainCube.GetComponent<Renderer>().transform.localScale.y * 0.5f,
            MainCube.GetComponent<Renderer>().bounds.center.z + MainCube.GetComponent<Renderer>().transform.localScale.z * 0.5f
            );
        }

        //public void ClearChild() 
        //{
        //    for (int i = 0; i < Section.transform.childCount; i++)
        //    {
                
        //    }
        //}
    }

   
}
