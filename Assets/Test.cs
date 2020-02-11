using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject cube;
    private GameObject sphere;
    private GameObject c;

    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.Find("Cube");
        sphere = GameObject.Find("Cube/Sphere");
        c = GameObject.Find("Cylinder");
    }

    void Update()
    {
        Matrix4x4 world = cube.transform.localToWorldMatrix;

        Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), new Color(0, 255, 0), 500f);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 1, 0), new Color(0, 255, 0), 500f);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 0, 1), new Color(0, 255, 0), 500f);



        if (Input.GetKeyUp(KeyCode.X))
        {
            Vector3 start = world.MultiplyPoint3x4(cube.transform.localPosition);
            Vector3 end = world.MultiplyPoint3x4(cube.transform.localPosition + new Vector3(5, 0, 0));
            Debug.Log("Start: " + start);
            Debug.Log("end: " + end);

            DrawLine(start, end);

            RaycastHit hit;

            Vector3 direction = (end - start).normalized;

            Debug.Log(direction);


            if (Physics.Raycast(start, direction, out hit))
            {
                Debug.Log("HIIIIIT");
                Debug.Log(hit);


            }
        }
    }



    void DrawLine(Vector3 start, Vector3 end, float lineWidth = 0.1f)
    {
        GameObject myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        Material material = new Material(Shader.Find("Transparent/Diffuse"));
        material.color = Color.red;
        lr.material = material;

        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(myLine, Time.deltaTime);
    }
}
