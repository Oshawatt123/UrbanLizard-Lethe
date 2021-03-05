using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPostProcess : MonoBehaviour
{
    public Material material;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }

    public void SetVignetteRadius(float radius)
    {
        material.SetFloat("_VRadius", radius);
    }

    private void Start()
    {
        //SetVignetteRadius(0f);
    }
}
