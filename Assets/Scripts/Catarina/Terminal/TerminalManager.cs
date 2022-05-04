using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    public GameObject directoryLine;
    public GameObject responseLine;

    public InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect SR;
    public GameObject msgList;

    Interpreter interpreter;

    void Start()
    {
        interpreter = GetComponent<Interpreter>();
    }

    private void OnGUI()
    {
        if (terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            // store wtv the user typed
            string userInput = terminalInput.text;

            ClearInputField();

            AddDirectoryLine(userInput);

            // add the interpretation lines
            int lines = AddInterpreterLines(interpreter.Interpret(userInput));

            // scroll to the bottom of the scrollRect
            ScrollToBottom(lines);

            // move the user input line to the end
            userInputLine.transform.SetAsLastSibling();

            // refocus the input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }
    }

    void ClearInputField()
    {
        terminalInput.text = "";
    }

    void AddDirectoryLine(string userInput)
    {
        // the size of the container has to grow if limits is passado senao fica estranho
        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        // will grow vertically
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35.0f);

        // instanciar directory line
        GameObject msg = Instantiate(directoryLine, msgList.transform);

        // set its child index
        msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);

        // set the text of this new gameobject
        msg.GetComponentsInChildren<Text>()[1].text = userInput;
    }

    int AddInterpreterLines(List<string> interpretation)
    {
        // instantiate the response line
        for(int i = 0; i < interpretation.Count; i++)
        {
            GameObject res = Instantiate(responseLine, msgList.transform);

            // set it to the end of all the messages
            res.transform.SetAsLastSibling();

            // get the size of the message list
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 35.0f);

            // set the text of this response line to be whatever the interpreter string is
            res.GetComponentInChildren<Text>().text = interpretation[i];
        }

        return interpretation.Count;
    }

    void ScrollToBottom(int lines)
    {
        if(lines > 4)
        {
            SR.velocity = new Vector2(0, 450);
        }
        else
        {
            // bottom of the scroll rect
            SR.verticalNormalizedPosition = 0;
        }
    }
}
