# Tasks
============

Rules and traits:
 * can we ensure that they are being used? ( unit testing? )
 * can we attach information to them to be used as hover tips
 
 View/Floodfill
  * need to start from target downhill view to create 'action remains' view
  * 'action remains' has -1 for 'cannot enter', and 0 or 1 for actions left when entering.
  * support for range and reach ( reach = polearms, range = bows )
  * need line of sight view

Resolve how puzzlemap is displayed in DCT ...
 * from (0,0) left is (1,0) right is (0.1) .. so north is +1
 * need to invert map storage ( ? ) or switch 'north' delta

Add action logic utilities:
* expand on concept of agent goals and fulfillment

Use PlayMap, Brains and Actions in DragonCourtTactics.
Clean up interface til basic game play is functional.

Agent Traits:
* isolate agent from type, type just creates a set of attributes ( including name ).
* this will allow trait-wise analysis ( eg. add one trait to each fight to see the benefit )

Brain Improvements:
* need brain that takes advantage of terrain
* brain that targets vulnerable opponents

Analysis Improvements:
* need to deploy teams of agents ( 1-3 of the same type )
* need brain comparison analysis
* need trait comparison analysis
* terrain comparison analysis ?  ( is that a thing )



# Objectives
============

Mark all files with standard copyright notice

Find/build simple tool that handles mocking non-virtual, not interface methods.
* some exists, but cost money and/or use injection ( yuck )
* switch to using the stifling interface pattern. 
