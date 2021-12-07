# Release Notes
===============


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
