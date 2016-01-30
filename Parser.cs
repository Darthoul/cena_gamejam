using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ParserValue
{
    public string name;
    public string value;
}
public class ParserNode
{
    public string name;
    public List<ParserValue> pairs = new List<ParserValue>();
    public ParserNode parent;
    public List<ParserNode> sons = new List<ParserNode>();
}
public class Parser
{

    #region Definitions & Variables
    enum State
    {
        Idle,
        ReadingName,
        ReadingNode,
        ReadingValue,
    }
    State state;
    ParserNode root = new ParserNode();

    public string localText;
    int localIndex = 0;
    ParserValue pair;
    int braceStart;
    int braceEnd;
    public bool debug;
    string errorText;
    bool error;
    bool starting = true;
    int equal = -1;
    bool equalCount = false;
    #endregion

    #region Initialization
    // Use this for initialization
	public void Start(string text) {
	    localText = text;
        StripExtraCharacter(ref localText);
        StripComments(ref localText);
        localIndex = 0;
        braceStart = 0;
        braceEnd = 0;
        error = false;
        errorText = "";
        starting = true;

        ReadWord(null);
        localText = "";

        if (debug) DebugNode(root);
        if (braceStart == braceEnd) {
            OnAttributes(root);
        }
        else {
            if (!error) {
                errorText = "Parse ERROR!! : braces count fail.";
                error = true;
            }
        }


        if (error) {
            Debug.Log(errorText);
        }
        root = null;
    }
    #endregion

    #region Striping
    void StripExtraCharacter(ref string text) {
        string trim="";
        bool quotes = false;
        for (int i = 0; i < text.Length; i++) {
            if (text[i] == '"') {
                quotes = !quotes;
            }
            if (quotes) {
                trim += text[i];
            }
            else {
                if (text[i] != ' ')
                    trim += text[i];
            }
        }
        //trim = text.Replace(" ", "");
        trim = trim.Replace("\r", "");
        trim = trim.Replace("\n", "");
        trim = trim.Replace("\t", "");
        localText = trim;
    }
    void StripComments(ref string text) {
    }
    #endregion

    #region Parse
    void ReadWord(ParserNode parent) {
        if (error) return;
        char c;
        bool end = false;
        string word = "";
        while (!end) {
            if (error) end = true;
            if (localIndex == localText.Length) {
                end = true;
                break;
            }
            c = localText[localIndex];
            localIndex++;
            if (c=='{') { // start node
                if (!starting) {
                    error = true;
                    errorText = "Parse ERROR!! : commas pair not found.";
                }
                else {
                    ParserNode node = new ParserNode();
                    node.name = word;
                    if (parent == null) {
                        node.parent = null;
                        root = node;
                    }
                    else {
                        node.parent = parent;
                        parent.sons.Add(node);
                    }
                    ReadWord(node);
                    word = "";
                    braceStart++;
                }
            }
            else if (c == '=') { // value
                if (!starting) {
                    error = true;
                    errorText = "Parse ERROR!! : commas pair not found.";
                }
                else {
                    pair.name = word;
                    word = "";
                    equal = 0;
                    equalCount = true;
                }
            }
            else if (c == '}') { // end node
                braceEnd++;
                end = true;
            }
            else if (c == '"') { // start and end of value
                if (!starting) {
                    if (parent == null) {
                        error = true;
                        errorText = "Parse ERROR!! : braces count fail.";
                    }
                    else {
                        pair.value = word;
                        word = "";
                        parent.pairs.Add(pair);
                        equal = -1;
                        equalCount = false;
                    }
                }
                else {
                    if (equal > 0) {
                        error = true;
                        errorText = "Parse ERROR!! : commas pair not found.";
                    }
                    if (equal == -1) {
                        error = true;
                        errorText = "Parse ERROR!! : equal not found.";
                    }
                }
                starting = !starting;
            }
            else {
                word += c;
                if (equalCount) equal++;
            }
        }
    }
    #endregion

    #region Debug
    void DebugNode(ParserNode node) {
        string str = node.name;
        for (int i = 0; i < node.pairs.Count; i++) {
            str += " " + node.pairs[i].name + " = " + node.pairs[i].value;
        }
        Debug.Log(str);
        for (int i = 0; i < node.sons.Count; i++) {
            DebugNode(node.sons[i]);
        }
    }
    #endregion

    #region Callers
    void OnAttributes(ParserNode node) {
        if (node.parent != null) {
            node.name = node.parent.name + "/" + node.name;
        }
        CallStart(ref node.name); 

        CallOnNode(ref node.name, node.pairs);

        for (int i = 0; i < node.sons.Count; i++) {
            OnAttributes(node.sons[i]);
        }

        CallEnd(ref node.name);
    }

    protected virtual void CallOnNode(ref string name, List<ParserValue> pairs) {

    }
    protected virtual void CallStart(ref string name) {

    }
    protected virtual void CallEnd(ref string name) {

    }
    #endregion
}
