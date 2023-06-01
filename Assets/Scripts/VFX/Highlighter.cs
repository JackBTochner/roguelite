using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Highlighter : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private Color targetColor = new Color(255, 17, 85);
    [SerializeField]
    private ParticleSystem particles;

    private List<Color> originalEmissiveColors = new List<Color>();
    private List<Material> materials = new List<Material>();

    [Header("Listening On")]
    public RuntimeSetBase<Highlighter> highlighters;

    public PlayerProjectileReceiver projectileReceiver;
    Tweener tweener;

    private void OnEnable()
    {
        if(highlighters)
            highlighters.Add(this);
    }

    private void OnDisable()
    {
        if(highlighters)
            highlighters.Remove(this);
    }
    
    private void Awake()
    {
        projectileReceiver = GetComponent<PlayerProjectileReceiver>();
        foreach (var renderer in renderers)
        {
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                renderer.materials[i].EnableKeyword("_EMISSION");
                originalEmissiveColors.Add(renderer.materials[i].GetColor("_EmissionColor"));
            }
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val)
    {
        if (projectileReceiver)
        { 
            if (projectileReceiver.projectileCount <= 0)
                return;
        }
        if (val)
        {
            foreach (var material in materials)
            {
                tweener = material.DOColor(targetColor, "_EmissionColor", 0.5f).SetLoops(-1, LoopType.Yoyo);
            }
            if(particles)
                particles.Play();
        } 
        else
        {
            for (int i = 0; i < materials.Count; i++)
            {
                tweener.Kill();
                materials[i].DOColor(originalEmissiveColors[i], "_EmissionColor", 0.5f);
            }
            if(particles)
                    particles.Stop();
        }
    }
}

