# Kitsune Corner (Working title)
> Narrative focussed roguelite with store management.

https://github.com/pmuenjohn/roguelite  
This is the birds-eye-view Design Documentation for Kitsune Corner (Working title).
For more in-depth or implementation specific documentation, see the [Manual](https://pmuenjohn.github.io/roguelite/manual/overview.html) (also in the navbar above) for details.

To edit this document please only edit the README.md file, the github-pages will be updated automatically after you commit README.md, and use [Github Flavored Markdown (GFM)](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/quickstart-for-writing-on-github)

## Overview
![provisional_logo](resources/provisional_logo.png)

Play as a shape-shifting Kitsune who runs a Melbourne based pencil store by day, and hunts for various trinkets and supplies during the night.

Build relationships with new customers that arrive at your store each day. Listen to their stories and serve them their favourite items to learn new abilities and unlock new recipes.


## Team

- Writer/Producer: Audrey He 103597518
- Programmer: Peamawat Muenjohn 102326287
- Programmer: Ricky Huo 101453511
- Artist 2D/3D: Brittany Holmes 101116966
- Designer: Ethan Babatsikos 103619218

## Platforms

The game will be built for PC to be played on mouse and keyboard, however the controls are designed to easily map and copy onto a controller for players who prefer a hand held experience.  
Various successful roguelikes have been released and made available on PC and the Nintendo Switch.  
These include The Binding of Isaac, Hades, Risk of Rain 2, Enter The Gungeon. 

The controls in the game will be programmed onto mouse and keyboard giving players easy and comfortable ergonomic access to use all available abilities without straining their hands. 

In a research report done by DFC Intelligence, it is found that approximately 48% of all gaming happens on PC. Releasing this game on PC gives access to a wider player base.


## Machinery

Players will play as a Kitsune in human form during the day time and a fully transformed nine-tailed fox at night who goes out to hunt.

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

Kit has nine abilties corespondent to her nine-tails. As Kit is a fox, she has the abilities to dig into the ground where dirt is present to avoid enemies and act as a invulnerable ability.

There will be various enemies within the game with unique abilities and movesets. The main form of enemies are known as wisps. Enemies will drop items that can be brought back to the shop and used as equipment and ingredients for the pens. For example, blue enemies will drop blue pigments that can be used for blue ink. Kit will also have to battle the final boss who has an upgraded moveset and power level compared to other and previous enemies.

After each level is completed, random drops will occur. This can include Modulars, fish and power-ups. Fish can be used to exchange goods with Hebert the bear, who is a lonely shop keeper. Herbert randomly spawns in between levels and has a small stall where goods can be exchanged given the player has enough fish. Power-ups can also randomly drop which powers up Kit's abilties and movesets.

The levels are all randomly generated based on locations of Melbourne. There is no set order that the level come and go. As the levels are based on Melbourne locations, the traps are also fixated on distinguishable Melbourne attributes such as tram traps. Trams go pass in levels that are applicable and can collide with Kit and the enemies. Collison will result in being damaged and stunned as well as pushed.

#### Hub/Storefront Gameplay Overview

The gameplay during this time will mainly revolve around making stationary for the customers based on their preferences.

This includes using various components of a pen such as pigment colour, body build and thin/thickness to create the pencil of the customer's desires.

Players also interact with the customer learning around their own stories as the game progresses. The relationship and story you build with the customer may be affected by your choices as well as fulfilling their needs.  
Building a strong relationship with the customer can benefit the player's combat and stats at night. 

Customers will pay with the in-game world currency known as Modulars. Modular coins can be used to upgrade the shop game-play wise or simply its appearance.

### Systems
(TODO: link to and write manual pages)

#### Scene Loading

#### Narrative System

#### Storefront Gameplay

#### Map Generation System

#### Combat System

Combat will be majoritly based on shooting projectiles at enemies, but you will also have a melee ability at your disposal. As the game progresses, you will unlock a total of 8 different projectiles. Each weapon represents a tail of the Kitsune, and will do the following:
```
Regular tail: Melee weapon.
Lightning Chain: Hits multiple enemies for chain damage (lock on).
Fire: Inflicts burn damage.
Ice: Slows enemies.
Earthquake: Inflicts impact damage on enemies in a straight line.
Poison: Reduces enemies total health by a percentage.
Piercing: Can hit multiple enemies (aimed).
Boomerang: Will inflict damage on initial release and when received.
Cupids Arrow: Will link enemy that's hit with the next closest enemy, any damage done to one will be inflicted on the other.

```

When attacking an enemy for the first time upon entering a room, you will attach your cast to the enemy you hit. Your cast will increase damage inflicted on that enemy, and your cast will be dropped once the enemy is defeated. You can regain your cast by picking it up after the enemy you affected is defeated or by entering the next room. You may not change the enemy that your cast is on until it is defeated.


#### Perk/Ability System

As you progress through the game and create bonds with the NPCs, you will gain passive perks and abilities. Each NPC will be able to give you a passive perk that you will keep throughout the game and will stack with other passive perks you may receive from other NPCs. When you complete a relationship with an NPC, you will receive an ultimate ability. You may unlock multiple ultimates, but you may only equip one at a time. These abilities and perks will be active in every run, and will stay with you when you die. 


#### Pen-Building system

Player must walk to their workbench and interact with it. It will open the pen-build UI. It will have a
work bench and tools. Tools include drill, hammer, saw, clips, and water. The drill can be used to
shape the pen body for plastic and aluminium. For wooden pens, a saw must be used. The pen ink
can be made from pigments simply by applying water. For the tip of the pen, clips must be used for
the materials. To assembly a full pen with all the right materials, a hammer must be used.
For the actual game play, players can drag the materials needed onto the work bench. They can then
leave the material on the work bench and drag the tool needs over the material which will
automatically apply the “building” animation resulting in the finished product. E.g, if the player wanted to make a blue plastic felt tip pen, they would need to drag the blue
pigment onto the work bench along with water on the work bench to make the ink. They will then
need to drag the plastic onto the work bench as well and drag the drill to make the body. Then they
will do the same with feathers and clippers. After all the materials are made, if they left all the made
materials on the work bench, then all they need to do to overlap the materials and drag the hammer
to make the pen. Double click on items to obtain them. 

### Currency systen

#### Modular

The world currency that customers use to pay for their goods. Modular can also be used to
upgrade the hub (shop). The shop can be upgraded with aesthetic changes (e.g different coloured
wallpaper and flooring). The shop can also be upgraded so that the pen making game play can be
made easier.

Obtainability:

• Making correct items for customers (some customers will pay anyways depending on the
customer and your relationship with them)

• Herbert the bear

• Completing levels

#### Fish

Fish can be used for Herbert the bear to purchase his shop items. He has a variety stall of items
including Modulars, Power-ups (boons) and enemy drops.

Obtainability:

• Destroying crates that are spawned around the world.

• Completing a level around the Yarra will have a 40% chance of dropping fish at the end of it.

#### Enemy drops

Colour Pigments: Blue, black, green, yellow, purple, red, pink.

Materials: Plastic, Wood, Aluminium, Feathers, lead.



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

Kit was an unnamed Kitsune who was given their name by Winnie. She was physically cursed into a room due to her past action that brought shame upon the Deity. She decided to revamp the room and refurnish it into a shop, pen shop to be exact. Believing pens have different uses and variations which to unique and special to everyone. Felt tips, ball-ink, gel-ink, Kit knew it all. She was happy running her shop, she found that as a Kitsune she was able to escape her physical body and roam as a soul at night. She discovers from Schrodinger that you can unbind yourself if you prove yourself worthy top the Deity. Kit initially has no interest in this as she is content with staying in her shop.

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
