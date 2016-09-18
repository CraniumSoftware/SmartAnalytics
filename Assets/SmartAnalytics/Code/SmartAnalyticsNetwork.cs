// Copyright (c) 2014-2015 Cranium Software

// SmartAnalyticsNetwork
//
// A wrapper for simple network functionality using HTTP requests

// TODO:
// * add support for more HTTP stuff

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SmartAnalyticsNetwork
{
	public delegate void HTTPRequestComplete( string response );
	
	public static void HTTPGetRequest( string requestString )
	{
		HTTPGetRequest( requestString, null );
	}
	
	public static void HTTPGetRequest( string requestString, HTTPRequestComplete onComplete )
	{
		DummyBehaviour.StartCoroutine( SendGetRequestAsyncHelper( requestString, onComplete ) ) ;
	}
	
	public static void HTTPPostRequest( string requestString, byte[] data, Dictionary< string, string > headers )
	{
		HTTPPostRequest( requestString, data, headers, null );
	}
	
	public static void HTTPPostRequest( string requestString, byte[] data, Dictionary< string, string > headers, HTTPRequestComplete onComplete )
	{
		DummyBehaviour.StartCoroutine( SendPostRequestAsyncHelper( requestString, data, headers, onComplete ) ) ;
	}
	
	private static IEnumerator SendGetRequestAsyncHelper( string requestString, HTTPRequestComplete onComplete )
	{
		WWW www = new WWW( requestString );
		yield return www;
		
		HandleHTTPRequestCompletion( www, onComplete );
	}
	
	private static IEnumerator SendPostRequestAsyncHelper( string requestString, byte[] data, Dictionary< string, string > headers, HTTPRequestComplete onComplete )
	{						
		WWW www = new WWW( requestString, data, headers );
		yield return www;
		
		HandleHTTPRequestCompletion( www, onComplete );
	}
	
	private static void HandleHTTPRequestCompletion( WWW www, HTTPRequestComplete onComplete )
	{
		if( www.error != null )
		{
			#if UNITY_EDITOR
			Debug.Log( "[Network] Error: " + www.error );
			#endif
		}
		else if( onComplete != null )
		{
			onComplete( www.text );
		}
	}
	
	private static MonoBehaviour DummyBehaviour
	{
		get
		{
			if( s_dummyBehaviour == null )
			{
				s_dummyObject = new GameObject();
				s_dummyBehaviour = s_dummyObject.AddComponent< MonoBehaviour >() as MonoBehaviour;
				s_dummyObject.name = "DummyObjectForNetworkingCoroutines";
				s_dummyObject.transform.position = -Vector3.one * 10000000f;
			}
			
			return s_dummyBehaviour;
		}
	}
	
	private static GameObject s_dummyObject = null;
	private static MonoBehaviour s_dummyBehaviour = null;
	
}
