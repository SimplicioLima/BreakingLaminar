using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    public GameObject objectToDelete;
    bool open = false;
    List<string> response = new List<string>();

    void Start()
    {
        objectToDelete.SetActive(true);
    }

    public List<string> Interpret(string userInput)
    {
        response.Clear();

        string[] args = userInput.Split();

        if(args[0] == "help")
        {
            response.Add("Check out the information book to know how to handle the terminal");

            return response;
        }
        if(args[0] == "open-door")
        {
            response.Add("More information needed. Insert door number and code.");
            open = true;
            return response;
        }
        if(args[0] == "access-server")
        {
            response.Add("Insert server password");
            
            return response;
        }
        if(args[0] == "1234")
        {
            response.Add("server accessed");
            
            return response;
        }
        if(args[0] == "nerak-info")
        {
            response.Add("Karen is a highly developed artificial intelligence, built to facilitate de lifes of our brave army.");
            return response;
        }
        if(args[0] == "off-karen-admin")
        {
            response.Add("Karen off.");
            return response;
        }
        else{
            response.Add("command not recognized.");

            return response;
        }
    }

    void Update()
    {
        
    }
}
