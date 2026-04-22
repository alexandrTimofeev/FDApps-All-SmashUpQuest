using System;
using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float currentScale;
    [SerializeField] private float fireScale;
    [SerializeField] private float scaleDownDuration;
    [SerializeField] private BallShooter ballShooter;
    [SerializeField] private GameObject hitVFXPref;
    public Rigidbody rb;
    public MeshRenderer meshRenderer;

    public void OnSpawn()
    {
        ballShooter = FindObjectOfType<BallShooter>();
        rb.isKinematic = true;
        meshRenderer.enabled = false;
    }

    public void OnFire()
    {
        rb.isKinematic = false;
        meshRenderer.enabled = true;
        transform.DOScale(currentScale, scaleDownDuration).From(fireScale);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            ballShooter.ballHitTargetAction?.Invoke();
        }

        if (rb.linearVelocity.magnitude > 0.5f)
            Instantiate(hitVFXPref, transform.position, transform.transform.rotation);
    }
}