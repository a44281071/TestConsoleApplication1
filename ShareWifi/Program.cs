using Windows.Networking.Connectivity;
using Windows.Networking.NetworkOperators;

// 获取当前的网络连接配置文件
ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();

if (connectionProfile == null)
{
    Console.WriteLine("无法获取当前网络连接配置。");
    return;
}

// 创建一个 NetworkOperatorTetheringManager 实例
NetworkOperatorTetheringManager tetheringManager = NetworkOperatorTetheringManager.CreateFromConnectionProfile(connectionProfile);

// 启动移动热点
NetworkOperatorTetheringOperationResult result = await tetheringManager.StartTetheringAsync();

// 检查启动结果
if (result.Status == TetheringOperationStatus.Success)
{
    Console.WriteLine("移动热点启动成功。");
}
else
{
    Console.WriteLine($"移动热点启动失败，错误代码：{result.Status}，错误信息：{result.AdditionalErrorMessage}");
}