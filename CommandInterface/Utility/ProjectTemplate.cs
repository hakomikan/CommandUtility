using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface.Utility
{
    public static class ProjectTemplates
    {
        public static string BasicMainSourceCode = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
";

        public static string BasicAssemblyInfo = @"using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle(""CommandInterface"")]
[assembly: AssemblyDescription("""")]
        [assembly: AssemblyConfiguration("""")]
        [assembly: AssemblyCompany("""")]
        [assembly: AssemblyProduct(""CommandInterface"")]
        [assembly: AssemblyCopyright(""Copyright ©  2017"")]
        [assembly: AssemblyTrademark("""")]
        [assembly: AssemblyCulture("""")]

        // ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
        // 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、
        // その型の ComVisible 属性を true に設定してください。
        [assembly: ComVisible(false)]

        // このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
        [assembly: Guid(""ae0d18b9-6def-479c-b526-c2f09397ee1c"")]

        // アセンブリのバージョン情報は次の 4 つの値で構成されています:
        //
        //      メジャー バージョン
        //      マイナー バージョン
        //      ビルド番号
        //      Revision
        //
        // すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を 
        // 既定値にすることができます:
        // [assembly: AssemblyVersion(""1.0.*"")]
        [assembly: AssemblyVersion(""1.0.0.0"")]
        [assembly: AssemblyFileVersion(""1.0.0.0"")]
";

        public static string BasicPackageConfig = @"<?xml version=""1.0"" encoding=""utf-8""?>
<packages>
  <package id = ""Microsoft.CodeAnalysis.Common"" version=""2.3.1"" targetFramework=""net462"" />
  <package id = ""Microsoft.CodeAnalysis.CSharp"" version=""2.3.1"" targetFramework=""net462"" />
  <package id = ""Microsoft.CodeAnalysis.CSharp.Scripting"" version=""2.3.1"" targetFramework=""net462"" />
  <package id = ""Microsoft.CodeAnalysis.Scripting.Common"" version=""2.3.1"" targetFramework=""net462"" />
  <package id = ""System.AppContext"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Collections"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Collections.Immutable"" version=""1.3.1"" targetFramework=""net462"" />
  <package id = ""System.Diagnostics.Debug"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Diagnostics.StackTrace"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Diagnostics.Tools"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Globalization"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.IO"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.IO.FileSystem"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.IO.FileSystem.Primitives"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Linq"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Linq.Expressions"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Reflection"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Reflection.Extensions"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Resources.ResourceManager"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Runtime"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Runtime.Extensions"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Runtime.InteropServices"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Threading"" version=""4.3.0"" targetFramework=""net462"" />
  <package id = ""System.Threading.Tasks"" version=""4.3.0"" targetFramework=""net462"" />
</packages>";

        public static string BasicAppConfig = @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
    <startup> 
        <supportedRuntime version = ""v4.0"" sku="".NETFramework,Version=v4.6.2"" />
    </startup>
  <runtime>
    <assemblyBinding xmlns = ""urn:schemas-microsoft-com:asm.v1"" >
      <dependentAssembly>
        <assemblyIdentity name=""System.Runtime"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.1.1.0"" newVersion=""4.1.1.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name = ""System.IO"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.1.1.0"" newVersion=""4.1.1.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name = ""System.Reflection"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.1.1.0"" newVersion=""4.1.1.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name = ""System.Runtime.Extensions"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.1.1.0"" newVersion=""4.1.1.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name = ""System.Runtime.InteropServices"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.1.0.0"" newVersion=""4.1.0.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name = ""System.IO.FileSystem"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.0.2.0"" newVersion=""4.0.2.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name = ""System.IO.FileSystem.Primitives"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.0.2.0"" newVersion=""4.0.2.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name = ""System.Diagnostics.StackTrace"" publicKeyToken=""b03f5f7f11d50a3a"" culture=""neutral"" />
        <bindingRedirect oldVersion = ""0.0.0.0-4.0.3.0"" newVersion=""4.0.3.0"" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
";

        public static string BasicSolution = @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 14
VisualStudioVersion = 14.0.24720.0
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""CommandInterface"", ""TestProject.csproj"", ""{AE0D18B9-6DEF-479C-B526-C2F09397EE1C}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{AE0D18B9-6DEF-479C-B526-C2F09397EE1C}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{AE0D18B9-6DEF-479C-B526-C2F09397EE1C}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{AE0D18B9-6DEF-479C-B526-C2F09397EE1C}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{AE0D18B9-6DEF-479C-B526-C2F09397EE1C}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";

        public static string BasicTemplate = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""14.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProjectGuid>{AE0D18B9-6DEF-479C-B526-C2F09397EE1C}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>CommandInterface</RootNamespace>
    <AssemblyName>CommandInterface</AssemblyName>
    <TargetFrameworkVersion>v4.6.2</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <TargetFrameworkProfile />
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""Microsoft.CodeAnalysis, Version=2.3.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.CodeAnalysis.Common.2.3.1\lib\netstandard1.3\Microsoft.CodeAnalysis.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.CodeAnalysis.CSharp, Version=2.3.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.CodeAnalysis.CSharp.2.3.1\lib\netstandard1.3\Microsoft.CodeAnalysis.CSharp.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.CodeAnalysis.CSharp.Scripting, Version=2.3.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.CodeAnalysis.CSharp.Scripting.2.3.1\lib\netstandard1.3\Microsoft.CodeAnalysis.CSharp.Scripting.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.CodeAnalysis.Scripting, Version=2.3.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.CodeAnalysis.Scripting.Common.2.3.1\lib\netstandard1.3\Microsoft.CodeAnalysis.Scripting.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System"" />
    <Reference Include=""System.AppContext, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.AppContext.4.3.0\lib\net46\System.AppContext.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Collections.Immutable, Version=1.2.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.Collections.Immutable.1.3.1\lib\portable-net45+win8+wp8+wpa81\System.Collections.Immutable.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.ComponentModel.Composition"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Diagnostics.StackTrace, Version=4.0.3.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.Diagnostics.StackTrace.4.3.0\lib\net46\System.Diagnostics.StackTrace.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.IO, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.IO.4.3.0\lib\net462\System.IO.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.IO.FileSystem, Version=4.0.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.IO.FileSystem.4.3.0\lib\net46\System.IO.FileSystem.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.IO.FileSystem.Primitives, Version=4.0.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.IO.FileSystem.Primitives.4.3.0\lib\net46\System.IO.FileSystem.Primitives.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Reflection, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.Reflection.4.3.0\lib\net462\System.Reflection.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Runtime, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.Runtime.4.3.0\lib\net462\System.Runtime.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Runtime.Extensions, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.Runtime.Extensions.4.3.0\lib\net462\System.Runtime.Extensions.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Runtime.InteropServices, Version=4.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL"">
      <HintPath>..\packages\System.Runtime.InteropServices.4.3.0\lib\net462\System.Runtime.InteropServices.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Runtime.Serialization"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Net.Http"" />
    <Reference Include=""System.Xml"" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""Program.cs"" />
    <Compile Include=""Properties\AssemblyInfo.cs"" />
    <Compile Include=""Scripts\TestScript.cs"" />
    <Compile Include=""Scripts\TestScript2.cs"" />
  </ItemGroup>
  <ItemGroup>
    <None Include=""App.config"" />
    <None Include=""packages.config"" />
  </ItemGroup>
  <ItemGroup />
  <Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name=""BeforeBuild"">
  </Target>
  <Target Name=""AfterBuild"">
  </Target>
  -->
</Project>";
    }
}
