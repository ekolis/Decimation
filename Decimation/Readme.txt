DECIMATION
or, ZERO HERO DAY
or, THE ROGUELIKE THAT YOU CAN PLAY USING ONLY YOUR NUMPAD
or, ED KOLIS' 2009 7DRL ENTRY

STORY
You are the number zero.
Being the number zero, you are a nihilist.
This means you want to wipe out all the other impure, barbaric numbers from the face of the Cartesian plane.

THE GAME WORLD
The game world is very simple: a 20 by 20 grid.
There are no obstacles - just you (the zero) and the monsters (the other digits from 1 to 9).

THE HERO
As stated above, you are the number zero.
You start with 100 / 100 rationality points which are your HP.
When you are attacked by an adjacent monster, you lose some rationality.
Should your rationality reach zero or below, you lose the game.
You are slower than all the monsters (you move once every ten game ticks; see THE MONSTERS for details on monster speeds).
Thus it is important to avoid letting other monsters see you until you're done dealing with the ones that can already see you.
You have no melee attacks; instead, you have four special attacks which affect all adjacent monsters with an arithmetic operation.
See COMMANDS for details on commands.

THE MONSTERS
Monsters are the other digits from 1 to 9.
Monsters that can see you will seek after you and attack you.
Monsters that cannot see you will mill about randomly.
Monsters' sight range is equal to their number.
Thus, a one can see you only if you're standing right next to it, but a nine can see you about halfway across the map!
If a monster can see you, it is displayed in red; otherwise it is displayed in yellow.
(The zero and any empty tiles are displayed in brown if they are in sight range of any monster, and white if they are not.)
Monsters' attack power is XdX, where X is the number of the monster.
Thus, a one does 1d1 (1) damage per hit, a five does 5d5 (5 to 25) damage per hit, and a nine does 9d9 (9 to 81) damage per hit.
However, lower numbered monsters are pests which move very quickly!
While the hero moves every 10 game ticks, monsters move and attack according to their number, so a 5 is twice as fast as the hero, and a 1 is ten times as fast!

COMMANDS
All commands in Decimation are performed using the numeric keypad.
The following commands are available:
1, 2, 3, 4, 6, 7, 8, 9: Move in various directions. (Note that you cannot use 5 or . to sit still and wait for the monsters to come to you; you have to at least side-step!)
0: Show a help dialog.
+: Attack all adjacent monsters with Addition. (See COMBAT for more details).
-: Attack all adjacent monsters with Subtraction. (See COMBAT for more details).
*: Attack all adjacent monsters with Multiplication. (See COMBAT for more details).
/: Attack all adjacent monsters with Division. (See COMBAT for more details).

COMBAT
The hero has four attacks with which to attack all adjacent monsters.
The goal is to transform the numbers so that they equal zero.
Whenever a monster reaches zero, it disappears and the hero regains half the rationality (HP) that have been lost in combat (rounded down).
Thus, if the hero is at 75 / 100 HP, and defeats a monster, the hero's HP will increase by 12 to 87 / 100.
The attacks are as follows (note that if a number reaches 10 or above, the number is taken "modulo 10", that is, all but the last digit are removed):
Addition (triggered by + key on numpad): Adds three to adjacent numbers. Not generally useful, but can weaken an eight or nine, or defeat a seven.
Subtraction (triggered by - key on numpad): Subtracts one from adjacent numbers. Useful for dealing with small numbers; will take too long to wear down larger ones.
Multiplication (triggered by * key on numpad): Multiplies adjacent numbers by themselves. Does nothing to a five or six, but is devastating to a nine!
Division (triggered by / key on numpad): Divides adjacent numbers by two, rounding up. Useful for quickly weakening large numbers.
Monster to player combat is explained in the sections THE HERO and THE MONSTERS.