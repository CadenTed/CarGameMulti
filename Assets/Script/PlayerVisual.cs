using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    [SerializeField] private List<MeshRenderer> bodyMeshRenderers;
    [SerializeField] private List<MeshRenderer> plowMeshRenderers;

    private Material plowMat;
    private Material bodyMat;

    private void Awake()
    {
        plowMat = new Material(plowMeshRenderers[0].material);
        bodyMat = new Material(bodyMeshRenderers[0].material);

        foreach (MeshRenderer bodyMesh in bodyMeshRenderers)
        {
            bodyMesh.material = bodyMat;
        }

        foreach (MeshRenderer plowMesh in plowMeshRenderers)
        {
            plowMesh.material = plowMat;
        }
    }

    public void SetBodyColor(Color color)
    {
        bodyMat.color = color;
    }

    public void SetPlowColor(Color color)
    {
        plowMat.color = color;
    }

}
