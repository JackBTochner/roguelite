using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


// TODO: methods NEED TO SANITISE THEIR INPUTS. PLEASE FIX.
public static class SerializerUtility
{
    public static List<string> StringToList(string field)
    {
        return JsonConvert.DeserializeObject<List<string>>(field);
    }

    public static string ListToString(List<string> list)
    {
        return JsonConvert.SerializeObject(list);
    }

    public static Dictionary<string, int> StringToDictionary(string field)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, int>>(field);
    }
}