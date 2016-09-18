// Copyright (c) 2014 Cranium Software

using UnityEngine;
using System.Collections;

public class SmartAnalyticsClickExample : MonoBehaviour
{
	void Update()
	{
		if( Input.GetMouseButtonDown( 0 ) )
		{
			SmartAnalyticsEvent analyticsEvent = GetComponent< SmartAnalyticsEvent >();

			if( analyticsEvent != null )
			{
				analyticsEvent.Report();
			}
		}
	}
}
