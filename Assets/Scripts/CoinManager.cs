using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

	private int coinCount;
	private int maxCoinCount;
	public List<GameObject> coinObject;
	public int GetCoinCount()
    {
		return coinCount;
    }

	public void ReduceCount()
    {
		coinCount--;
    }
	
	void Awake()
	{
		coinCount = transform.childCount;
		maxCoinCount = coinCount;
	}

	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
			GameObject coin = transform.GetChild(i).gameObject;
			coinObject.Add(coin);
        }
	}

	public void ResetCoins()
    {
        foreach (var item in coinObject)
        {
			item.SetActive(true);

        }
		coinCount = maxCoinCount;
    }
}