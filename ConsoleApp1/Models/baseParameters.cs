using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable

namespace ConsoleApp1;

[XmlRoot("root")]
public class BaseParametersRoot
{
    [XmlElement("classList")]
    public List<ClassListNode> ClassListItems { get; set; }

    [XmlElement("class")]
    public List<ClassNode> ClassItems { get; set; }
}

#region classList

public class ClassListNode
{
    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlElement("class")]
    public List<ClassListNodeItem> Classes { get; set; }
}

public class ClassListNodeItem
{
    [XmlAttribute("className")]
    public string ClassName { get; set; }

    [XmlAttribute("cn_name")]
    public string CnName { get; set; }

    [XmlAttribute("safeLevel")]
    public int SafeLevel { get; set; }
}

#endregion

#region class
 
public class ClassNode
{
    [XmlAttribute("className")]
    public string ClassName { get; set; }

    [XmlElement("parameter")]
    public List<ClassNodeParameter> Parameters { get; set; }
}

public class ClassNodeParameter
{
    [XmlAttribute("parameterName")]
    public string ParameterName { get; set; }

    [XmlAttribute("cn_name")]
    public string CnName { get; set; }

    [XmlAttribute("leftValue")]
    public string LeftValue { get; set; }

    [XmlAttribute("rightValue")]
    public string RightValue { get; set; }

    [XmlAttribute("nowValue")]
    public string NowValue { get; set; }

    [XmlAttribute("inputTypeJudge")]
    public string InputTypeJudge { get; set; }

    [XmlAttribute("value_0")]
    public string Value0 { get; set; }

    [XmlAttribute("value_1")]
    public string Value1 { get; set; }

    [XmlElement("ralatedParameter")]
    public ClassNodeRelatedParameter RelatedParameter { get; set; }
}

public class ClassNodeRelatedParameter
{
    [XmlAttribute("className")]
    public string ClassName { get; set; }

    [XmlAttribute("parameterName")]
    public string ParameterName { get; set; }

    [XmlAttribute("relation")]
    public string Relation { get; set; }
}

#endregion