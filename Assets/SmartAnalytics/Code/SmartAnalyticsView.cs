// Copyright (c) 2014 Cranium Software

using UnityEngine;
using System.Collections;

public class SmartAnalyticsView : MonoBehaviour
{
	public string Name = "(unnamed)";
	public string TrackingID = null;

	void Start()
	{
		if( !string.IsNullOrEmpty( TrackingID ) )
		{
			SmartAnalytics.SetTrackingID( TrackingID );
		}

		SmartAnalytics.SendView( Name );
	}
}
