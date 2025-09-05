using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterCard : MonoBehaviour
{
    public Image img;
    public Text charName;
    public Text btnTxt;
    public Button selectBtn;
    public bool selectStatus;
    public int charIndex;

    public bool SelectStatus
    {
        get
        {
            return selectStatus;
        }
        set
        {
            selectStatus = value;
            selectBtn.interactable = !selectStatus;
            btnTxt.text = selectStatus ? "selected" : "select";
        }
    }
    public void Initialise(CharacterData data, bool _selectStatus, int _charIndex, Action<int> OnBtnClick)
    {
        charIndex = _charIndex;
        SelectStatus = _selectStatus;

        img.sprite = data.charPic;
        charName.text = "Name : " + data.charName + "\n";

        selectBtn.onClick.AddListener(() => {
            SelectStatus = true;
            OnBtnClick.Invoke(charIndex);
        });
    }
}
