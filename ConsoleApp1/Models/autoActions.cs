using System.Xml.Serialization;

#nullable disable

namespace ConsoleApp1;

[XmlRoot("root")]
public class AutoActionsRoot
{
    [XmlElement("Type")]
    public List<AutoActionsType> Types { get; set; }
}

public class AutoActionsType
{
    [XmlAttribute("type")]
    public string TypeAttribute { get; set; }

    [XmlAttribute("cn_name")]
    public string CnName { get; set; }

    [XmlElement("ActionList")]
    public List<AutoActionsActionList> ActionLists { get; set; }

    [XmlElement("Mode")]
    public List<AutoActionsMode> Modes { get; set; }
}

public class AutoActionsActionList
{
    [XmlAttribute("actionListName")]
    public string ActionListName { get; set; }

    [XmlAttribute("cn_name")]
    public string CnName { get; set; }

    [XmlAttribute("interSupportDelay")]
    public string InterSupportDelay { get; set; }

    [XmlElement("Action")]
    public List<AutoActionsAction> Actions { get; set; }
}

public class AutoActionsAction
{
    [XmlAttribute("actionName")]
    public string ActionName { get; set; }

    [XmlAttribute("duration")]
    public string Duration { get; set; }

    [XmlAttribute("step")]
    public string Step { get; set; }
}

public class AutoActionsMode
{
    [XmlAttribute("mode")]
    public string ModeName { get; set; }

    [XmlAttribute("cn_name")]
    public string CnName { get; set; }

    [XmlElement("Parameter")]
    public List<AutoActionsParameter> Parameters { get; set; }
}

public class AutoActionsParameter
{
    [XmlAttribute("parameterName")]
    public string ParameterName { get; set; }

    [XmlAttribute("nowValue")]
    public string NowValue { get; set; }

    [XmlAttribute("minValue")]
    public string MinValue { get; set; }

    [XmlAttribute("maxValue")]
    public string MaxValue { get; set; }
}