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

    private static int x = 1;
    private static int y;

    // Use this for initialization
    void Start () {
        LoadXMLFromAssest();

        FindPlaythrough();

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

    private void FindPlaythrough()
    {
        y = xmlDoc.GetElementsByTagName("Playthrough").Count + 1;
        XmlNode parentNode = xmlDoc.SelectSingleNode("Data");
        XmlElement playThroughElement = xmlDoc.CreateElement("Playthrough");
        playThroughElement.SetAttribute("ID", "#" + y.ToString());
        parentNode.AppendChild(playThroughElement);
        xmlDoc.Save(Application.dataPath + "/Resources/testfile.xml");
    }

    private static XmlNode createNodeByName(string name, string innerText)
    {
        XmlNode node = xmlDoc.CreateElement(name);
        node.InnerText = innerText;
        return node;
    }

    public static void WriteToXML(string helpChance, string rejected, string helpedOrHarmed, string timeStamp)
    {
        XmlNode parentNode = xmlDoc.GetElementsByTagName("Playthrough")[xmlDoc.GetElementsByTagName("Playthrough").Count - 1]; 

        XmlElement element = xmlDoc.CreateElement("Dataset");
        element.SetAttribute("ID", x.ToString());
        element.AppendChild(createNodeByName("HelpChance", helpChance));
        element.AppendChild(createNodeByName("Rejected", rejected));
        element.AppendChild(createNodeByName("Helpedorharmed", helpedOrHarmed));
        element.AppendChild(createNodeByName("Timestamp", timeStamp));
        parentNode.AppendChild(element);
        xmlDoc.Save(Application.dataPath + "/Resources/testfile.xml");

        x++;
    }
}
