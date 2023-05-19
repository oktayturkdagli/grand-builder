using UnityEngine;

public class ParticleSystemManager : Singleton<ParticleSystemManager>
{
    public ParticleSystem destinationPing;
    
    public void PlayPS(ParticleSystem ps, Vector3 position = default)
    {
        ps.transform.position = position;
        ps.Play();
    }
    
    public void StopPS(ParticleSystem ps)
    {
        ps.Stop();
    }
    
    public void PlayPointerDestinationPS(Vector3 position)
    {
        PlayPS(destinationPing, position);
    }
}