# Design Considerations
=======================

## Quick History on Intended Experience

The game is intended to be a puzzle despite it's tactical flavor.
Puzzles should allow the player to quickly try multiple approaches.
This means that the game will not constrain the flow of play to a single direction.
Here are what I hope to add to the game to simplify 'rollback' to previous states.

* In-memory play history.  Some UI in the game will permit rapid rollback to previous play states.
  The related question is, do I retain different play threads?  Or will rollback discard all moves?
  This is something that needs experimentation.

* In addition to keeping snapshots of the game at every step, I would like for the record of 
  actions to be reversible.  If needed, I can reconstruct a previous state by 'un-playing' 
  the previous moves.  This may prove problematic, as some actions inherently lose information.
  For example, if an attack is 'overkill', it may not be feasible to figure out the previous
  hit points of a victim.  ( unless damage and hit points are separate values, that might work ).

## Play Organization

The game progresses in rounds until one faction prevails.   

Each round is a series of faction rounds.  A faction round is one turn for 
each agent in the faction.  Each agent turn is two actions, some combination 
of move, attack, defend and sprint.
