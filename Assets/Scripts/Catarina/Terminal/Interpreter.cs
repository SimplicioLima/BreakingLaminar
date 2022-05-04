using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    public GameObject door;
    bool open = false;
    List<string> response = new List<string>();
    public List<string> Interpret(string userInput)
    {
        response.Clear();

        string[] args = userInput.Split();

        if(args[0] == "help")
        {
            response.Add("if you want to use the terminal, type \"boop\" ");

            return response;
        }
        if(args[0] == "open-door")
        {
            response.Add("More information needed. Insert door number and code.");
            open = true;
            return response;
        }
        if(args[0] == "close-door")
        {
            response.Add("door closed");
            open = false;
            
            return response;
        }
        if(args[0] == "deactivate-lights")
        {
            response.Add("Insert room number and code.");
            return response;
        }
        else{
            response.Add("command not recognized.");

            return response;
        }
    }

    void Update()
    {
        if(open)
            door.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        else if(!open)
            door.transform.Rotate(new Vector3(0, 0, 0), Space.Self);
    }
}
