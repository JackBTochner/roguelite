# Kitsunetic
> Narrative focussed roguelite with store management.

https://github.com/pmuenjohn/roguelite  
This is the birds-eye-view Design Documentation for Kitsune Corner (Working title).
For more in-depth or implementation-specific documentation, see the [Manual](https://pmuenjohn.github.io/roguelite/manual/overview.html) (also in the navbar above) for details.

To edit this document please only edit the README.md file, the github-pages will be updated automatically after you commit README.md, and use [Github Flavored Markdown (GFM)](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/quickstart-for-writing-on-github)

## Overview
![provisional_logo](resources/provisional_logo.png)


Play as a shape-shifting Kitsune who runs a Melbourne-based cyber-repair store by day, and hunts for various materials and supplies during the night.

In this early version of the game built for PAX, talk to your customers who arrive every day with new dialogue. Venture out into the night as a nine-tailed fox and experience levels designed to imitate the streets of Melbourne CBD. Battle waves of fast-paced enemies, pick your upgrades after each wave to power up your character to face the final boss! Or return back to the shop and try again tomorrow night..


## Team

- Writer/Producer: Audrey He 103597518
- Lead programmer: Peamawat Muenjohn 102326287
- Programmer: Ricky Huo 101453511
- Artist 2D/3D: Brittany Holmes 101116966
- Programmer: Felix Xu 32498535

## Platforms

The game will be built for PC to be played on mouse and keyboard, however the controls are designed to easily map and copy onto a controller for players who prefer a handheld experience.  
Various successful roguelikes have been released and made available on PC and the Nintendo Switch.  
These include The Binding of Isaac, Hades, Risk of Rain 2, Enter The Gungeon. 

The controls in the game will be programmed onto the mouse and keyboard giving players easy and comfortable ergonomic access to use all available abilities without straining their hands. 

In a research report done by DFC Intelligence, it is found that approximately 48% of all gaming happens on a PC. Releasing this game on PC gives access to a wider player base.


## Machinery

Players will play as a Kitsune in human form during the daytime and a fully transformed nine-tailed fox at night who goes out to hunt.

> During the day, players run a pencil shop where NPC customers arrive to purchase writing goods.
> 
> During the night, they will fight through a randomly generated route of combat encounters, gaining abilities and resources along the way.

To activate night-time, players can close their shop to time-skip to night.



### Macro Gameplay Loop

![Alt text](resources/Macro_GameplayLoop.png)

[Link to diagrams on Miro](https://miro.com/app/board/uXjVMZTNF-k=/?share_link_id=762415482083)

The hub scene acts as the vehicle for the main narrative, whilst the combat acts as the main gameplay pull. 

### Micro Gameplay Loops

#### Roguelite Gameplay Overview

Kit has nine abilities corresponding to her nine-tails. As Kit is a fox, she has the ability to dig into the ground where dirt is present to avoid enemies and act as an invulnerable ability.

There will be various enemies within the game with unique abilities and movesets. The main form of enemies are known as wisps. Enemies will drop items that can be brought back to the shop and used as equipment and ingredients for the pens. For example, blue enemies will drop blue pigments that can be used for blue ink. Kit will also have to battle the final boss who has an upgraded moveset and power level compared to other and previous enemies.

After each level is completed, random drops will occur. This can include Modulars, fish and power-ups. Fish can be used to exchange goods with Hebert the bear, who is a lonely shop keeper. Herbert randomly spawns in between levels and has a small stall where goods can be exchanged given the player has enough fish. Power-ups can also randomly drop which powers up Kit's abilties and movesets.

The levels are all randomly generated based on locations of Melbourne. There is no set order that the level come and go. As the levels are based on Melbourne locations, the traps are also fixated on distinguishable Melbourne attributes such as tram traps. Trams go pass in levels that are applicable and can collide with Kit and the enemies. Collison will result in being damaged and stunned as well as pushed.

#### Hub/Storefront Gameplay Overview

The gameplay during this time will mainly revolve around making stationery for the customers based on their preferences.

This includes using various components of a pen such as a pigment colour, body build, and thin/thickness to create the pencil of the customer's desire.

Players also interact with the customer learning around their own stories as the game progresses. The relationship and story you build with the customer may be affected by your choices as well as fulfilling their needs.  
Building a strong relationship with the customer can benefit the player's combat and stats at night. 

Customers will pay with the in-game world currency known as Modulars. Modular coins can be used to upgrade the shop's game-play-wise or simply its appearance.

### Systems
(TODO: link to and write manual pages)

#### Scene Loading

#### Narrative System

#### Storefront Gameplay

#### Map Generation System

#### Enemies
Basic Wisp: The Basic Wisp is our most common type of enemy, spawning the most frequently and having the most basic attack. The Basic Wisp locks onto the player's position and shoots a slower moving projectile in a straight line towards the player. The Basic Wisp has 200HP and does 10 damage per shot, taking 2 hits to be killed and 10 hits to kill the player. The Basic Wisp moves at 60% of the player's movement speed.

Exploding Wisp: The Exploding Wisp is our ‘glass cannon’ enemy, doing large amounts of damage, but in return is weak and dies on detonation. The Exploding Wisp quickly makes its way to the player, and when overwhelmed, can cause the player serious damage. The Exploding Wisp has 100HP and does 40 damage on detonation, taking 1 hit to be killed and 3 detonations from separate Exploding Wisps to kill the player. The Exploding Wisp moves at 80% of the player's speed.

The Turtle: The Turtle Wisp is a shield-type enemy that can’t be killed without special playstyle consideration. The Turtle can only be damaged by a dig attack, forcing the player to conserve stamina and make use of our unique dig mechanics. The Turtle is slower and doesn’t do as much damage as other wisps, but its tough shell makes it more complex to evade and defeat. The Turtle has 200HP and does 15 damage per melee attack, taking 2 hits to be killed and 7 hits to kill the player. The Turtle moves at 15% of the player’s movement speed.

Ground Slam Wisp: The Ground Slam Wisp is a tankier wisp that moves slowly, but can pack a punch if you get in its way. The Ground Slam Wisp will jump up and ground slam attack the player 3 times before becoming a melee only tank.The Ground Slam Wisp has 600HP and does 30 damage when landing on the player, as well as hitting for 20 damage with a melee attack. It will take 6 hits for it to be killed by the player and has to hit 3 slams and a melee to kill the player. The Ground Slam Wisp moves at 10% of the player’s movement speed


#### Enemy Spawn System

 The enemy spawns will vary in every encounter you enter. Majority of enemy spawns will be random, within set conditions. The conditions are as follows:
 
 • Each enemy will receive a rating on how difficult it is to defeat, for example a minor wisp may have a difficulty rating of 1, and an explosive wisp may have a difficulty of 3.
 
 • Each room will receive a rating, similarly to the enemies. For example, a less difficult room may have a rating of 10, and a more difficult room with a rating of 50.
 
 • Each room will randomly spawn a set of enemies, that ratings add up to equal the set rating of the room you've entered. So if the room is a 10 rating, including the 2 enemies from the previous example, there would be 4 possible enemy combinations you could encounter:
 
 ```
 3 explosive wisps and 1 minor wisp = 10 points
 2 explosive wisps and 4 minor wisps = 10 points
 1 explosive wisp and 7 minor wisps = 10 points
 10 minor wisps = 10 points
```

#### Combat System

Combat will be majority based on shooting projectiles at enemies, but you will also have a melee ability at your disposal. As the game progresses, you will unlock a total of 8 different projectiles. Each weapon represents a tail of the Kitsune, and will do the following:
```
Regular tail: Melee weapon, deals damage in a small cone radius, swinging right to left.
Lightning Chain: Hits the nearest 3 enemies in a chain  formation (nearest enemy will get hit for a higher damage output than the furthest enemy in the chain).
Fire: Inflicts burn damage over time, (eg. 5 damage ticks per second, lasting 3 seconds).
Ice: Slows enemies for a small period of time.
Earthquake: Earth rises from the ground, inflicts impact damage on enemies in a straight line.
Poison: Reduces enemies total health by a percentage when hit (minor enemies will be affected by this attack more than major enemies).
Piercing: Can hit multiple enemies in a straight line (aimed).
Boomerang: Will inflict damage on initial release, will return to sender and deal a second wave of damage on the way back..
Cupids Arrow: Will link enemy that's hit with the next closest enemy, any damage done to one will be inflicted on the other.

```

When attacking an enemy for the first time upon entering a room, you will attach your cast to the enemy you hit. Your cast will increase damage inflicted on that enemy, and your cast will be dropped once the enemy is defeated. You can regain your cast by picking it up after the enemy you affected is defeated or by entering the next room. You may not change the enemy that your cast is on until it is defeated.

#### Projectiles

Ice - Slow: The Ice projectile’s main purpose is to buy the player time. When swarmed by enemies, if used effectively, the ice projectile can slow the toughest of enemies and save the player. The Ice projectile lasts 3 ticks (3 Seconds), then the enemy hit will return to regular speed.

Burn - Damage: The Burn projectile allows the player to have extra damage output. The extra damage becomes incredibly useful when trying to take down larger enemies quicker, or lesser enemies instantly. The Burn projectile does an additionally 10 damage per tick for 3 ticks (30 damage total). After 3 ticks the enemy no longer takes burn damage, unless hit again by a burn projectile.

Earthquake - AOE: The Earthquake projectile allows the player to deal extra damage to a larger number of enemies if used effectively. When fired, the earthquake projectile creates a straight trail of earth that comes out of the ground, dealing significant damage on impact. The Earthquake projectile creates a line, 10 tiles long of earth, each doing 10 damage. If all tiles hit an enemy, it totals to 100 damage in 1 shot.

#### Pick-up Upgrades

Ice Upgrades:
 1 - Increase Slow Duration - This upgrade doubles the amount of time the enemy is slowed for (6 seconds instead of 3)
2 - Freeze Enemies - When hit by the Ice projectile, instead of being slowed, the enemy will freeze in place indefinitely.
3 - Damage Over Time - When an enemy is hit by the Ice Projectile, they will now be slowed as well as take 5 points of damage every second, for 3 seconds. (Unless player also has Increase Slow Duration, 6 seconds)

Burn Upgrades: 
1 - Increase Crit Chance by 5% - This upgrade increases the base crit chance of 10% by 5% (15% total crit chance with this upgrade). This allows the player to have a better chance of dealing more damage over time. 
2 - Shotgun Spread, 3 projectiles - The shotgun style spread allows the player to shoot out 3 burn projectiles at once. If all projectiles hit, this upgrade allows triple base damage and triple burn damage, totaling 390 damage. 
3 - Damage Increase - This upgrade gives a 10% increase to your damage. (110 damage per hit, 11 damage per burn tick)

Earthquake Upgrades:
1 - AOE+ Cone Shape - AOE+ will change the shape of the earth trailing the projectile. Instead of being a straight line being 10 tiles long, it becomes a cone shape, spanning 5 tiles long, but having +1 tiles each tile it moves forward. 1:2:3:4:5 tile spread, 15 total tiles covered.
2 - Increase Knockback + Damage - This upgrade increases the damage of the earthquake projectile impact and AOE effect. Damage of the initial projectile impact will increase by 30 and each AOE tile damage by 5. Knockback is also increased by 100%.
3 - Stuns Enemies - This upgrade will stun any enemy that is hit by the projectile itself or the AOE trail left behind it. Any enemy hit will not be able to move for 3 seconds. After 3 seconds, the enemies hit will recover and return to normal.


#### Perk/Ability System

As you progress through the game and create bonds with the NPCs, you will gain passive perks and abilities. Each NPC will be able to give you a passive perk that you will keep throughout the game and will stack with other passive perks you may receive from other NPCs. When you complete a relationship with an NPC, you will receive an ultimate ability. You may unlock multiple ultimates, but you may only equip one at a time. These abilities and perks will be active in every run, and will stay with you when you die. 

*This aspect is now scrapped*


#### Cyber repair system (Once Pen building system)

Player must walk to their workbench and interact with it. It will open the pen-build UI. It will have a work bench and tools. Tools include drill, hammer, saw, clips, and water. The drill can be used to shape the pen body for plastic and aluminium. For wooden pens, a saw must be used. The pen ink can be made from pigments simply by applying water. For the tip of the pen, clips must be used for the materials. To assembly a full pen with all the right materials, a hammer must be used. For the actual game play, players can drag the materials needed onto the work bench. They can then leave the material on the work bench and drag the tool needs over the material which will automatically apply the “building” animation resulting in the finished product. E.g, if the player wanted to make a blue plastic felt tip pen, they would need to drag the blue pigment onto the work bench along with water on the work bench to make the ink. They will then need to drag the plastic onto the work bench as well and drag the drill to make the body. Then they will do the same with feathers and clippers. After all the materials are made, if they left all the made materials on the work bench, then all they need to do to overlap the materials and drag the hammer to make the pen. Double click on items to obtain them.

*This aspect is now scrapped*

### Currency system

#### Modular

The world currency that customers use to pay for their goods. Modular can also be used to
upgrade the hub (shop). The shop can be upgraded with aesthetic changes (e.g different coloured
wallpaper and flooring). The shop can also be upgraded so that the pen making game play can be
made easier.

*This Aspect is now scrapped*


Obtainability:

• Making correct items for customers (some customers will pay anyways depending on the
customer and your relationship with them)

• Herbert the bear

*This Aspect is now scrapped*

• Completing levels

*This Aspect is now scrapped*


#### Fish

Fish can be used for Herbert the bear to purchase his shop items. He has a variety stall of items
including Modulars, Power-ups (boons) and enemy drops.

Obtainability:

• Destroying crates that are spawned around the world.

• Completing a level around the Yarra will have a 40% chance of dropping fish at the end of it.

*This Aspect is now scrapped*

#### Enemy drops

Colour Pigments: Blue, black, green, yellow, purple, red, pink.

Materials: Plastic, Wood, Aluminium, Feathers, lead.

*This aspect is now scrapped*


## Brand Identity

Kitsune Corner is fully designed and produced in the heart of Melbourne. Therefore, the setting of the game is also fully based in a cyberpunk, synthwave futuristic fantasy version of Melbourne Victoria.  
Unlike other Melbourne based games, Kitsune Corner aims to integrate various Melbourne street culture and location within the game. All the levels in the game will be various locations of the Melbourne CBD including famous iconic spaces such as Federation Square, Southbank Walk and Hozier Lane. There will also be less known locations that are still a symbol of Melbourne such as The Block Arcade.

According to the UNESCO Institute for Statistics, in 2018 the United States of America accounted for 24% of global cultural creative export. This includes many forms of entertainment like Films, video games, music and other media items. This means that various of these contents display American landmarks and locations. Australia is only known for 6.5% of contribution in the creative and cultural industries in 2019. 

The game will also comprise characters from different cultural folklore. Melbourne is a very diverse and multicultural city with a significant population of people from different cultural backgrounds. In fact, almost half of the population in Melbourne are either born overseas or have at least one parent who was born overseas (Australian Bureau of Statistics 2016 Census). Having these different cultural characters will highlight the city's multiculturalism.

Kitsune Corner aims to impress players with the games take on the cyberpunk aesthetic and its hand drawn 2D illustrations. The game also aims to generate connectivity within Australian players, particularly those who reside in Melbourne and display the identical levels to the locations of Melbourne.


## Schedule

![Capstone Project: JIRA Schedule](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/ab8c5bd4-1259-47ab-8090-a1f2f89e3002_rw_1920.png?h=4669792ed5c773a4a054dab251f89ecb)

https://gamecapstone2023.atlassian.net/jira/software/projects/CP/boards/1

## Narrative

Kit was an unnamed Kitsune who was given their name by Winnie. She was physically cursed into a room due to her past action that brought shame upon the Deity. She decided to revamp the room and refurnish it into a shop, cyber repair shop to be exact. Believing everyone has different reasons for the way their body works, she wants to be a trusted technician for all those around. She was happy running her shop, she found that as a Kitsune she was able to escape her physical body and roam as a soul at night. She discovers from Schrodinger that you can unbind yourself if you prove yourself worthy top the Deity. Kit initially has no interest in this as she is content with staying in her shop.

Conflict: Wisps originally have always been around but as Kit progresses through the game, more and more powerful wisps are starting to show up almost overpowering other hostile creatures. She discovers that there is a wisp summoner who self-proclaims himself as the King of Wisps who takes control of the streets. Kit is interested in finding out more about them.

She becomes determined to stop them as they are making her hunting process more difficult. The wisp summoner gains the ability to take souls from the living and convert them into wisps along with their physical form from the blue stone (which Kit also obtains and becomes stronger). He entraps Schrodinger and turns him into a wisp to become his ultimate vessel. Kit must also stop him before Schrodinger becomes a wisp forever with no return. 

At some point, Kit also discovers that the Wisp Commander is also physically trapped somewhere, and he is also just a walking spirit. The initial boss fight was only Kit fighting his spirit but the final boss fight with him is Kit fighting his physical body. Both Kit and the Wisp Commander must get to each other’s physical body before the other one does.

### Characters

List of all Characters:  
```
Kit: Player  
Winnie: Wisp sidekick/guide  
Schrodinger: Customer  
Chang-E: Customer  
Banshee: Customer  
Horus: Customer  
Milton: Customer  
Aziel: Main enemy/Final boss  
Herbert: Upgrade keeper 
```
Schrodinger

Schrodinger is a stoic, intelligent and mysterious person who hides their feelings. They have an extremely calm demeaner up front, but behind this they seem to be easily emotionally upset. They have a room mate who is named Milton who they seem to be very fond of. Schrodinger was known to be a university student but dropped out due to unknown circumstances. Now they gain an income by gambling, winning most of the bets they partake in. Schrodinger discovered this talent when they betted on the liveability of an animal inhaling deadly gas.

Personality: ISTP

Appearance: Fluffy short hair, cat ears, usually has their hands in pockets. Oversized clothing especially the jacket.

Chang-E

An astronomy enthusiast, Chang-E knows everything there is about the moon. She took a special interest in the moon when she was younger and was separated from her parents on holidays. The full moon was out that day and she used it and its bright light as a guide to find her way back to her parents. Ever since then she believed that her and the moon have a strong connection. She is also highly interested in astrology and believes that it greatly has an impact on people and their personalities.

Personality: INFJ

Appearance: Long black hair with two space buns (pins in the space buns), flowy Chinese-traditional inspired outfit but modernised.


Banshee

Being gifted with the voice of an angel, Banshee wows those are listens. She moved to Melbourne from Ireland to stay aboard and makes a living busking everyday on the streets of Melbourne. She has the power to hear people’s spirits and their ‘song,’ being able to perfect her pitch and harmony to please them. Having this power, she wants to use it to better peoples lives and bring harmony together. 

Personality: ESFJ

Appearance: Green jacket, like a flowy fashionable cloak over a white dress. She has red hair. Either has red eye makeup or just red eyes.


Summoner of wisps Aziel

Stealing souls at a long attempt to bring his dead sister back, Name longs to grab the attention from the Diety believing that becoming more powerful with wisps will force the to release him and return his sister. But name needs to accept his sister fate in order to be released. Now transformed into a dark, powerful, and unrivalled entity, name stand as the King of all wisps controlling the streets with his vessels. 

Personality: INTJ

Appearance: Dark long hood with fast to cover top half of face, one eye clearly red.

Horus

A former pilot, Horus flew planes around the world carrying massive amount of passengers. He fell in love with his job and flying freely in the sky. One fateful day, he has received the news that his wife had Fuchs dystrophy in her left eye and needed immediate cornea transplant. With no available donors, Horus gave up his left eye along his career and passion to save her. Now with one eye to use, he is unfit to fly a plane. He was perished from his job. From this, he decided to travel instead to continue experience the world from the sky.

Personality: INTP

Appearance: One eye (right eye remaining), coat, lots of gold ascents. Some bird features maybe like mini wings.


Winnie

An authentic wisp, Winnie was transformed with full free will and a mind as intelligent as a living being. While she doesn’t remember her past life, she does have the privilege to roam and live as she pleases. She hides out in a run down shop and claimed that as her own home. One day something happened, a being was casted into the shop, Winnie saw that this being was different and decided to take it upon her own hands to help them. She continues to act as Kit’s close friend and mentor.

Personality: ENFP

Appearance: Wisp, more feminine

## Level Design
City Map Initial Concepts:
![Kitsune Corner Map](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/1de79ef3-1369-4311-911d-1ef7ae228fbc_rw_1920.png?h=d9c9a3af7ba90fa8bc435944aa03ba0e)

## Character Sprites
Below are the current characters that the player can interact with in the game, and Kit herself.
<p>
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/1752f3d8-1e74-40d6-bf9f-89eeb6bf14ee_rw_1200.png?h=c205fc7306f5bee183a9a07cb17fe825" width="250" />
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/8e8a0f59-2e70-44d6-b015-90d7cc6170cf_rw_1200.png?h=90e1f8d2d2ac3fe7edaa4491490f3a5a" width="200" /> 
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/540dfafa-7343-4349-9908-9e2fc41686f2_rw_1200.png?h=b0f9d6b196a39792ccbb2d3a9010111e" width="250" />
</p>

<p float="left">
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/39b5c971-0756-4c18-85ff-6379d6a4de6f_rw_1200.png?h=b4d23837d55cc56895b0a11c2b27cd27" width="250" />
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/8fbd08e2-f79d-4e49-894f-9227a31e266a_rw_1200.png?h=9db22bc2cd83d9843927bdbb4d2b34ad" width="250" /> 
</p>

## 3D Animation
Below are the animations for Kit in her fox form. They currently include an Idle, a Run animation, a dig exit and entry, and a melee.
<p>
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/9abb292c-a3e9-49d1-b2b0-e0ffec701eec_rw_600.gif?h=8d1b36f88539d82c7b585100727117fd" width="450" />
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/e41c038c-3a82-4669-a205-9f9999b04089_rw_600.gif?h=2e8750c0b727659a51646d4f4f127b19" width="450" /> 
</p>

<p>
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/8a4ace1f-8168-443f-a596-f44e530d96b9_rw_600.gif?h=60872c2f4f68743e1c0516211a7b48c7" width="450" />
  <img src="https://cdn.myportfolio.com/09407fbb-bb14-4b8b-b59f-0cd585ad972e/b069ec26-c8bc-4dcc-8ce5-421a47d718fb_rw_600.gif?h=4a8e1951f691cd73c3bc6bb45717efa1" width="450" />
</p>

## Concept Art

Below are the character concepts completed by our 2D artist:
### Character Art
#### Kit

Kit Outfit Designs:
![KIT OUTFIT 1 Concept Art](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/19ed6fb4-ba7e-4314-920e-430a31e40f5f_rw_1920.png?h=b8a10fdecbe7a4e625e73b920cfd5e96)

![KIT OUTFIT 2 Concept Art](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/d3bf5b66-35bf-4f0e-a5a1-9d4b7952c4cc_rw_1920.png?h=e738b02c058297e73a4cce145f6dd9cf)

Kit Turnaround:
![FOX CONCEPT Turnaround](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/18615722-fd08-41c8-b81b-870a6541081c_rw_1920.png?h=372fbe633ad03cc15ddc9d0d34234030)

Kit (Fox form) Turnaround:
![FOX CONCEPT Turnaround](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/aad3a189-3246-46c4-a6d0-7273872063a1_rw_1920.png?h=f3a505eb38d5e52f74342f1905c546f9)

Kit Concept Art (Colourway 1):
![KIT Concept Art](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/18178751-955a-4ef9-9ba2-f2e6af06844c_rw_1920.png?h=6860a4b9cffbcc17721d7aee0aab8744)

Kit Concept Art (Colourway 2):
![KIT Concept Art](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/f2477c7a-0df9-42d9-aa4e-b3ae70e945e5_rw_1920.png?h=bd06c3e38a10a8fe3a7d24b82fd9093b)

#### NPCs
Chang-e and Shrodinger Character Art WIP.
![Chang-e and Shrodinger Character](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/5de2ec7c-f7ec-4584-99c6-d69f781aa6a7_rw_1920.png?h=68450987376cbaabc94f41d3e6335eb4)

### Ability and UI Art
Melee and Dig Ability Icons:
![Melee and Dig Ability Icons](https://cdn.myportfolio.com/7c2f05bf-f874-41c1-8d1a-1bfd2c2480a4/4ce8ef51-4d46-4ef0-99e4-b4c1db810edc_rw_1920.png?h=6a78b347ccf6993e22b1b3ea5043fe45)
