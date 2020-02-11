using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSet : MonoBehaviour
{
    private GameObject falcon;
    public ParticleSystem exp;
    public GameObject cannon1;
    public GameObject cannon2;

    // Start is called before the first frame update
    void Start()
    {
        
        falcon = GameObject.Find("FalconTarget");
        cannon1 = Instantiate(cannon1, transform);
        cannon2 = Instantiate(cannon2, transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Falcon positionn
        Matrix4x4 falconMatrix = Matrix4x4.TRS(falcon.transform.position, falcon.transform.rotation, falcon.transform.lossyScale);

        //canon1 position relative
        Matrix4x4 cannonMatrix1 = falconMatrix
            * S(0.02f, 0.02f, 0.02f)
            * T(2f, 0, 20f);

        //canon2 position relative
        Matrix4x4 cannonMatrix2 = falconMatrix
            * S(0.02f, 0.02f, 0.02f)
            * T(-2f, 0, 20f);

        cannon1.transform.position = cannonMatrix1.GetColumn(3);
        cannon1.transform.rotation = cannonMatrix1.rotation;

        cannon2.transform.position = cannonMatrix2.GetColumn(3);
        cannon2.transform.rotation = cannonMatrix2.rotation;

        if (Input.GetKeyUp(KeyCode.X))
        {
            ShootCanon(cannon1);
            ShootCanon(cannon2);
            ShootCanon(falcon);
        }
    }


    void ShootCanon(GameObject canon)
    {
        Matrix4x4 world = canon.transform.localToWorldMatrix;
        // Define start vector
        Vector3 start = world.MultiplyPoint3x4(canon.transform.localPosition);

        // Shoot out the z axis
        Vector3 end = world.MultiplyPoint3x4(canon.transform.localPosition + new Vector3(0, 0, 5000));
        DrawLine(start, end);

        // The direction is the difference normalized
        Vector3 direction = (end - start).normalized;

        // Destroy or "reset" the expolsion Particle System if there is any
        Destroy(exp);

        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit))
        {
            if (hit.collider != null)
            {
                string name = hit.collider.gameObject.name;

                GameObject hit_object = GameObject.Find(name);
           
                Instantiate(exp, hit_object.transform);

                exp.Play();
            }

        }
    }


    void DrawLine(Vector3 start, Vector3 end, float lineWidth = 0.001f)
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
