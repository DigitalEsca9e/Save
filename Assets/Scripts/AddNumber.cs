using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddNumber : MonoBehaviour, IDataPersistence
{
    private Text game_Text;
    public GameObject UI_text;
    private int number = 0;
    // Start is called before the first frame update
    void Start()
    {
        number = 0;
        game_Text = UI_text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // loads the game data 
    public void LoadData(GameData data)
    {
        this.number= data.number;
    }


    // saves the game data
    public void SaveData(ref GameData data)
    {
        data.number = this.number;
    }

    public void Add()
    {
        number++;
        game_Text.text = number.ToString();
    }
}
