using System.Collections;
using Lean.Pool;
using UnityEngine;

public class AirplaneMovement : MonoBehaviour
{
    [SerializeField] private BallShooter _ballShooter;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB; 
    [SerializeField] private float speed = 10f;

    private bool movingToB = false; 
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    private void Update()
    {
        Transform target = movingToB ? pointB : pointA;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            transform.Rotate(0f, 180f, 0f);
            movingToB = !movingToB;
        }
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            _ballShooter.AddBullet(2);
            
            StartCoroutine(Despawn());
        }
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1f);
        LeanPool.Despawn(gameObject);
        audioManager.Stop(SoundType.AviaFly);
    }
}