using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using System.Collections.Generic;
using Helpers;
using Assets;

public class DropScript : MonoBehaviour
{
     
    Dropdown Drop;  //выбор ранее созданной секции 
    Dropdown SideSection;  //выбор стороны поворота секции
    GameObject panelSection; // секция которая двигается 
    Toggle EnableClaster;
    GameObject respawnPrefab;
    GameObject Claster;
    GameObject AnimationPanel; // секция для анимации
    Vector3 centerCube;
    public float maxDist;
    Scrollbar ScrollbarOpacity;
    Toggle ModeView;
    //получить все точки для анимации
    List<SectionOfShape> Sections;
    SectionOfShape MovingSections;

    List<GameObject> listOfSphere = new List<GameObject>();
    List<Vector3> listOfSphereVector3 = new List<Vector3>();
    Timer timer;
    int counterStop = 0;

    //проверка изменения цвета
    public static void Count(object obj, float param = 0f)
    {

        //Color ghjjuyg = ((GameObject)obj).gameObject.GetComponent<Renderer>().material.color;
        //((GameObject)obj).gameObject.GetComponent<Renderer>().material.color = new Color(1, ghjjuyg.g, ghjjuyg.b, param);

       
        //((GameObject)obj).gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, param);
    }


    void Start()
    {
        respawnPrefab = GameObject.FindWithTag("CenterAquo");

        //respawnPrefab.AddComponent<GlobalFields>();
        //получение глобальной перемонной. список всех срезов
        Sections = respawnPrefab.GetComponent<GlobalFields>().Sections;
        
 
        SideSection = GameObject.FindWithTag("SideSection").GetComponent<Dropdown>();
        Drop = GameObject.FindWithTag("Drop").GetComponent<Dropdown>();
        EnableClaster = GameObject.FindWithTag("EnableClaster").GetComponent<Toggle>();
        panelSection = GameObject.FindWithTag("panelSection");
        Claster = GameObject.FindWithTag("Claster");
        ScrollbarOpacity = GameObject.FindWithTag("ScrollbarOpacity").GetComponent<Scrollbar>();
        AnimationPanel = GameObject.FindWithTag("AnimationPanel");
        ModeView = GameObject.FindWithTag("ModeView").GetComponent<Toggle>();

        centerCube = respawnPrefab.GetComponent<Renderer>().bounds.center;

        Vector3 VMaxDist = new Vector3(
            respawnPrefab.GetComponent<Renderer>().bounds.center.x + respawnPrefab.GetComponent<Renderer>().transform.localScale.x * 0.5f,
            respawnPrefab.GetComponent<Renderer>().bounds.center.y + respawnPrefab.GetComponent<Renderer>().transform.localScale.y * 0.5f,
            respawnPrefab.GetComponent<Renderer>().bounds.center.z + respawnPrefab.GetComponent<Renderer>().transform.localScale.z * 0.5f
            );

        maxDist = Vector3.Distance(VMaxDist, centerCube);

        SideSection.onValueChanged.AddListener(delegate { SideSectionChange(); });
        Drop.onValueChanged.AddListener(delegate { myDropdownValueChangedHandler(); });
        EnableClaster.onValueChanged.AddListener(delegate { ToggleEnableClaster(); });
        ModeView.onValueChanged.AddListener(delegate { changeMode(); });
        ScrollbarOpacity.onValueChanged.AddListener(delegate { ChangeOpasitySelectedSection(); });


        /////ТЕСТ
        //AddNewOption();
        //ModeView.isOn = false;
        //changeMode();
        //Animation();
    }

  

    /// <summary>
    /// Изменение режима просмотра
    /// </summary>
    void changeMode()
    {


        //if (ModeView.isOn)
        //{
        //    for (int i = 0; i < panelSection.transform.childCount; i++)
        //    {
        //        Color oldCol = panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;

        //        panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
        //            = new Color(oldCol.r, oldCol.g, oldCol.b, 1);
        //    }

        //    Color calCol = sections[Drop.value].GetComponent<Renderer>().material.color;
        //    sections[Drop.value].GetComponent<Renderer>().material.color = new Color(calCol.r, calCol.g, calCol.b, 1);
        //}
        //else
        //{
        //    for (int i = 0; i < panelSection.transform.childCount; i++)
        //    {
        //        Color oldCol = panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;

        //        panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color
        //            = new Color(oldCol.r, oldCol.g, oldCol.b, 0);
        //    }

        //    foreach (GameObject item in sections)
        //    {
        //        Color oldCol = item.GetComponent<Renderer>().material.color;
        //        item.GetComponent<Renderer>().material.color = new Color(oldCol.r, oldCol.g, oldCol.b, 0);
        //    }
        //    //panelSection
        //}
    }
    /// <summary>
    /// Изменение прозрачности выделенной секции
    /// </summary>
    void ChangeOpasitySelectedSection()
    {
        if (Sections.Count != 0)
        {
            Sections[Drop.value].Opacity = ScrollbarOpacity.value;
        }
    }
    /// <summary>
    /// Прересоздать секции с учётом поподания точек в кластер
    /// </summary>
    private void ToggleEnableClaster()
    {
        // добавить условие что только если включен режим пузырей
        if (true)
        {
            foreach (SectionOfShapeBubble item in Sections)
            {
                //positionSections.Add(item.transform.position);
                Destroy(item.Section);
                item.EnableClaster = EnableClaster.isOn;
                item.Create();
                item.CreateBubbles();
            }
        }
    }

    private void SideSectionChange() 
    {

    }
    

    /// <summary>
    /// Изменение цвета секции которая выделена с учётом режима просмотра
    /// </summary>
    private void myDropdownValueChangedHandler()
    {
        ////полная прозрачность для не выделенных
        //foreach (var item in sections)
        //{
        //    item.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        //}

        ////режима просмотра
        //if (ModeView.isOn)
        //{
        //    sections[Drop.value].GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.8f);
        //}
        //// sections.RemoveAt(Drop.value);


        //// Debug.Log("selected: " + target.value);
    }

    /// <summary>
    /// Добавление новой секции
    /// </summary>
    public void AddNewOption()
    {

        //сохранение ссылки на обьект при первом добовлении
        if (MovingSections == null)
        {
            MovingSections = respawnPrefab.GetComponent<GlobalFields>().MovingSection;
        }

        Sections.Add(new SectionOfShapeBubble(respawnPrefab,
                Claster,
                MovingSections.CenterPanelSection,
                //panelSection.GetComponent<Renderer>().bounds.center, 
                SideSection.value, 
                EnableClaster.isOn, 
                ScrollbarOpacity.value));

        Drop.options.Add(new Dropdown.OptionData("new text"));
    }
    /// <summary>
    /// Удаление секции 
    /// </summary>
    public void DeleteOption()
    {
        Destroy(Sections[Drop.value].Section);
        Sections.RemoveAt(Drop.value);
    }

    public void Animation() 
    {
       // //Time.deltaTime

       //// Update();
       // получить все точки
        //listOfSphere = new List<GameObject>();

        //foreach (GameObject item in sections)
        //{
        //    for (int i = 0; i < item.transform.childCount; i++)
        //    {
        //        //panelSection.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color

        //        listOfSphere.Add(item.transform.GetChild(i).gameObject);
        //        listOfSphereVector3.Add(item.transform.GetChild(i).gameObject.transform.localPosition);
        //    }
        //}



       // //по ка что выключим так как оно и так в отсортированном виде listOfSphere[j] > listOfSphere[j +1]

       // ////сортировка по Х 
       // //for (int i = 0; i < listOfSphere.Count - 1; i++)
       // //{
       // //    for (int j = 0; j < listOfSphere.Count - 1; j++)
       // //    {
       // //        if (listOfSphere[j].transform.localPosition.x > listOfSphere[j + 1].transform.localPosition.x)
       // //        {
       // //            GameObject buf = listOfSphere[j];
       // //            listOfSphere[j] = listOfSphere[j + 1];
       // //            listOfSphere[j + 1] = buf;
       // //        }
       // //    }
       // //}


       //int state = 2;

       // foreach (GameObject item in listOfSphere)
       // {
       //     Count(item);
       // }



       // TimerCallback tm = new TimerCallback(CheckStatus);
       // timer = new Timer(tm, state, 0, 1000);



       // foreach (GameObject item in listOfSphere)
       // {


       //     //Count(item);


       //     // item.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
       //     //GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
       // }




    }

    public void CheckStatus(object stateInfo)
    {


        if (counterStop < 10)
        {
            float oneStep = 0.5f - counterStop * 0.1f;//0.5f; //5 / 10;

            //oneStep -= counterStop * 0.1f; 
            List<GameObject> RangeList = new List<GameObject>();

            //foreach (GameObject item in listOfSphere)
            for (int i = 0; i < listOfSphereVector3.Count; i++)            
            {
                if (listOfSphereVector3[i].x <= oneStep + 0.1f && listOfSphereVector3[i].x >= oneStep)
                {
                    RangeList.Add(listOfSphere[i]);
                }
            }

            foreach (GameObject item in RangeList)
            {
                Count(item, 1f);
            }

            counterStop++;
          
            Debug.Log(counterStop);
        }
    }
    // Update is called once per frame

    //public float titi = 0.25f;
    public float tyty = 0;

    


    void Update()
    {
        //if (titi > 0)
        //{
        //    titi -= Time.deltaTime;
        //}
        //if (titi <= 0)
        //{
        //    titi -= Time.deltaTime;
        //}


        //if (tyty > 0)
        //{
        //    tyty -= Time.deltaTime;
        //}
        //if (tyty <= 0)
        //{

        //    if (counterStop < 10)
        //    {
        //        float oneStep = 0.5f - counterStop * 0.1f;//0.5f; //5 / 10;

        //        //oneStep -= counterStop * 0.1f; 
        //        List<GameObject> RangeList = new List<GameObject>();

        //        foreach (GameObject item in listOfSphere)
        //        {
        //            if (item.transform.localPosition.x <= oneStep + 0.1f && item.transform.localPosition.x >= oneStep)
        //            {
        //                RangeList.Add(item);
        //            }
        //        }

        //        foreach (GameObject item in RangeList)
        //        {
        //            Count(item, 1f);
        //        }

        //        counterStop++;
        //        tyty = 0.0001f;
        //        Debug.Log(counterStop);
        //    }
            
        //}


          //  Debug.Log(Time.deltaTime);


        




        //counterStop++;
        //if (counterStop >= 10)
        //{
        //    timer.Change(System.Threading.Timeout.Infinite, 0);
        //    counterStop = 0;
        //}
    }
}
