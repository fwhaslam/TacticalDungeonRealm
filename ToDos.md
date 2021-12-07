# Tasks
============

[done] Removed net5.0 from <TargetFramework>.  
      It seems that having two versions in the DLL was confusing Unity.

[done] Add flood fill for pathing
* [done] FloodFillView.ToDisplay() puts north end on top ( first line )

[done] SimpleMind needs to select simple actions
 * [done] move towards nearest foe ( based on pathing )
 * [done] attack adjacent foes

[done] After incrementing version, figure out how to make the versioned library 
            available to other projects in my local environment.
* [done] added script to building to local repo using Nuget
* [done] storing 'prerelease' by using -alpha and a timestamp

Resolve how puzzlemap is displayed in DCT ...
 * need to ensure that North means 'top' of the display.
 * change FloodFillView, PuzleMap, Grid, DirEnum classes
 * in DCT display, there is left and right rising .. is Left X or Y ?
 * from (0,0) left is (1,0) right is (0.1) .. so north is +1
 * need to invert map storage ( ? ) or switch 'north' DY


Add action logic utilities:
* tool to apply action-chains to create altered puzzlemaps
* game state evaluation, are we done?  Is there more?
* expand on concept of agent goals and fulfillment


Switch to:
 * AgentTurn
 ** AgentId / Move-Sprint-Attack-Defend 
 *** [MoveEnum]-TargetDmg-NullBool
 * with reversible description

# Objectives
============

Mark all files with standard copyright notice

Find/build simple tool that handles mocking non-virtual, not interface methods.
