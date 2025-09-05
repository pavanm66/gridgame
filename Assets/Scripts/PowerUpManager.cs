using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
	public List<GameObject> powerUp;

	void Awake () {
        for (int i = 0; i < transform.childCount; i++)
        {
			powerUp.Add(transform.GetChild(i).gameObject);
        }
	}
    public void ResetPowerUps()
    {
        foreach (var item in powerUp)
        {
            item.SetActive(true);
        }
    }
}
public enum PowerType
{
    none,
    doubleCoin,
    immunity,
    doubleSpeed

}
