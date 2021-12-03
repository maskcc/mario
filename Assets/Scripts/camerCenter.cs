using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
create by keefor On 20200106
*/
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class CameraPlay : MonoBehaviour
{
    [HideInInspector]
    public WebCamTexture camtex;
    private Material mat;
    private Camera cam;

    private static CameraPlay inst;
    public static CameraPlay Inst
    {
        get
        {
            if (inst == null)
            {
                var pp = GameObject.CreatePrimitive(PrimitiveType.Quad);
                pp.name = "WebCamera";
                Destroy(pp.GetComponent<Collider>());
                inst = pp.AddComponent<CameraPlay>();
            }
            return inst;
        }
    }


    void Awake()
    {
        inst = this;

        UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
        WebCamDevice[] cd = WebCamTexture.devices;
        camtex = new WebCamTexture(cd[0].name, 480, 320, 15);//考虑到性能问题这里选择了小尺寸
        var render = this.GetComponent<Renderer>();
        render.material = mat = new Material(Shader.Find("Unlit/Texture"));
        cam = Camera.main;
        if (cam == null) cam = Camera.allCameras[0];
        transform.SetParent(cam.transform);
        transform.localPosition = new Vector3(0, 0, cam.farClipPlane);
    }

    void OnEnable()
    {
        camtex.Play();
        mat.mainTexture = camtex;
        var lt = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.farClipPlane));
        var rb = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.farClipPlane));
        Debug.Log(lt + ":" + rb);
        var sc = new Vector3(Mathf.Abs(rb.x - lt.x), Mathf.Abs(lt.y - rb.y), 1);
        var ratio = sc.x / sc.y;
        var scratio = (float)camtex.width / camtex.height;
        if (ratio > scratio)
        {
            sc.y = sc.x / scratio;
        }
        else
        {
            sc.x = sc.y * scratio;
        }
        transform.localScale = sc;
    }

    void OnDisable()
    {
        camtex.Stop();
    }

}

