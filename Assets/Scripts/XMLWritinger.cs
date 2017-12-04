using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class XMLWritinger : MonoBehaviour {

    
    public static XmlWriterSettings writerSettings;
    public static XmlWriter xmlWriter;
    private TextAsset textXML;
    private static XmlDocument xmlDoc;
    public static int chanceStore;
    public static bool rejectStore;
    public static string statusStore;
    public static string timeToStore;
    private static bool fileMade = false;

    private static int x = 1;

    // Use this for initialization
    void Start () {
        LoadXMLFromAssest();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadXMLFromAssest()
    {
        xmlDoc = new XmlDocument();
        textXML = (TextAsset)Resources.Load("testfile", typeof(TextAsset));
        xmlDoc.LoadXml(textXML.text);
    }

    private static XmlNode createNodeByName(string name, string innerText)
    {
        XmlNode node = xmlDoc.CreateElement(name);
        node.InnerText = innerText;
        return node;
    }

    public void StartWrite()
    {
        //chanceStore = gameObject.GetComponent<DataStore>().helpChance;
        //rejectStore = gameObject.GetComponent<DataStore>().rejected;
        //statusStore = gameObject.GetComponent<DataStore>().helpedOrHarmed;
        //timeToStore = gameObject.GetComponent<DataStore>().timeStamp;

        //WriteToXML();
    }

    public static void WriteToXML(string helpChance, string rejected, string helpedOrHarmed, string timeStamp)
    {
        XmlNode parentNode = xmlDoc.SelectSingleNode("Data");
        XmlElement element = xmlDoc.CreateElement("DataSet");
        element.SetAttribute("SessionID", x.ToString());
        element.AppendChild(createNodeByName("HelpChance", /*chanceStore.ToString()*/helpChance));
        element.AppendChild(createNodeByName("Rejected",/* rejectStore.ToString()*/rejected));
        element.AppendChild(createNodeByName("Helpedorharmed", /*statusStore*/helpedOrHarmed));
        element.AppendChild(createNodeByName("Timestamp", timeStamp));
        parentNode.AppendChild(element);
        xmlDoc.Save(Application.dataPath + "/Resources/testfile.xml");

        x++;
    }
}
