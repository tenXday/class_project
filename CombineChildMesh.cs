using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineChildMesh : MonoBehaviour
{
    [SerializeField] Material[] TestMatirial;
    void Start()
    {

        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        Material[] materials = new Material[1];
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            materials[0] = meshRenderers[i].sharedMaterial;
            Destroy(meshFilters[i].gameObject);
            //meshFilters[i].gameObject.SetActive(false);
        }
        gameObject.AddComponent<MeshFilter>().mesh = new Mesh();
        gameObject.AddComponent<MeshCollider>();
        gameObject.AddComponent<MeshRenderer>();

        gameObject.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true);
        gameObject.GetComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;
        gameObject.GetComponent<MeshRenderer>().sharedMaterials = TestMatirial;
        transform.gameObject.SetActive(true);

        transform.rotation = oldRot;
        transform.position = oldPos;
        transform.localScale = new Vector3(1, 1, 1);
    }
}
