# Tasks
============

Resolve how puzzlemap is displayed in DCT ...
 * [done] need to ensure that North means 'top' of the display.
 * [done] change FloodFillView, PuzzleMap, Grid, DirEnum classes
 * [done] in DCT display, there is left and right rising .. is Left X or Y ?
 * from (0,0) left is (1,0) right is (0.1) .. so north is +1
 * need to invert map storage ( ? ) or switch 'north' delta


[done] Add action logic utilities:
* [done] tool to apply action-chains to create altered puzzlemaps
* [done] game state evaluation, are we done?  Is there more?
* expand on concept of agent goals and fulfillment


Switch to:
 * [done] AgentTurn ( named GameTurn )
 * [done] AgentId / Move-Sprint-Attack-Defend 
 * [done] MoveEnum-TargetDmg-NullBool
 * [done] with reversible description

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
