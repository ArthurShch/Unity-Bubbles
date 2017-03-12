using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Collections.Generic;
using Helpers;

public class DropScript : MonoBehaviour
{

    List<GameObject> sections;
    List<float> listOpacity;
    Dropdown Drop;
    GameObject panelSection;
    Toggle EnableClaster;
    GameObject respawnPrefab;
    GameObject Claster;
    Vector3 centerCube;
    float maxDist;
    Scrollbar ScrollbarOpacity;
    Toggle ModeView;

    //public  Drop;
    // Use this for initialization

    //Color getColor(Vector3 positionShare)
    //{


    //    float dist = maxDist - Vector3.Distance(centerCube, positionShare);

    //    dist = dist < 0 ? 0 : dist;

    //    float percentRED = dist / (maxDist / 100);
    //    float www = (100 - percentRED) / 100;

    //    Color result = new Color(1, www, www, 1);

    //    return result;
    //}

    void Start()
    {

        respawnPrefab = GameObject.FindWithTag("CenterAquo");
        Drop = GameObject.FindWithTag("Drop").GetComponent<Dropdown>();
        EnableClaster = GameObject.FindWithTag("EnableClaster").GetComponent<Toggle>();
        panelSection = GameObject.FindWithTag("panelSection");
        Claster = GameObject.FindWithTag("Claster");
        ScrollbarOpacity = GameObject.FindWithTag("ScrollbarOpacity").GetComponent<Scrollbar>();
        ModeView = GameObject.FindWithTag("ModeView").GetComponent<Toggle>();


      

        sections = new List<GameObject>();
        listOpacity = new List<float>();

        centerCube = respawnPrefab.GetComponent<Renderer>().bounds.center;

        Vector3 VMaxDist = new Vector3(
            respawnPrefab.GetComponent<Renderer>().bounds.center.x + respawnPrefab.GetComponent<Renderer>().transform.localScale.x * 0.5f,
            respawnPrefab.GetComponent<Renderer>().bounds.center.y + respawnPrefab.GetComponent<Renderer>().transform.localScale.y * 0.5f,
            respawnPrefab.GetComponent<Renderer>().bounds.center.z + respawnPrefab.GetComponent<Renderer>().transform.localScale.z * 0.5f
            );

        maxDist = Vector3.Distance(VMaxDist, centerCube);

        Drop.onValueChanged.AddListener(delegate { myDropdownValueChangedHandler(); });
        EnableClaster.onValueChanged.AddListener(delegate { ToggleEnableClaster(); });
        ModeView.onValueChanged.AddListener(delegate { changeMode(); });
        ScrollbarOpacity.onValueChanged.AddListener(delegate { ChangeOpasitySelectedSection(); });

    }

    // Update is called once per frame
    void Update()
    {

    }

    void changeMode()
    {
        if (ModeView.isOn)
        {
            for (int i = 0; i < panelSection.transform.childCount; i++)
            {
                Color oldCol = panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;

                panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                    = new Color(oldCol.r, oldCol.g, oldCol.b, 1);
            }

            Color calCol = sections[Drop.value].GetComponent<Renderer>().material.color;
            sections[Drop.value].GetComponent<Renderer>().material.color = new Color(calCol.r, calCol.g, calCol.b, 1);
        }
        else
        {
            for (int i = 0; i < panelSection.transform.childCount; i++)
            {
                Color oldCol = panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;

                panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                    = new Color(oldCol.r, oldCol.g, oldCol.b, 0);
            }


            foreach (GameObject item in sections)
            {
                Color oldCol = item.GetComponent<Renderer>().material.color;
                item.GetComponent<Renderer>().material.color = new Color(oldCol.r, oldCol.g, oldCol.b, 0);
            }


            //panelSection
        }
    }
    void ChangeOpasitySelectedSection()
    {
        if (sections.Count != 0)
        {
            int selected = Drop.value;
            float opVal = ScrollbarOpacity.value;
            int count = sections[selected].transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Color oldColor = sections[selected].transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;
                sections[selected].transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
                    = new Color(oldColor.r, oldColor.g, oldColor.b, opVal);
            }
        }



    }

    private void ToggleEnableClaster()
    {
        List<Vector3> nnnn = new List<Vector3>();

        foreach (GameObject item in sections)
        {
            nnnn.Add(item.transform.position);
            Destroy(item);
        }

        sections.Clear();

        for (int i = 0; i < nnnn.Count; i++)
        {
            sections.Add(Helper.createNewBoolsPanel
            (
                respawnPrefab,
                Claster,
                EnableClaster.isOn,
                listOpacity[i],
                maxDist,
                nnnn[i]
            ));
            //sections.Add(kreate(nnnn[i], listOpacity[i]));
        }

    }

    private void myDropdownValueChangedHandler()
    {

        foreach (var item in sections)
        {
            item.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        }

        if (ModeView.isOn)
        {
            sections[Drop.value].GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.8f);
        }


       

        // sections.RemoveAt(Drop.value);


        // Debug.Log("selected: " + target.value);
    }

    //public GameObject kreate(Vector3 cenet, float opVal)
    //{
    //    GameObject cyb = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //    //Vector3 centerPanelSection = panelSection.GetComponent<Renderer>().bounds.center;
    //    Vector3 centerPanelSection = cenet;
    //    cyb.GetComponent<Renderer>().transform.position = centerPanelSection;
    //    // cyb.GetComponent<Renderer>().transform.localScale = panelSection.GetComponent<Renderer>().transform.lossyScale;

    //    cyb.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0);
    //    cyb.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
    //    cyb.transform.localScale = new Vector3(50, 50, 2);

    //    for (int x = 0; x < 50; x++)
    //    {
    //        for (int y = 0; y < 50; y++)
    //        {
    //            Vector3 Dot = new Vector3(
    //                x + centerPanelSection.x - (50 * 0.5f),
    //                y + centerPanelSection.y - (50 * 0.5f),
    //                centerPanelSection.z);

    //            // здесь использование тугле
    //            //(!EnableClaster.isOn) &&

    //            if (!EnableClaster.isOn)
    //            {
    //                if (!Claster.GetComponent<Renderer>().bounds.Contains(Dot))
    //                    continue;
    //            }


    //            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //            cylinder.transform.position = Dot;

    //            //(cylinder.GetComponent<Collider>() as SphereCollider).radius = 100f;
    //            cylinder.GetComponent<Renderer>().material.color = new Color(1, 0, 0, opVal);
    //            cylinder.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");


    //            // cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, cylinder.transform.localScale.y, 0.1f);
    //            cylinder.transform.parent = cyb.transform;
    //            //cylinder.transform.parent = panelSection.transform;
    //            //cylinder.transform.localScale = new Vector3(1, 1, 1);

    //        }
    //    }


    //    for (int i = 0; i < cyb.transform.childCount; i++)
    //    {
    //        // Color wqww = panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;

    //        // cyb.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = new Color(wqww.r, wqww.g, wqww.b, wqww.a);
    //        cyb.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
    //            = getColor(cyb.transform.GetChild(i).gameObject.transform.position);
    //    }

    //    return cyb;

    //}


    public void AddNewOption()
    {
        //ScrollbarOpacity.value;

        sections.Add(Helper.createNewBoolsPanel
            (
                respawnPrefab,
                Claster,
                EnableClaster.isOn,
                ScrollbarOpacity.value,
                maxDist,
                panelSection.GetComponent<Renderer>().bounds.center
            ));

        //sections.Add(kreate(panelSection.GetComponent<Renderer>().bounds.center, ScrollbarOpacity.value));
        listOpacity.Add(ScrollbarOpacity.value);
        //        sections.Add(Instantiate(panelSection));
        Drop.options.Add(new Dropdown.OptionData("new text"));

        // Drop.transform.GetChild

        //получить координаты панели
        //добавить в список контролла и свой список

       // Debug.Log("new text");
    }

    public void DeleteOption()
    {
        Drop.options.RemoveAt(Drop.value);

        Destroy(sections[Drop.value]);
        sections.RemoveAt(Drop.value);
        listOpacity.RemoveAt(Drop.value);
    }
}
