using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models;

// 转换类型
// nowValue 【int 0:左，正，关，禁止 | 1:右，反，开，允许】【int Int32】【double Double】
record NormalSettingField(string className, string parameterName, string nowValue);
record CustomSettingInfo(int srcId, string mode, string zjType, string actionName, CustomSettingField[]? data);
record CustomSettingField(string name, string nowValue);

