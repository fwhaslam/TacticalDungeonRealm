# Analysis Support for DCT_Workshop
===================================

These classes support the DCT Workshop in performing analysis on the game design.

## Goals
=================

The goal of the analysis is to ensure that the game is challenging yet solvable.
This is my current definition of 'fun'.

The analysis will perform Agent vs Agent combat to determine the relative power of 
agent types, and to determine the relative effectiveness of the intelligent algorithms.
This will require the following from the model:

 * collection of maps with diverse and exemplary terrain features
 * collection of combatants, both individual and groups
 * brains of varying qualities to drive the agents.

The analysis will attempt to solve the puzzles using 

## Considerations
=================

This code does not need to reside within the RealmModel package.  At some point it 
should be broken out into a separate package.

Similarly, it would be useful to have a template from the Workshop to help build 
these classes.


## TODO
=================

Double fighter arena.   Like voting bloc power, how often does adding an agent 
to a 1v1 fight change the outcome ?
