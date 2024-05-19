using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraRenderer : MonoBehaviour
{
    private Material material;

    public float darkvision;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/ToCel"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Darkvision", darkvision);
        Graphics.Blit(source, destination, material);
    }
}