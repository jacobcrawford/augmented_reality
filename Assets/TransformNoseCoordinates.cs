using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformNoseCoordinates : MonoBehaviour
{
    private GameObject nose;
    private GameObject shuttle;
    private GameObject earth;
    private Vector3 localEarthPos;
    // Start is called before the first frame update
    void Start()
    {
        nose = GameObject.Find("ShuttleTarget/Nose");
        shuttle = GameObject.Find("ShuttleTarget");
        earth = GameObject.Find("EarthTarget");
    }

    // Update is called once per frame
    void Update()
    {
 
        // 2 Get Earth matrix
        Matrix4x4 earthMatrix = earth.transform.worldToLocalMatrix;

        // 3 Get Shuttle matrix
        Matrix4x4 shuttleMatrix = shuttle.transform.localToWorldMatrix;

        // 4 Transform nose coordinates
        localEarthPos = shuttleMatrix.MultiplyPoint3x4(nose.transform.localPosition);
        Debug.Log(nose.transform.localPosition);
        //localEarthPos = earthMatrix.MultiplyPoint3x4(localEarthPos);



    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(10, 10, 500, 100), localEarthPos.ToString() );
    }



    /**************************************************************************/
    /************ CONVENIENCE FUNCTIONS FOR AFFINE TRANSFORMATIONS ************/
    /**************************************************************************/

    public static Matrix4x4 T(float x, float y, float z)
    {
        Matrix4x4 m = new Matrix4x4();

        m.SetRow(0, new Vector4(1, 0, 0, x));
        m.SetRow(1, new Vector4(0, 1, 0, y));
        m.SetRow(2, new Vector4(0, 0, 1, z));
        m.SetRow(3, new Vector4(0, 0, 0, 1));

        return m;
    }

    public static Matrix4x4 Rx(float a)
    {
        Matrix4x4 m = new Matrix4x4();

        m.SetRow(0, new Vector4(1, 0, 0, 0));
        m.SetRow(1, new Vector4(0, Mathf.Cos(a), -Mathf.Sin(a), 0));
        m.SetRow(2, new Vector4(0, Mathf.Sin(a), Mathf.Cos(a), 0));
        m.SetRow(3, new Vector4(0, 0, 0, 1));

        return m;
    }

    public static Matrix4x4 Ry(float a)
    {
        Matrix4x4 m = new Matrix4x4();

        m.SetRow(0, new Vector4(Mathf.Cos(a), 0, Mathf.Sin(a), 0));
        m.SetRow(1, new Vector4(0, 1, 0, 0));
        m.SetRow(2, new Vector4(-Mathf.Sin(a), 0, Mathf.Cos(a), 0));
        m.SetRow(3, new Vector4(0, 0, 0, 1));

        return m;
    }

    public static Matrix4x4 Rz(float a)
    {
        Matrix4x4 m = new Matrix4x4();

        m.SetRow(0, new Vector4(Mathf.Cos(a), -Mathf.Sin(a), 0, 0));
        m.SetRow(1, new Vector4(Mathf.Sin(a), Mathf.Cos(a), 0, 0));
        m.SetRow(2, new Vector4(0, 0, 1, 0));
        m.SetRow(3, new Vector4(0, 0, 0, 1));

        return m;
    }


    public static Matrix4x4 S(float sx, float sy, float sz)
    {
        Matrix4x4 m = new Matrix4x4();

        m.SetRow(0, new Vector4(sx, 0, 0, 0));
        m.SetRow(1, new Vector4(0, sy, 0, 0));
        m.SetRow(2, new Vector4(0, 0, sz, 0));
        m.SetRow(3, new Vector4(0, 0, 0, 1));

        return m;
    }
}
