using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastOnVisible : MonoBehaviour
{
    private int UID = 0;
    private bool isActive = false;

    [SerializeField] private List<Transform> hitTransforms = new List<Transform>();

    private List<Vector3> tempVertice = new List<Vector3>();
    private Vector3[] uniqueVertice = null;

    private Transform target = null;
    private Enemy enemy = null;
    private MeshFilter meshFilter = null;
    private RaycastOnVisible[] meshFilterVertices = null;

    #region Monobehaviour
    Camera cam = null;

    private void Awake()
    {

        meshFilter = GetComponent<MeshFilter>();

        for (int i = 0; i < meshFilter.mesh.vertexCount; i++)
        {
            if (!tempVertice.Contains(meshFilter.mesh.vertices[i]))
            {
                tempVertice.Add(meshFilter.mesh.vertices[i]);
            }
        }

        uniqueVertice = tempVertice.ToArray();
    }

    private void Start()
    {        
        target = EntityManager.Instance.GetMainCamera();
        cam = target.GetComponent<Camera>();
    }

    private void Update()
    {
        if(cam.WorldToScreenPoint(transform.position).x/cam.pixelWidth < 0 || cam.WorldToScreenPoint(transform.position).x / cam.pixelWidth > 1 ||
            cam.WorldToScreenPoint(transform.position).y / cam.pixelWidth < 0 || cam.WorldToScreenPoint(transform.position).y / cam.pixelWidth > 1 ||
            cam.WorldToScreenPoint(transform.position).z < 0)
        {
            hitTransforms.Clear();
            isActive = true;
            enemy.UpdateActiveStatus(UID, isActive);
        }
        else
        {
            CheckActiveStatusMeshRenderer();

        }
    }

    //private void OnBecameVisible()
    //{

    //}

    //private void OnWillRenderObject()
    //{
    //    CheckActiveStatusMeshRenderer();
    //}

    //private void OnBecameInvisible()
    //{
    //    hitTransforms.Clear();
    //    isActive = true;
    //    enemy.UpdateStatus(UID, isActive);
    //}

    #endregion

    #region Custom Function

    private void CheckActiveStatusMeshRenderer()
    {
        hitTransforms.Clear();

        for (int i = 0; i < uniqueVertice.Length; i++)
        {
            RaycastHit raycastHit;

            Physics.Raycast(transform.TransformPoint(uniqueVertice[i]), target.position - transform.TransformPoint(uniqueVertice[i]), out raycastHit, 100f);

            if (enemy.IsDebug())
            {
                if (raycastHit.transform != null)
                {
                    Debug.DrawLine(transform.TransformPoint(uniqueVertice[i]), raycastHit.point);
                }
                else
                {
                    Debug.DrawLine(transform.TransformPoint(uniqueVertice[i]), target.position);
                }
            }

            hitTransforms.Add(raycastHit.transform);

            if (raycastHit.transform == target.root)
            {
                isActive = false;
                enemy.UpdateActiveStatus(UID, isActive);
                break;
            }
            else
            {
                isActive = true;
                enemy.UpdateActiveStatus(UID, isActive);
            }
        }
    }

    public void SetupMeshFilterVertice(int pUID, Enemy pEnemy, RaycastOnVisible[] pMeshFilterVertice)
    {
        UID = pUID;
        enemy = pEnemy;
        meshFilterVertices = pMeshFilterVertice;
    }

    #endregion
}