using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalManager : MonoBehaviour
{
    public GameObject coinParticlePrefab;
    public List<GameObject> particleList;

    private void Start()
    {
        for (int i = 0; i <= 5; i++)
        {
            GameObject newParticles = Instantiate(coinParticlePrefab);
            newParticles.SetActive(false);
            newParticles.transform.SetParent(transform);
            particleList.Add(newParticles);
        }
    }
    public void PlayParticleEffect(Vector3 position)
    {
        foreach (GameObject item in particleList)
        {
            if (!item.activeSelf)
            {
                item.GetComponent<CoinParticleEffect>().PlayParticleEffect(position);
                return;
            }
        }
    }
}
