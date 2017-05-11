using System.Linq;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace Assets
{

   public class SectionOfShapContourCircle : SectionOfShape
    {
        GameObject RedCircle;
        GameObject BlueCircle;
        GameObject GreenCircle;


        public SectionOfShapContourCircle(
            GameObject MainCube,
            GameObject Claster,
            Vector3 centerPanelSection,
            int sideRotate,
            bool EnableClaster)
            : base(MainCube, Claster, sideRotate, centerPanelSection, EnableClaster)
        {
            maxDist = Vector3.Distance(VMaxDist, MainCube.GetComponent<Renderer>().bounds.center);

            GameObject SectionADdd = AssetDatabase.LoadAssetAtPath("Assets/untitled.fbx", typeof(GameObject)) as GameObject;
            SectionADdd.transform.rotation = Quaternion.identity;
            SectionADdd.transform.Rotate(90f, 0, 0);

            SectionADdd.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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

            SetSizeCircle();

            //SectionADdd.transform.parent = Section.transform;

            //CreateBubbles();

            // _sideRotate = sideRotate;

            //_opacity

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

            ScaleBlueCircle = ScaleBlueCircle < 0 ? 0 : ScaleBlueCircle;
            ScaleGreenCircle = ScaleGreenCircle < 0 ? 0 : ScaleGreenCircle;

            BlueCircle.transform.localScale = new Vector3(ScaleBlueCircle, ScaleBlueCircle, ScaleBlueCircle);
            GreenCircle.transform.localScale = new Vector3(ScaleGreenCircle, ScaleGreenCircle, ScaleGreenCircle);



            // RedCircle.


            // SectionADdd.transform.Rotate(90f, 0, 0);
            //.GetComponent<Renderer>().bounds.center;
        }


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
