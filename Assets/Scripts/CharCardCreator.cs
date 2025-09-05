using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCardCreator : MonoBehaviour
{
    
    public GameObject cardPrefab;
    public int charIndex;
    public List<CharacterData> cardData;
    public List<CharacterCard> characterCards;
    string characterKey = "charkey";

    void Awake()
    {
        charIndex = PlayerPrefs.GetInt(characterKey, 0);
        for (int i = 0; i < cardData.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, transform);
            bool selectStatus = (i == charIndex);
            newCard.GetComponent<CharacterCard>().Initialise(cardData[i], selectStatus, i, OnBtnClick);
            characterCards.Add(newCard.GetComponent<CharacterCard>());
        }
    }
    public void OnBtnClick(int index)
    {
        print(index);
        for (int i = 0; i < characterCards.Count; i++)
        {   
            characterCards[i].SelectStatus = (i == index);
        }
        charIndex = index;
        PlayerPrefs.SetInt(characterKey, charIndex);
    }
}
