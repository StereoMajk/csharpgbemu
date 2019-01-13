using System;
using System.Windows.Forms;
using System.Xml;

namespace QuickNDirtyXML
{
  public class Settings
  {
    XmlDocument xmlDocument = new XmlDocument();
    string documentPath = Application.StartupPath + "\\settings.xml";

    public  Settings()
    { try {xmlDocument.Load(documentPath);}
      catch {xmlDocument.LoadXml("<settings></settings>");}
    }

    public int GetSetting(string xPath, int defaultValue)
    { return Convert.ToInt32(GetSetting(xPath, Convert.ToString(defaultValue))); }

    public void PutSetting(string xPath, int value)
    { PutSetting(xPath, Convert.ToString(value)); }

    public string GetSetting(string xPath,  string defaultValue)
    { XmlNode xmlNode = xmlDocument.SelectSingleNode("settings/" + xPath );
      if (xmlNode != null) {return xmlNode.InnerText;}
      else { return defaultValue;}
    }

    public void PutSetting(string xPath,  string value)
    { XmlNode xmlNode = xmlDocument.SelectSingleNode("settings/" + xPath);
      if (xmlNode == null) { xmlNode = createMissingNode("settings/" + xPath); }
      xmlNode.InnerText = value;
      xmlDocument.Save(documentPath);
    }

    private XmlNode createMissingNode(string xPath)
    { string[] xPathSections = xPath.Split('/');
      string currentXPath = "";
      XmlNode testNode = null;
      XmlNode currentNode = xmlDocument.SelectSingleNode("settings");
      foreach (string xPathSection in xPathSections)
      { currentXPath += xPathSection;
        testNode = xmlDocument.SelectSingleNode(currentXPath);
        if (testNode == null){currentNode.InnerXml += "<" + xPathSection + "></" + xPathSection + ">";}
        currentNode = xmlDocument.SelectSingleNode(currentXPath);
        currentXPath += "/";
      }
      return currentNode;
    }
  }
}
