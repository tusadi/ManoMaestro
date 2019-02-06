using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoUtils : MonoBehaviour
{
    #region Singleton
    private static ManoUtils instance;

    public static ManoUtils Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    private Vector3 correction_ratio = Vector3.one;

    protected void Awake()
    {
        if (instance == null)
            instance = this;
        if (!cam)
            cam = Camera.main;
    }

    [SerializeField]
    private Camera cam;

    /// <summary>
    /// Calculates the new position in relation to the main camera.
    /// </summary>
    /// <param name="Point">Requires a Vector3 point</param>
    /// <param name="depth">Requires the float value of depth</param>
    /// <returns></returns>
    internal Vector3 CalculateNewPosition(Vector3 Point, float depth)
    {

        Vector3 correct_point = Point - Vector3.one * 0.5f;
        correct_point.Scale(correction_ratio);
        correct_point = correct_point + Vector3.one * 0.5f;
        correct_point = new Vector3(Mathf.Clamp(correct_point.x, 0, 1), Mathf.Clamp(correct_point.y, 0, 1), Mathf.Clamp(correct_point.z, 0, 1));
        return cam.ViewportToWorldPoint(correct_point + Vector3.forward * depth);
    }

    /// <summary>
    /// Adjust the transform in the received mesh renderer to fit the screen without stretching
    /// </summary>
    /// <param name="mesh_renderer"></param>
    /// 
    internal void AjustBorders(MeshRenderer mesh_renderer, Camera cam, bool change_correction_ratio)
    {
        float ratio = (float)ManomotionManager.Instance.Width / ManomotionManager.Instance.Height;

        mesh_renderer.transform.localScale = new Vector3(ratio, 1, 1);
        Bounds b = mesh_renderer.bounds;

        Vector3 v3ViewPort = new Vector3(0, 0, mesh_renderer.transform.localPosition.z);
        Vector3 v3BottomLeft = cam.ViewportToWorldPoint(v3ViewPort);
        v3ViewPort.Set(1, 1, mesh_renderer.transform.localPosition.z);
        Vector3 v3TopRight = cam.ViewportToWorldPoint(v3ViewPort);
        Vector3 v1 = (v3TopRight - v3BottomLeft);
        Vector3 v2 = (b.max - b.min);
        float size;

        if ((ManomotionManager.Instance.Manomotion_Session.orientation == SupportedOrientation.PORTRAIT || ManomotionManager.Instance.Manomotion_Session.orientation == SupportedOrientation.PORTRAIT_INVERTED))
        {
            size = v1.y / v2.y;
        }
        else
        {
            size = v1.x / v2.x;
        }
        mesh_renderer.transform.localScale = new Vector3(size * ratio, size, 1);
        if (change_correction_ratio)
        {
            if (ManomotionManager.Instance.Manomotion_Session.orientation == SupportedOrientation.PORTRAIT || ManomotionManager.Instance.Manomotion_Session.orientation == SupportedOrientation.PORTRAIT_INVERTED)
            {
                float correction_value = (mesh_renderer.bounds.max - mesh_renderer.bounds.min).x / (v1.x);
                if (correction_value == 0 || correction_value == float.NaN)
                    correction_value = .1f;
                correction_ratio = new Vector3(correction_value, 1, 0);
            }
            else
            {
                float correction_value = (mesh_renderer.bounds.max - mesh_renderer.bounds.min).y / (v1.y);
                if (correction_value == 0 || correction_value == float.NaN)
                    correction_value = .1f;
                correction_ratio = new Vector3(1, correction_value, 0);
            }

        }
    }

    /// <summary>
    /// Retrieve the absolute values of a Vector3
    /// </summary>
    /// <returns>The abs.</returns>
    /// <param name="vector">Requires a Vector3 value.</param>
    Vector3 VectorAbs(Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
}
