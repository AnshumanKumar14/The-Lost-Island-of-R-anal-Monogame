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


### Screenshots:
Example Enemy: 
![alt text](https://github.com/TheGeekiestOne/The-Lost-Island-of-R-anal-Monogame/blob/master/Screenshots/enemy.JPG "Enemy - Melee")

Boss: 
![alt text](https://github.com/TheGeekiestOne/The-Lost-Island-of-R-anal-Monogame/blob/master/Screenshots/Boss.PNG "Boss")


NPC That turns into a werewolf: 
![alt text](https://github.com/TheGeekiestOne/The-Lost-Island-of-R-anal-Monogame/blob/master/Screenshots/17499320_271797973261671_3633742176124042856_n.PNG  "NPC 2 ")
