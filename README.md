# The Lost Island of R'anal-Monogame
### Rpg Written in C# using the MonoGame framework
Planing on developing this further or moving to Unity? 
## Made WIth:

![alt text](https://camo.githubusercontent.com/7f8e73045428d031028bab0288434e344046dd77/68747470733a2f2f7365637572652e67726176617461722e636f6d2f6176617461722f37376563663066623964383431396265373731356336653832326536363536323f733d313530 "NLUA logo")

![alt text](https://github.com/TheGeekiestOne/The-Lost-Island-of-R-anal-Monogame/blob/master/Screenshots/monogame-logo.png "Monogame")

## Using the Entity Component System Setup:

#### What is an ECS?
An Entity Component System (ECS) is a way to build and manage the entities (or game objects) in your game by composing their component parts together. An ECS consists of three main parts:

#### Components
A component is simply a class that holds some state about the entity. Typically, components are lightweight and don't contain any game logic. It's common to have components with only a few properties or fields. Components can be more complex but inheritence is not encouraged.

#### Entities
An entity is a composition of components identified by an ID. Often you only need the ID of the entity to work with it. For performance reasons, and entity ID is only valid while the entity is alive. Once the entity is destroyed, it's ID may be recycled.

#### Systems
A system is a class that will run during the game's Update or Draw calls. They usually contain the game logic about how to manage a filtered collection of entities and their components.

Using components, we can pick and choose bits of behavior for each object we want
![alt text](https://cdn-images-1.medium.com/max/800/1*gObzr-U5tFUl-TJYmTxhow.png "ECS Layout")

Some components (like Position) are shared between systems. Other components can be private to a particular system, which allows for information hiding and greater optimization in memory layouts.

For example, let’s say that one scene in the game contains a thousand entities. Almost all of these entities contain a position, so we could just allocate a large 1000-element array for our position components, and store the position for each entity indexed by its entity ID. Since the array is almost completely populated, and we’ll want to reference entity positions on a regular basis, this is probably the most efficient way to store this information.

On the other hand, maybe only a relatively small amount of our scene’s entities have a velocity. In this case, we might use some other strategies to allocate just enough memory for the velocities we do have, and then map entity IDs into our velocities array.

Strategies like this let us potentially do a better job than a general-purpose GC or system allocator — although of course we should be careful and continually profile to make sure we’re actually benefiting from the extra complexity of managing our own memory.

## Further reading:

A list of useful data-oriented design resources.
- http://gameprogrammingpatterns.com/ — a discussion of other game programming patterns that might also be novel. The section on “data locality” is particularly relevant.
- Building a data-oriented entity system (part 1) — if you want to build an ECS framework yourself. http://bitsquid.blogspot.com/2014/08/building-data-oriented-entity-system.html
- Unity ECS documentation — if you want to build inside an ECS framework. https://github.com/Unity-Technologies/EntityComponentSystemSamples/blob/master/Documentation~/index.md

### Screenshots:
Example Enemy: 
![alt text](https://github.com/TheGeekiestOne/The-Lost-Island-of-R-anal-Monogame/blob/master/Screenshots/enemy.JPG "Enemy - Melee")

Boss: 
![alt text](https://github.com/TheGeekiestOne/The-Lost-Island-of-R-anal-Monogame/blob/master/Screenshots/Boss.PNG "Boss")


NPC That turns into a werewolf: 
![alt text](https://github.com/TheGeekiestOne/The-Lost-Island-of-R-anal-Monogame/blob/master/Screenshots/17499320_271797973261671_3633742176124042856_n.PNG  "NPC 2 ")
