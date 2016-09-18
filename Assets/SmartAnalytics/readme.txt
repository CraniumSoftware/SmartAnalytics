# SmartAnalytics

A platform agnostic wrapper for the Google Analytics Web API.

Allows sending events, views and timings to Google Analytics with the SmartAnalytics script API and providing components to be added to game objects for ease of reporting events and views.

This document is formatted following the conventions of Markdown to aid readability - you can use websites like http://markable.in/editor/ to view it in a more visually pleasing format than plain-text.

## Notes

For use with iOS it is important to not use the .NET 2.0 subset stripping option since this disables the network functionality required for this script to work.

If you do not set your Google Analytics tracking ID with SmartAnalytics.SetTrackingID or one of the game object components then your analytics will be sent to our testing account.

## Examples

There is an example scene included in the package under SmartAnalytics/Scenes/Example.scene

## Usage

The SmartAnalyticsEvent component provides one function:

### public void Report()

Call this function to report the event using data set in the inspector. For an example of use see SmartAnalytics/Code/SmartAnalyticsClickExample.cs

The SmartAnalytics API provides the following functions:

### public static void SetTrackingID( string trackingID )

Set the tracking ID from your Google Analytics account, e.g. SmartAnalytics.SetTrackingID( "UA-43926062-2" );

### public static void SetReferrer( string referrer ) { s_referrer = WWW.EscapeURL( referrer ); }

Set the referrer to track where events and views originate from

### public static void SendEvent( string eventLabel, float eventValue, string eventAction, string eventCategory )
### public static void SendEvent( string eventLabel, float eventValue, string eventAction )
### public static void SendEvent( string eventAction, float eventValue )
### public static void SendEvent( string eventLabel, string eventAction, string eventCategory )
### public static void SendEvent( string eventLabel, string eventAction )
### public static void SendEvent( string eventAction )

Report an analytics event

### public static void SendView( string name )

Report a view, e.g. SmartAnalytics.SendView( "Main Menu" );

### public static void SendTiming( string name, string category, string label, int timeInMS )

Report a timing in milliseconds
	

## Version History

### 1.0

- initial release
  - support for events, views and timings in SmartAnalytics class
  - SmartAnalyticsEvent for reporting events more easily
  - SmartAnalyticsView for reporting views more easily

### 1.0.1

- Unity 5.x support release
  - uses a dictionary instead of a hash table where some functionality was ‘deprecated’ and that causes build errors.
 
## Contact Us

info@cranium-software.co.uk

