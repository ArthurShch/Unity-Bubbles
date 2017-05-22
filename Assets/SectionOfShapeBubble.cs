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
        List<ParticleSystem> ListParticleSystem = new List<ParticleSystem>();

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
            //setColorAllparcticle();
            if (EnableClaster)
            {
                for (int i = 0; i < Section.transform.childCount; i++)
                {
                    GameObject Circle = Section.transform.GetChild(i).gameObject;

                    Color Ncolor = getColorForCylinder(Circle.transform.position);
                    Circle.GetComponent<Renderer>().material.color
                        = Ncolor;

                    if (MovedSection)
                    {
                        ParticleSystemRenderer psr =  Circle.GetComponent<ParticleSystemRenderer>();
                        psr.material.color = Ncolor;

                        var el = Circle.GetComponent<ParticleSystem>();

                        el.startLifetime = getTimeStartLife(Circle.transform.position, MainCube.transform.position);


                    }

                    //var el = Circle.GetComponent<ParticleSystem>();

                    //ParticleSystem.Particle[] m_Particles = new ParticleSystem.Particle[1000];
                    //int countParcticle = el.GetParticles(m_Particles);

                    //for (int t = 0; t < countParcticle; t++)
                    //{
                    //    // m_Particles[i].

                    //    m_Particles[t].color = getColorParcticle(m_Particles[t].position, MainCube.transform.position);
                    //}

                    //el.SetParticles(m_Particles, countParcticle);




                    //if (MovedSection)
                    //{
                    //    var ps = Section.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                    //    var colorModule = ps.colorOverLifetime;
                    //    colorModule.color = Ncolor;

                    //    //   ps.startColor = Ncolor;
                    //}
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

                        //if (MovedSection)
                        //{
                        //    var ps = Section.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                        //    var colorModule = ps.colorOverLifetime;
                        //    colorModule.color = Ncolor;

                        //    //  ps.startColor = Ncolor;
                        //}
                    }
                    else
                    {
                        //if (MovedSection)
                        //{
                        //    var ps = Section.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                        //    var colorModule = ps.colorOverLifetime;
                        //    colorModule.color = new Color(1, 0, 1, 0);

                        //    //ps.startColor =  new Color(1,0,1,0);
                        //}
                    }
                }
            }
        }

        public void setColorAllparcticle()
        {
            foreach (ParticleSystem el in ListParticleSystem)
            {
                ParticleSystem.Particle[] m_Particles = new ParticleSystem.Particle[1000];
                int countParcticle = el.GetParticles(m_Particles);

                for (int i = 0; i < countParcticle; i++)
                {
                    m_Particles[i].lifetime = 5; //getTimeStartLife(m_Particles[i].position, MainCube.transform.position);
                        //startLifetime = getTimeStartLife(m_Particles[i].position, MainCube.transform.position);

                    // m_Particles[i].color = getColorParcticle(m_Particles[i].position, MainCube.transform.position);
                }

                el.SetParticles(m_Particles, countParcticle);

                //el
            }

            //foreach (ParticleSystem el in ListParticleSystem)
            //{
            //    ParticleSystem.Particle[] m_Particles = new ParticleSystem.Particle[1000];
            //    int countParcticle = el.GetParticles(m_Particles);

            //    for (int i = 0; i < countParcticle; i++)
            //    {
            //        // m_Particles[i].sta

            //       // m_Particles[i].color = getColorParcticle(m_Particles[i].position, MainCube.transform.position);
            //    }

            //    el.SetParticles(m_Particles, countParcticle);

            //    //el
            //}
        }

        Color getColorParcticle(Vector3 positionShare, Vector3 positionMainCube)
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

        float getTimeStartLife(Vector3 positionShare, Vector3 positionMainCube) 
        {
            //1 to 3

            float startLife = 0.5f;


            float maxDist = 43.30127f;


            float wdwda = Vector3.Distance(positionMainCube, positionShare);
            float dist = maxDist - wdwda;

            dist = dist < 0 ? 0 : dist;
            float percentRED = dist / (maxDist / 100);
           // float www = (100 - percentRED) / 100;


            float dwqs = 2f / 100f;

            startLife += dwqs * percentRED ;

            return startLife;
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
                        ParticleSystem ps = cylinder.GetComponent<ParticleSystem>();
                        ListParticleSystem.Add(ps);

                        ParticleSystemRenderer psr = cylinder.GetComponent<ParticleSystemRenderer>();

                        //psr.get
                        //psr.renderMode = ParticleSystemRenderMode.Billboard;
                        //Material mat = new Material(Shader.Find("Unlit/Color"));
                        //mat.color = new Color(1, 0, 0);

                        //Shader wdwdw = Shader.Find("Particles/Alpha Blended Premultiply");

                        //Material mat = new Material(wdwdw);


                        Material mat = new Material(Shader.Find("Transparent/Diffuse"));


                        psr.material = mat;

                      //  psr.material.shader = Shader.Find("Particles/Alpha Blended Premultiply");
                        psr.material.color = new Color(1, 0, 0);
                        //psr.material.color = new Color(1, 0, 0);


                        //Mesh mesh  = new 

                        //GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
                        ////  Mesh mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<MeshFilter>().sharedMesh;

                        //psr.mesh = new Mesh();
                        //psr.mesh.
                        //GameObject.Destroy(gameObject);

                        //var pssssssssss = cylinder.GetComponent<Rigidbody>();

                        var sh = ps.shape;
                        // var trigger = ps.trigger;
                        // var colorOverLife = ps.colorOverLifetime;
                        var em = ps.emission;
                        //var colorModule = ps.colorOverLifetime;

                        //var sssss = ps.GetComponent<Renderer>();


                        // trigger.enabled = true;
                        //trigger.inside = ParticleSystemOverlapAction.Callback;

                        ps.scalingMode = ParticleSystemScalingMode.Shape;
                        
                        
                        //colorModule.enabled = true;
                        
                        
                        sh.enabled = false;
                        em.rate = 30;
                        ps.startSpeed = 0;
                        ps.startLifetime = 1f;

                        //ps.star
                        ps.startSize = 0.6f;

                         //ps.startColor = new Color(1, 0, 0);

                       // ps.startColor = new Color(1, 0, 0, 0);
                       // colorModule.color = new Color(1, 0, 0, 1);

                        ps.simulationSpace = ParticleSystemSimulationSpace.World;

                        //ps.playbackSpeed = 5;



                        // trigger.SetCollider(0, MainCube.GetComponent<BoxCollider>());

                        //cylinder.AddComponent<TriggerOfBubblesSetColor>();
                    }
                }
            }

            //if (MovedSection) 
            //{
            //    Section.AddComponent<ParticleSystem>();
            //    var ps = Section.GetComponent<ParticleSystem>();

            //    ParticleSystemRenderer psr = Section.GetComponent<ParticleSystemRenderer>();
            //    Material mat = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            //    psr.material = mat;
            //    psr.renderMode = ParticleSystemRenderMode.Mesh;

            //    //Mesh mesh  = new 

            //    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //    Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            //    //  Mesh mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<MeshFilter>().sharedMesh;

            //    psr.mesh = mesh;
            //    GameObject.Destroy(gameObject);

            //    //var pssssssssss = cylinder.GetComponent<Rigidbody>();

            //    var sh = ps.shape;
            //    var trigger = ps.trigger;
            //    // var colorOverLife = ps.colorOverLifetime;
            //    var em = ps.emission;
            //    var colorModule = ps.colorOverLifetime;

            //    //var sssss = ps.GetComponent<Renderer>();


            //    // trigger.enabled = true;
            //    //trigger.inside = ParticleSystemOverlapAction.Callback;
            //    ps.maxParticles = 100000;

            //    colorModule.enabled = true;
            //    sh.enabled = false;
            //    em.rate = 80;
            //    ps.startSpeed = 0;
            //    ps.startLifetime = 1f;
            //    ps.startSize = 0.01f;

            //    // ps.startColor = new Color(1, 0, 0);


            //    colorModule.color = new Color(1, 0, 0, 1);

            //    ps.simulationSpace = ParticleSystemSimulationSpace.World;

            //    //ps.playbackSpeed = 5;



            //    // trigger.SetCollider(0, MainCube.GetComponent<BoxCollider>());

            //    //cylinder.AddComponent<TriggerOfBubblesSetColor>();
            //}
        }

        public SectionOfShapContourCircle RepleceType()
        {
            SectionOfShapContourCircle sdwdw = new SectionOfShapContourCircle(MainCube, Claster, CenterPanelSection, SideRotate, EnableClaster);

            return sdwdw;
        }

    }

}
