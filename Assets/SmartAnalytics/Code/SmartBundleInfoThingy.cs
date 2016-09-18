// Copyright (c) 2014 Cranium Software

using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class SmartBundleInfoThingy
{
	private const string k_className = "SmartBundleInfo";
	
	private const string k_fileName = "Assets/SmartAnalytics/Code/" + k_className + ".cs";
	
	static SmartBundleInfoThingy()
	{
		UpdateCode( PlayerSettings.bundleVersion, PlayerSettings.productName );
	}
	
	private static void UpdateCode( string bundleVersion, string appName )
	{
		string output = GenerateCode( bundleVersion, appName );
		File.WriteAllText( k_fileName, output );
	}

	private static string GenerateCode( string bundleVersion, string appName )
	{
		string code = "public static class " + k_className + "\n{\n";
		code += "\tpublic static readonly string Version = \"" + bundleVersion + "\";\n";
		code += "\tpublic static readonly string AppName = \"" + appName + "\";\n";
		code += "\n}\n";
		return code;
	}
}
