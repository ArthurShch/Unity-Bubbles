using System.Linq;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;

//using UnityEditor;


namespace Assets
{

    public class SectionOfShapContourCircle : SectionOfShape
    {
        public GameObject RedCircle;
        public GameObject BlueCircle;
        public GameObject GreenCircle;



        public SectionOfShapContourCircle(
            GameObject MainCube,
            GameObject Claster,
            Vector3 centerPanelSection,
            int sideRotate,
            bool EnableClaster)
            : base(MainCube, Claster, sideRotate, centerPanelSection, EnableClaster)
        {
            maxDist = Vector3.Distance(VMaxDist, MainCube.GetComponent<Renderer>().bounds.center);

            //  GameObject SectionADdd = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/untitled.fbx", typeof(GameObject)) as GameObject;
            GameObject SectionADdd = Resources.Load("untitled") as GameObject;


            SectionADdd.transform.rotation = Quaternion.identity;
            //SectionADdd.transform.Rotate(90f, 0, 0);

            SectionADdd.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //SectionADdd.transform.GetChild(1).GetComponent<Renderer>().material.color = new Color(1, 0, 0);
            //SectionADdd.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
            MonoBehaviour.Instantiate(SectionADdd, Section.transform, false);

            //SectionADdd.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
            SectionADdd.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            MonoBehaviour.Instantiate(SectionADdd, Section.transform, false);

            //SectionADdd.GetComponent<Renderer>().material.color = new Color(0, 0, 1);
            SectionADdd.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
            MonoBehaviour.Instantiate(SectionADdd, Section.transform, false);

            RedCircle = Section.transform.GetChild(0).gameObject;
            BlueCircle = Section.transform.GetChild(1).gameObject;
            GreenCircle = Section.transform.GetChild(2).gameObject;

            RedCircle.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
            BlueCircle.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1);
            GreenCircle.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);

            RedCircle.transform.GetChild(1).gameObject.transform.rotation = Quaternion.identity;
            BlueCircle.transform.GetChild(1).gameObject.transform.rotation = Quaternion.identity;
            GreenCircle.transform.GetChild(1).gameObject.transform.rotation = Quaternion.identity;


            //GameObject ExampleParticleSystem = GameObject.FindWithTag("ExampleParticleSystem");


            //#if UNITY_EDITOR

            //UnityEditor.

            //UnityEditorInternal.ComponentUtility.CopyComponent(ExampleParticleSystem.GetComponent<ParticleSystem>());
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(GreenCircle);
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(BlueCircle);
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(RedCircle);

            //UnityEditorInternal.ComponentUtility.CopyComponent(ExampleParticleSystem.GetComponent<ParticleSystem>());
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(GreenCircle.transform.GetChild(1).gameObject);
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(BlueCircle.transform.GetChild(1).gameObject);
            //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(RedCircle.transform.GetChild(1).gameObject);


            RedCircle.AddComponent<ParticleSystem>();
            BlueCircle.AddComponent<ParticleSystem>();
            GreenCircle.AddComponent<ParticleSystem>();

            ParticleSystem psRedCircle = RedCircle.GetComponent<ParticleSystem>();
            ParticleSystem psBlueCircle = BlueCircle.GetComponent<ParticleSystem>();
            ParticleSystem psGreenCircle = GreenCircle.GetComponent<ParticleSystem>();

            ParticleSystemRenderer psrRedCircle = RedCircle.GetComponent<ParticleSystemRenderer>();
            ParticleSystemRenderer psrBlueCircle = BlueCircle.GetComponent<ParticleSystemRenderer>();
            ParticleSystemRenderer psrGreenCircle = GreenCircle.GetComponent<ParticleSystemRenderer>();

            Material mat = new Material(Shader.Find("Transparent/Diffuse"));
            psrRedCircle.material = mat;
            psrBlueCircle.material = mat;
            psrGreenCircle.material = mat;

            psrRedCircle.material.color = new Color(1, 0, 0);
            psrBlueCircle.material.color = new Color(0, 0, 1);
            psrGreenCircle.material.color = new Color(0, 1, 0);

            psRedCircle.simulationSpace = ParticleSystemSimulationSpace.World;
            psBlueCircle.simulationSpace = ParticleSystemSimulationSpace.World;
            psGreenCircle.simulationSpace = ParticleSystemSimulationSpace.World;

            psRedCircle.scalingMode = ParticleSystemScalingMode.Shape;
            psBlueCircle.scalingMode = ParticleSystemScalingMode.Shape;
            psGreenCircle.scalingMode = ParticleSystemScalingMode.Shape;

            //psRedCircle.duration = 1;
            //psBlueCircle.duration = 1;
            //psGreenCircle.duration= 1f;

            psRedCircle.startLifetime = 0.6f;
            psBlueCircle.startLifetime = 0.6f;
            psGreenCircle.startLifetime = 0.6f;

            psRedCircle.startSpeed = 0;
            psBlueCircle.startSpeed = 0;
            psGreenCircle.startSpeed = 0;


            var shRedCircle = psRedCircle.shape;
            var shBlueCircle = psBlueCircle.shape;
            var shGreenCircle = psGreenCircle.shape;

            shRedCircle.shapeType = ParticleSystemShapeType.ConeVolumeShell;
            shBlueCircle.shapeType = ParticleSystemShapeType.ConeVolumeShell;
            shGreenCircle.shapeType = ParticleSystemShapeType.ConeVolumeShell;

            shRedCircle.angle = 1;
            shBlueCircle.angle = 1;
            shGreenCircle.angle = 1;

            shRedCircle.radius = 1;
            shBlueCircle.radius = 1;
            shGreenCircle.radius = 1;

            shRedCircle.length = 1;
            shBlueCircle.length = 1;
            shGreenCircle.length = 1;


            //psRedCircle.Play();
            //psBlueCircle.Play();
            //psGreenCircle.Play();

            var emRedCircle = psRedCircle.emission;
            var emBlueCircle = psBlueCircle.emission;
            var emGreenCircle = psGreenCircle.emission;

            emRedCircle.rate = 500;
            emBlueCircle.rate = 500;
            emGreenCircle.rate = 500;



            //GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

            //psrRedCircle.mesh = mesh;
            //psrBlueCircle.mesh = mesh;
            //psrGreenCircle.mesh = mesh;




            //Material matRedCircle = new Material(Shader.Find("Transparent/Diffuse"));
            //Material matBlueCircle = new Material(Shader.Find("Transparent/Diffuse"));
            //Material matGreenCircle = new Material(Shader.Find("Transparent/Diffuse"));

            //matRedCircle.color = new Color(1, 0, 0);

            //psrRedCircle.material = matRedCircle;












            //var ps = qwerty.GetComponent<ParticleSystem>();

            //ps.simulationSpace = ParticleSystemSimulationSpace.World;
            //ps.scalingMode = ParticleSystemScalingMode.Shape;



            //  GreenCircle.AddComponent<ParticleSystem>();

            // var ps = GreenCircle.gameObject.GetComponent<ParticleSystem>();
            // ParticleSystemRenderer psr = RedCircle.GetComponent<ParticleSystemRenderer>();

            // var sh = ps.shape;
            // sh.shapeType = ParticleSystemShapeType.Cone;
            // sh.angle = 1;
            // sh.radius = 1;
            // sh.length = 1;
            // var emitParams = new ParticleSystem.EmitParams();
            //// emitParams.velocity
            // //ps.emission.
            //// sh.randomDirection = true;

            // ps.simulationSpace = ParticleSystemSimulationSpace.World;
            // ps.scalingMode = ParticleSystemScalingMode.Shape;


            // var em = ps.emission;


            // SetSizeCircle();

            //SectionADdd.transform.parent = Section.transform;

            //CreateBubbles();

            // _sideRotate = sideRotate;

            //_opacity
            //#endif
        }

        public SectionOfShapeBubble RepleceType()
        {
            return new SectionOfShapeBubble(MainCube, Claster, CenterPanelSection, SideRotate, EnableClaster, Opacity);
        }

        public void SetSizeCircle()
        {
            //получить позицию круга 

            float Value = Vector3.Distance(RedCircle.transform.position, MainCube.transform.position);
            //дистанция от центра

            //25 = 100, самый край маленький размер
            //0 = 0, самый центр, большой размер



            float ValueRedCircle = 25f - Value;
            float ValueBlueCircle = 12.5f - Value;
            float ValueGreenCircle = 6.25f - Value;

            float OnePercentValueRedCircle = 25f / 100f;
            float OnePercentValueBlueCircle = 12.5f / 100f;
            float OnePercentValueGreenCircle = 6.25f / 100f;

            float PercentRedCircle = ValueRedCircle / OnePercentValueRedCircle;
            float PercentBlueCircle = ValueBlueCircle / OnePercentValueBlueCircle;
            float PercentGreenCircle = ValueGreenCircle / OnePercentValueGreenCircle;

            float OnePercentSizeRedCircle = 0.5f / 100f;
            float OnePercentSizeBlueCircle = 0.25f / 100f;
            float OnePercentSizeGreenCircle = 0.125f / 100f;

            float ScaleRedCircle = PercentRedCircle * OnePercentSizeRedCircle;
            float ScaleBlueCircle = PercentBlueCircle * OnePercentSizeBlueCircle;
            float ScaleGreenCircle = PercentGreenCircle * OnePercentSizeGreenCircle;

            // RedCircle.transform.rotation = Quaternion.identity;
            RedCircle.transform.localScale = new Vector3(ScaleRedCircle, ScaleRedCircle, ScaleRedCircle);
            var ps = BlueCircle.GetComponent<ParticleSystem>();
            var psg = GreenCircle.GetComponent<ParticleSystem>();

            if (ScaleBlueCircle < 0)
                ps.Stop();
            else ps.Play();

            if (ScaleGreenCircle < 0)
                psg.Stop();
            else psg.Play();

            ScaleBlueCircle = ScaleBlueCircle < 0 ? 0 : ScaleBlueCircle;
            ScaleGreenCircle = ScaleGreenCircle < 0 ? 0 : ScaleGreenCircle;

            BlueCircle.transform.localScale = new Vector3(ScaleBlueCircle, ScaleBlueCircle, ScaleBlueCircle);
            GreenCircle.transform.localScale = new Vector3(ScaleGreenCircle, ScaleGreenCircle, ScaleGreenCircle);





            // RedCircle.


            // SectionADdd.transform.Rotate(90f, 0, 0);
            //.GetComponent<Renderer>().bounds.center;
        }
        //public override void ClearPS()
        //{
        //    var psR = RedCircle.GetComponent<ParticleSystem>();
        //    var psB = BlueCircle.GetComponent<ParticleSystem>();
        //    var psG = GreenCircle.GetComponent<ParticleSystem>();

        //    psR.Clear();
        //    psB.Clear();
        //    psG.Clear();
        //}

        public override void SetPositionOffset(float offset)
        {
            base.SetPositionOffset(offset);


            //if (EnableClaster)
            //{
            SetSizeCircle();
            //}
        }
    }

}
