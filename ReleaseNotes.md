# Release Notes
===============

## 2021/12/30 :: 0.0.8-alpha

* Expanded enums, better association of traits to enums.
    
* Tools to support YAML production, and generic cloneable interface for deep-copy.

* Added CloneableIf<> for generic deep-copy.

* Added YamlTools and Yaml annotations to assets for consistent streaming.

* Game assets:
  * simplified PlayMap for game advancement
  * actions became GameTurn, which ActionHandler will apply to PlayMap

* Puzzle assets:
  * PuzzleMap extends PlayMap
  * Place uses AgentId instead of Agent ptr.  NO_AGENT_ID for empty place.

* Flood Fill views using PlayMap instead of PuzzleMap

* Brains using updated tooling to produce GameTurns.  Added CautiousBrain to ChaseBrain.

* PlayMap grid as strings logic migrated to PuzzleMapFactory.

* First draft of power analysis for agents, with framework for additional examinations.


## 2021/12/06 :: 0.0.7-alpha

* Removed net5.0 from <TargetFramework>.  
    It seems that having two versions in the DLL was confusing Unity.

* Add flood fill for pathing
  * FloodFillView.ToDisplay() puts north end on top ( first line )

* SimpleMind needs to select simple actions
  * move towards nearest foe ( based on pathing )
  * attack adjacent foes

* After incrementing version, figure out how to make the versioned library 
            available to other projects in my local environment.
  * added script to building to local repo using Nuget
  * storing 'prerelease' by using -action and a timestamp
  
END OF NOTES
