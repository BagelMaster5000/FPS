using System.Collections;
using UnityEngine;

// Detaches particle system from all parents and destroys it automatically when particles stop
[RequireComponent(typeof(ParticleSystem))]
public class ParticlesDetachAndDestroy : MonoBehaviour
{
    ParticleSystem particles;

    // Activates particles
    public void Activate()
    {
        transform.localScale = Vector3.one;
        this.transform.parent = null;
        particles = GetComponent<ParticleSystem>();
        particles.Play();
        StartCoroutine(DestroyWhenDone());
    }

    // Destroys particles when done
    IEnumerator DestroyWhenDone()
    {
        while (particles.isPlaying)
            yield return null;
        Destroy(this.gameObject);
    }
}
