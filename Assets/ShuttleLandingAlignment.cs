using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleLandingAlignment : MonoBehaviour
{
    private GameObject quad;
    private GameObject shuttle;
    private GameObject landingLane;
  
    // Start is called before the first frame update
    void Start()
    {
        shuttle = GameObject.Find("ShuttleTarget");
        quad = GameObject.Find("ShuttleTarget/Quad");
        landingLane = GameObject.Find("LandingLaneTarget");
    }

    // Update is called once per frame
    void Update()
    {
        float right_o = Vector3.Dot(shuttle.transform.right, landingLane.transform.right);
        float up_o = Vector3.Dot(shuttle.transform.up, landingLane.transform.up);
        float forward_o = Vector3.Dot(shuttle.transform.forward, landingLane.transform.forward);
        float align = right_o + up_o + forward_o;

        if (align <= 3 && align > 2.9)
        {
            quad.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
        else if (align <= 2.9 && align > 2)
        {
            quad.GetComponent<Renderer>().material.color = new Color(125, 125, 0);
        }
        else {
            quad.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }

    }



}
