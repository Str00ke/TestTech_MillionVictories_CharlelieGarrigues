using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static class JsonDeserializer
{
    //Deserialize Json file with provided file path, store data into class/struct of type T, and return it.
    //If Json contains multiple data, Type T must be an Array (for exemple List<T>)
    public static T Deserialize<T>(string path) 
    {
        JsonSerializer serializer = new JsonSerializer();

        using (StreamReader sr = new StreamReader(path))
        using (JsonReader reader = new JsonTextReader(sr))
        {
            T items = serializer.Deserialize<T>(reader);
            return items;
        }
    }
}
