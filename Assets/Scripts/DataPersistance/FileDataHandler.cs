using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath="";

    private string dataFileName="";


    public FileDataHandler(string dtDirPath, string dtFileName){
        this.dataDirPath=dtDirPath;
        this.dataFileName=dtFileName;
    }

    public GameData Load(){

        //gets the full path of the file directory
        string fullPath =Path.Combine(dataDirPath,dataFileName);

        //sets the gamedata to null at start
        GameData loadedData=null;

        //checks if the provided path exists 
        if(File.Exists(fullPath)){

            //try-catch to check for path errors
            try{
                //Creates an emt string to hold the contents of the JSON file
                string dataToLoad="";

                //Opens the JSON file 
                using(FileStream stream=new FileStream(fullPath, FileMode.Open)){

                    //Opens a Stream Reder to read from file
                    using (StreamReader reader=new StreamReader(stream)){
                        // Reads till end of file and saves it to dataToLoad
                        dataToLoad=reader.ReadToEnd();
                    }
                }

                //converts the JSON file to GameData Object
                loadedData=JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e){
                Debug.LogError("Error occured when trying to laod data from file: "+ fullPath +"\n"+e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data){

        //gets the full path of the file 
        string fullPath =Path.Combine(dataDirPath,dataFileName);

        //try- catch to check for full path of directory errors
        try{
            //creates the directory of the JSON file
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore=JsonUtility.ToJson(data, true);

            //Opens the JSON file
            using (FileStream stream =new FileStream(fullPath, FileMode.Create))
            {

                //writes to the JSON file
                using(StreamWriter writer= new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }
        //exception catch
        catch (Exception e){
            Debug.LogError("Error occured when trying to save data to file "+fullPath +"\n"+e);
        }
    }
}
