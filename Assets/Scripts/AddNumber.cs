using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddNumber : MonoBehaviour, IDataPersistence
{
    private Text game_Text;
    public GameObject UI_text;
    private int number;
    // Start is called before the first frame update
    void Start()
    {
        
        game_Text = UI_text.GetComponent<Text>();
        game_Text.text=number.ToString();
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // loads the game data 
    public void LoadData(GameData data)
    {
        this.number= data.num;
    }


    // saves the game data
    public void SaveData(ref GameData data)
    {
        data.num = this.number;
    }

    public void Add()
    {
        number++;
        game_Text.text = number.ToString();
    }
}
