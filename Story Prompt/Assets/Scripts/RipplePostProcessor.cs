using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;
    public float transiAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;

    void Update()
    {
        this.RippleMaterial.SetFloat("_Amount",  this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }

    public void RippleEffect()
    {
        this.Amount = this.MaxAmount;
        Vector3 pos = Input.mousePosition;
        this.RippleMaterial.SetFloat("_CenterX", pos.x);
        this.RippleMaterial.SetFloat("_CenterY", pos.y);
    }

    public void transitionRippleEffect(float multiplier)
    {
        this.Amount =   transiAmount * multiplier;
        //Vector3 pos = gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector2 (0.5f, 0.5f));
        this.RippleMaterial.SetFloat("_CenterX", Screen.width / 2);
        this.RippleMaterial.SetFloat("_CenterY", Screen.height / 2);
    }
}
