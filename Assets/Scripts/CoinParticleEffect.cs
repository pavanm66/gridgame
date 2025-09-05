using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParticleEffect : MonoBehaviour
{
    public ParticleSystem particleSys;
    private void Awake()
    {
        particleSys = GetComponent<ParticleSystem>();
    }
    public void PlayParticleEffect(Vector3 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        particleSys.Play();
        Invoke("Deactivate", 0.5f);
    }
    void Deactivate()
    {
        particleSys.Stop();
        gameObject.SetActive(false);
    }
}
