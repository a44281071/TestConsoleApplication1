使用C#代码启用wifi热点共享

## 搜索到一些提示

win10已经抛弃了承载网络，现在的移动热点基于WiFi Direct技术，大部分新的网卡也都支持。在UWP平台下，取而代之的是TetheringNetwork，关于这东西的API可以在官网上查到，但是需要在powershell中才能调用，cmd已经不支持了。

## 官方文档

[在桌面应用中调用 Windows 运行时 API](https://learn.microsoft.com/zh-cn/windows/apps/desktop/modernize/desktop-to-uwp-enhance)

[控制和配置特定网络帐户的共享功能](https://learn.microsoft.com/zh-cn/uwp/api/windows.networking.networkoperators.networkoperatortetheringmanager?view=winrt-26100&devlangs=csharp&f1url=%3FappId%3DDev17IDEF1%26l%3DZH-CN%26k%3Dk(Windows.Networking.NetworkOperators.NetworkOperatorTetheringManager)%3Bk(DevLang-csharp)%26rd%3Dtrue)

[表示 StartTetheringAsync 或 StopTetheringAsync 操作的结果](https://learn.microsoft.com/zh-cn/uwp/api/windows.networking.networkoperators.networkoperatortetheringoperationresult?view=winrt-26100)