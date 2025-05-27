using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable

namespace ConsoleApp1;

// 根节点类
[XmlRoot("root")]
public class ExclusiveActionsRoot
{
    [XmlElement("Action")]
    public List<ExclusiveActionsItem> Actions { get; set; }
}

// Action节点类
public class ExclusiveActionsItem
{
    [XmlAttribute("exclusiveActions")]
    public string ExclusiveActions { get; set; }

    [XmlAttribute("cn_name")]
    public string CnName { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("unmodifiableOnes")]
    public string UnmodifiableOnes { get; set; }
}