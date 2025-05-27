using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Xml.Serialization;
using ConsoleApp1;
using ConsoleApp1.Models;
using WisdomMSocketServiceCore;

/*
TestBaseParameters(@"D:\faner\文档\控制器参数\baseParameters.xml");
Console.WriteLine("-----=====-----");
TestAutoactions(@"D:\faner\文档\控制器参数\autoActions.xml");
Console.WriteLine("-----=====-----");
TestExclusiveactions(@"D:\faner\文档\控制器参数\exclusiveActions.xml");
*/


while (true)
{
    TestSendBuildrootClientBaseData();
    Console.WriteLine("press any key to resend random value.");
    Console.ReadKey();
}


// 向下位机控制器发送基础设置修改数据
void TestSendBuildrootClientBaseData()
{
    UdpClient udpSender = new() { EnableBroadcast = true };
    IPEndPoint broadcastEndPoint = new(IPAddress.Broadcast, 7788);

    string gsmcd = Random.Shared.Next(30, 60).ToString();  // 主控时长 [30,60]
    var nsf = new NormalSettingField("globalSettings", "masterCtrlDuration", gsmcd);
    NormalSettingField[] content = [ nsf ];
    string json = JsonSerializer.Serialize(content);
    byte[] buffer = System.Text.Encoding.ASCII.GetBytes(json);
    var lens = buffer.Length;
    Controller controller = new() { funcCode_ = 0x30, body_ = json, len_ = 32 + lens };

    byte[] bytes = controller.MakeSendMsg();
    udpSender.Send(bytes, bytes.Length, broadcastEndPoint);

    Console.WriteLine("udp content: " + nsf);
}

void TestBaseParameters(string filePath)
{
    // 使用 XmlSerializer 反序列化
    XmlSerializer serializer = new(typeof(BaseParametersRoot));
    using FileStream fs = new(filePath, FileMode.Open);
    BaseParametersRoot root = (BaseParametersRoot)serializer.Deserialize(fs)!;

    // 输出显示结果
    if (root.ClassListItems != null)
    {
        foreach (var classList in root.ClassListItems)
        {
            Console.WriteLine($"ClassListNode {{ Type = {classList.Type}, Classes = [");
            if (classList.Classes != null)
            {
                foreach (var classItem in classList.Classes)
                {
                    Console.WriteLine($"  ClassListNodeItem {{ ClassName = {classItem.ClassName}, CnName = {classItem.CnName}, SafeLevel = {classItem.SafeLevel} }}");
                }
            }
            Console.WriteLine("] }");
        }
    }

    if (root.ClassItems != null)
    {
        foreach (var classItem in root.ClassItems)
        {
            Console.WriteLine($"ClassNode {{ ClassName = {classItem.ClassName}, Parameters = [");
            if (classItem.Parameters != null)
            {
                foreach (var parameter in classItem.Parameters)
                {
                    Console.WriteLine($"  ClassNodeParameter {{ ParameterName = {parameter.ParameterName}, CnName = {parameter.CnName}, LeftValue = {parameter.LeftValue}, RightValue = {parameter.RightValue}, NowValue = {parameter.NowValue}, InputTypeJudge = {parameter.InputTypeJudge}, Value0 = {parameter.Value0}, Value1 = {parameter.Value1}");
                    if (parameter.RelatedParameter != null)
                    {
                        Console.WriteLine($"    ClassNodeRelatedParameter {{ ClassName = {parameter.RelatedParameter.ClassName}, ParameterName = {parameter.RelatedParameter.ParameterName}, Relation = {parameter.RelatedParameter.Relation} }}");
                    }
                    Console.WriteLine("  }");
                }
            }
            Console.WriteLine("] }");
        }
    }
}

void TestAutoactions(string filePath)
{
    // 使用 XmlSerializer 反序列化
    XmlSerializer serializer = new(typeof(AutoActionsRoot));
    using FileStream fs = new(filePath, FileMode.Open);
    AutoActionsRoot root = (AutoActionsRoot)serializer.Deserialize(fs)!;

    // 输出反序列化后的数据
    foreach (var type in root.Types)
    {
        Console.WriteLine($"Type: {type.TypeAttribute}, CnName: {type.CnName}");

        if (type.ActionLists != null)
        {
            foreach (var actionList in type.ActionLists)
            {
                Console.WriteLine($"  ActionList: {actionList.ActionListName}, CnName: {actionList.CnName}, InterSupportDelay: {actionList.InterSupportDelay}");
                foreach (var action in actionList.Actions)
                {
                    Console.WriteLine($"    Action: {action.ActionName}, Duration: {action.Duration}, Step: {action.Step}");
                }
            }
        }

        if (type.Modes != null)
        {
            foreach (var mode in type.Modes)
            {
                Console.WriteLine($"  Mode: {mode.ModeName}, CnName: {mode.CnName}");
                foreach (var parameter in mode.Parameters)
                {
                    Console.WriteLine($"    Parameter: {parameter.ParameterName}, NowValue: {parameter.NowValue}, MinValue: {parameter.MinValue}, MaxValue: {parameter.MaxValue}");
                }
            }
        }
    }
}

void TestExclusiveactions(string filePath)
{
    XmlSerializer serializer = new(typeof(ExclusiveActionsRoot));

    // 从文件中加载XML并反序列化
    using FileStream stream = new(filePath, FileMode.Open);
    ExclusiveActionsRoot actionsRoot = (ExclusiveActionsRoot)serializer.Deserialize(stream)!;

    // 打印反序列化后的数据
    foreach (var action in actionsRoot.Actions)
    {
        Console.WriteLine($"Name: {action.Name}, CN Name: {action.CnName}, ExclusiveActions: {action.ExclusiveActions}, UnmodifiableOnes: {action.UnmodifiableOnes}");
    }
}