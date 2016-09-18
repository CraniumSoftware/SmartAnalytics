// Copyright (c) 2014 Cranium Software

// SmartAnalytics
//
// A platform agnostic wrapper for the Google Analytics Web API


using UnityEngine;
using System.Collections;

/// <summary>
/// A platform agnostic wrapper for the Google Analytics Web API
/// </summary>
public class SmartAnalytics
{
	private const int k_protocolVersion = 1;
	private const string k_apiEndpoint = "http://www.google-analytics.com/collect?payload_data";

	/// <summary>
	/// Set the tracking ID from your Google Analytics account, e.g. SmartAnalytics.SetTrackingID( "UA-43926062-2" );
	/// </summary>
	public static void SetTrackingID( string trackingID )
	{
		s_trackingID = trackingID;
	}

	/// <summary>
	/// Set the referrer to track where events and views originate from
	/// </summary>
	public static void SetReferrer( string referrer ) { s_referrer = WWW.EscapeURL( referrer ); }

	/// <summary>
	/// Report an event
	/// </summary>
	public static void SendEvent( string eventLabel, float eventValue, string eventAction, string eventCategory )
	{
		string requestString = k_apiEndpoint + GetEventString()
			+ "&ec=" + WWW.EscapeURL( eventCategory ) + "&ea=" + WWW.EscapeURL( eventAction ) + ( ( eventLabel.Length > 0 ) ? ( "&el=" + WWW.EscapeURL( eventLabel ) ) : "" ) + "&ev=" + eventValue.ToString()
				+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}

	/// <summary>
	/// Report an event
	/// </summary>
	public static void SendEvent( string eventLabel, float eventValue, string eventAction )
	{
		string requestString = k_apiEndpoint + GetEventString()
			+ "&ec=" + WWW.EscapeURL( "(uncategorised)" ) + "&ea=" + WWW.EscapeURL( eventAction ) + ( ( eventLabel.Length > 0 ) ? ( "&el=" + WWW.EscapeURL( eventLabel ) ) : "" ) + "&ev=" + eventValue.ToString()
				+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}

	/// <summary>
	/// Report an event
	/// </summary>
	public static void SendEvent( string eventAction, float eventValue )
	{
		string requestString = k_apiEndpoint + GetEventString()
			+ "&ec=" + WWW.EscapeURL( "(uncategorised)" ) + "&ea=" + WWW.EscapeURL( eventAction ) + ( ( eventAction.Length > 0 ) ? ( "&el=" + WWW.EscapeURL( eventAction ) ) : "" ) + "&ev=" + eventValue.ToString()
				+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}

	/// <summary>
	/// Report an event
	/// </summary>
	public static void SendEvent( string eventLabel, string eventAction, string eventCategory )
	{
		string requestString = k_apiEndpoint + GetEventString()
			+ "&ec=" + WWW.EscapeURL( eventCategory ) + "&ea=" + WWW.EscapeURL( eventAction ) + ( ( eventLabel.Length > 0 ) ? ( "&el=" + WWW.EscapeURL( eventLabel ) ) : "" ) + "&ev=0"
				+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}

	/// <summary>
	/// Report an event
	/// </summary>
	public static void SendEvent( string eventLabel, string eventAction )
	{
		string requestString = k_apiEndpoint + GetEventString()
			+ "&ec=" + WWW.EscapeURL( "(uncategorised)" ) + "&ea=" + WWW.EscapeURL( eventAction ) + ( ( eventLabel.Length > 0 ) ? ( "&el=" + WWW.EscapeURL( eventLabel ) ) : "" ) + "&ev=0"
				+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}

	/// <summary>
	/// Report an event
	/// </summary>
	public static void SendEvent( string eventAction )
	{
		string requestString = k_apiEndpoint + GetEventString()
			+ "&ec=" + WWW.EscapeURL( "(uncategorised)" ) + "&ea=" + WWW.EscapeURL( eventAction ) + ( ( eventAction.Length > 0 ) ? ( "&el=" + WWW.EscapeURL( eventAction ) ) : "" ) + "&ev=0"
				+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}

	/// <summary>
	/// Report a view, e.g. SmartAnalytics.SendView( "Main Menu" );
	/// </summary>
	public static void SendView( string name )
	{
		string requestString = k_apiEndpoint + GetViewString( name )
			+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}

	/// <summary>
	/// Report a timing in milliseconds
	/// </summary>
	public static void SendTiming( string name, string category, string label, int timeInMS )
	{
		string requestString = k_apiEndpoint + GetTimingString()
			+ "&utc=" + WWW.EscapeURL( category ) + "&utv=" + WWW.EscapeURL( name ) + "&utl=" + WWW.EscapeURL( label ) + "&utt=" + timeInMS
				+ GetRequiredParameters() + GetSaltString();
		SmartAnalyticsNetwork.HTTPGetRequest( requestString );
	}
	
	private static void InitialiseClientID()
	{
		if( s_clientID == null )
		{
			s_clientID = PlayerPrefs.GetString( "GACID" );
			if( ( s_clientID == null ) || ( s_clientID.Length == 0 ) )
			{
				s_clientID = Random.Range( 0, int.MaxValue ).ToString("X8")
					+ "-1337-"
						+ ( Random.Range( 0, int.MaxValue ) & 0xFFFF ).ToString("X4")
						+ "-" + ( Random.Range( 0, int.MaxValue ) & 0xFFFF ).ToString("X4")
						+ "-" + ( Random.Range( 0, int.MaxValue ) & 0xFFFFFF ).ToString("X6") + "B00B1E";
				
				PlayerPrefs.SetString( "GACID", s_clientID );
			}
		}
	}

	private static void InitialiseVersion()
	{
		if( s_version == null )
		{
			s_version = SmartBundleInfo.Version;
		}
	}

	private static void InitialiseAppName()
	{
		if( s_appName == null )
		{
			s_appName = SmartBundleInfo.AppName;
		}
	}
	
	private static string GetRequiredParameters() { return GetTrackingIDString() + GetVersionString() + GetClientString() + GetAppNameString() + GetOptionString(); }
	
	private static string GetTrackingIDString() { return "&tid=" + s_trackingID; }
	private static string GetVersionString() { InitialiseVersion (); return "&v=" + k_protocolVersion; }
	private static string GetClientString() { InitialiseClientID(); return "&cid=" + s_clientID; }
	private static string GetAppNameString() { InitialiseAppName(); return "&an=" + WWW.EscapeURL( s_appName ) + "&av=" + WWW.EscapeURL( s_version ); }
	
	private static string GetOptionString()
	{
		string optionString = "&aip=" + ( s_anonymiseIP ? "1" : " 0 " );
		if( s_referrer.Length > 0 )
		{
			optionString += "&dr=" + s_referrer;
		}
		
		return optionString + GetScreenResolutionString();
	}
	
	private static string GetScreenResolutionString()
	{
		return "&sr=" + Screen.width + "x" + Screen.height;
	}
	
	private static string GetEventString() { return "&t=event"; }
	private static string GetTimingString() { return "&t=timing"; }
	private static string GetViewString( string name ) { return "&t=appview&cd=" + WWW.EscapeURL( name ); }
	
	private static string GetSaltString() { return "&z=" + Random.Range( 10000000, 30000000 ); }
	
	private static string s_referrer = "";

	private static string s_trackingID = "UA-43926062-2"; // this is our test tracking ID - if not overwritten with SetTrackingID your analytics will go here!
	private static string s_clientID = null;
	
	private static string s_appName = null;
	private static string s_version = null;

	private static bool s_anonymiseIP = true;
	
}

