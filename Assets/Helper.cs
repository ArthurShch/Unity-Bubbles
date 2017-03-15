using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Helpers
{
    public class Helper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="respawnPrefab">Секция на место которой создаются секции</param>
        /// <param name="Claster">Обьект в который нужно вписать шары</param>
        /// <param name="EnableClaster">true - показать все точки, false - скрыть точки вне кластера</param>
        /// <param name="opVal">Прозрачность секции</param>
        /// <param name="maxDist">Максимальная дистация </param>
        /// <param name="centerPanelSection">Позиция секции</param>
        /// <returns>Возвращает секцию с шарами</returns>
        public static GameObject createNewBoolsPanel(GameObject respawnPrefab, GameObject Claster, bool EnableClaster, float opVal, float maxDist, Vector3 centerPanelSection)
        {
            GameObject cyb = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //Vector3 centerPanelSection = panelSection.GetComponent<Renderer>().bounds.center;

            cyb.GetComponent<Renderer>().transform.position = centerPanelSection;
            // cyb.GetComponent<Renderer>().transform.localScale = panelSection.GetComponent<Renderer>().transform.lossyScale;

            cyb.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0);
            cyb.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
            cyb.transform.localScale = new Vector3(50, 50, 2);

            putBools(ref cyb, EnableClaster, opVal, centerPanelSection, Claster);

            Vector3 centerCube = respawnPrefab.transform.position;

            for (int i = 0; i < cyb.transform.childCount; i++)
            {
                // Color wqww = panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;

                // cyb.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(wqww.r, wqww.g, wqww.b, wqww.a);
                cyb.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                    = getColor(centerCube, cyb.transform.GetChild(i).gameObject.transform.position, maxDist);
            }

            return cyb;

        }

        /// <summary>
        /// Функция которая помещает шары в обьект 
        /// </summary>
        /// <param name="cyb">обьект в который будут помещены точки</param>
        /// <param name="EnableClaster">true - показать все точки, false - скрыть точки вне кластера</param>
        /// <param name="opVal">Прозрачность секции</param>
        /// <param name="centerPanelSection">Позиция секции</param>
        /// <param name="Claster">Обьект в который нужно вписать шары</param>
        public static void putBools(ref GameObject cyb, bool EnableClaster, float opVal, Vector3 centerPanelSection, GameObject Claster, int side = 0)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    Vector3 Dot = new Vector3();
                    switch (side)
                    {
                        case 0:
                            {
                                Dot = new Vector3(
                     centerPanelSection.x,
                     x + centerPanelSection.y - (50 * 0.5f),
                     y + centerPanelSection.z - (50 * 0.5f));
                               
                            }
                            break;
                        case 1:
                            {
                                Dot = new Vector3(
                       x + centerPanelSection.x - (50 * 0.5f),
                       y + centerPanelSection.y - (50 * 0.5f),
                       centerPanelSection.z);

                                
                            }
                            break;
                        case 2:
                            {
                                Dot = new Vector3(
                     x + centerPanelSection.x - (50 * 0.5f),
                     centerPanelSection.y,
                     y + centerPanelSection.z -  (50 * 0.5f));
                            }
                            break;
                        default:
                            break;
                    }

                    if (!EnableClaster)
                    {
                        if (!Claster.GetComponent<Renderer>().bounds.Contains(Dot))
                            continue;
                    }

                    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    Rigidbody cylinderRigidbody = cylinder.AddComponent<Rigidbody>();
                    cylinderRigidbody.isKinematic = true;
                    cylinderRigidbody.useGravity = false;
                    //cylinderRigidbody.mass = 1;


                    cylinder.transform.position = Dot;

                    //(cylinder.GetComponent<Collider>() as SphereCollider).radius = 100f;
                    cylinder.GetComponent<Renderer>().material.color = new Color(1, 0, 0, opVal);
                    cylinder.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");


                    // cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, cylinder.transform.localScale.y, 0.1f);

                    cylinder.transform.parent = cyb.transform;
                    //cylinder.transform.parent = panelSection.transform;
                    //cylinder.transform.localScale = new Vector3(1, 1, 1);

                }
            }
        }

        /// <summary>
        /// Функция получения цвета для точки на основании отдолённости от цвета 
        /// </summary>
        /// <param name="centerCube">Координаты центра</param>
        /// <param name="positionShare">Координаты точки</param>
        /// <param name="maxDist">Максимальная дистанция</param>
        /// <returns></returns>
        public static Color getColor(Vector3 centerCube, Vector3 positionShare, float maxDist)
        {


            float dist = maxDist - Vector3.Distance(centerCube, positionShare);

            dist = dist < 0 ? 0 : dist;

            float percentRED = dist / (maxDist / 100);
            float www = (100 - percentRED) / 100;

            Color result = new Color(1, www, www, 1);

            return result;
        }
    }

}
