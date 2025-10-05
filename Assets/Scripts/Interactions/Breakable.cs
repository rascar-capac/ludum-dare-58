using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Breakable : MonoBehaviour, ITreasure
{
    public Rigidbody2D Rigidbody;
    public Vector2 MinMaxDamagingVelocityMagnitude;
    public SpriteRenderer SpriteRenderer;

    public GameObject Object => gameObject;
    public List<GameObject> Content;
    public float Resistance01;
    public bool IsBroken;

    public static event Action<Breakable> OnBroken;

    private void Awake()
    {
        TreasureProximityDetector.Instance.RegisterTreasure(this);
        SetResistance(1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsBroken)
        {
            return;
        }

        float damage = math.remap(MinMaxDamagingVelocityMagnitude.x, MinMaxDamagingVelocityMagnitude.y, 0f, 1f, collision.relativeVelocity.magnitude);
        damage = Mathf.Clamp01(damage);

        SetResistance(Mathf.Clamp01(Resistance01 - damage));
    }

    public void Break()
    {
        foreach (GameObject content in Content)
        {
            Instantiate(content, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward));
        }

        TreasureProximityDetector.Instance.UnregisterTreasure(this);
        Destroy(Object);
        IsBroken = true;
        OnBroken?.Invoke(this);
    }

    public void SetResistance(float value)
    {
        Resistance01 = value;
        SpriteRenderer.material.SetFloat("_Intensity", 1f - Resistance01);

        if (Resistance01 == 0f)
        {
            Break();
        }
    }
}
