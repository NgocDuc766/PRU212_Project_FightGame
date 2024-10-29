using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    private int index;
    [SerializeField] GameObject[] characters;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    public void OnPrevBtnClick()
    {
        if (index > 0)
        {
            index--;
        }
        SelectCharacter();
    }
    public void OnNextBtnClick()
    {
        if (index < characters.Length)
        {
            index++;
        }
        SelectCharacter();
    }
    public void SelectCharacter() { 
    
    }
}
