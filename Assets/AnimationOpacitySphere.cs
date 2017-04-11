using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets
{
    class AnimationOpacitySphere : MonoBehaviour
    {
        bool plusMinus = false;
        void Update()
        {

            Color oldColor = this.gameObject.GetComponent<Renderer>().material.color;

            //if (oldColor.a <= 0 || oldColor.a >=1)
            //{
            //    plusMinus = !plusMinus;
            //}
            this.gameObject.GetComponent<Renderer>().material.color 
               // = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - 0.1f);
               = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - (plusMinus ? -0.1f : 0.1f));

        }

    }
}
