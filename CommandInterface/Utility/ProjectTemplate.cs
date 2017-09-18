﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterface.Utility
{
    public static class ProjectTemplates
    {
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
    <Compile Include=""CommandManager.cs"" />
    <Compile Include=""Exceptions.cs"" />
    <Compile Include=""Utility\NameConverter.cs"" />
    <Compile Include=""Program.cs"" />
    <Compile Include=""Properties\AssemblyInfo.cs"" />
    <Compile Include=""Utility\CSharpAssembly.cs"" />
    <Compile Include=""Utility\ProjectConstructor.cs"" />
    <Compile Include=""Utility\SolutionConstructor.cs"" />
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
