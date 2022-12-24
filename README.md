<p align="center"><img src="Assets/Images/Энд.png" alt="Madness Cooking Logo" width="500"></p>

# Madness Cooking
The first game I created on the Unity engine for the summer of 2021. The plot as such was not originally envisaged, there was only the idea of combining the two
genres - cafe simulator and farm simulator. In the early stages of the idea, it was supposed to add a third to these two genres - RPG, but it was decided to abandon the idea due to
due to lack of skill and time. As a result, the game was actually created in a month and a half, and I can only call it a test of the pen, no more.

# The game

Link to the game page - https://nytrock.itch.io/madness-cooking-english-ver <br>
You can also download the latest release on github, unzip the resulting archive and run `Madness cooking.exe` in it
> If you encounter any bug, please file an [issue](https://github.com/Nytrock/Madness_Cooking_EN/issues).

There are also [Russian version](https://github.com/Nytrock/Madness_Cooking_RU).

# Main game locations

## Main menu
It is hardly possible to call the main menu a "location", but I cannot help but write about it. When you start the game, you get exactly here. If you have a save, you can continue
play on it, otherwise - start a new game. In the settings there is only sound control and a list of used soundtracks. The exit button does you know what.

## Cafe
The first location you'll go to on your first game. Here begins the tutorial, showing all the basic mechanics of the game. But now is not about that. So you have
the ability to open and close cafes for visitors, speed up time to reduce the waiting time for visitors. By the way, acceleration can be activated in any location
games, but it only works with respect to visitors. When a new visitor appears, you must find out his order and complete it in a limited time. With successful
when the order is completed, you receive money, the visitor eats the brought order for a while and only then leaves.

## Kitchen
The place where the list of orders from customers is located. If you have the right ingredients and equipment, the dish can be cooked, but it will take some time to cook.
There is also a mechanism for the gradual breakdown of equipment, so that periodically it will have to be repaired for in-game currency.

## Farm
On it you will grow all the necessary ingredients for dishes. There are standard beds on which you can grow most of the ingredients. The beds themselves can be fertilized
and water, thereby increasing the yield. Gradually, pests and weeds will appear on the beds, which must be disposed of in a timely manner.
At later stages of the game, it will be possible to improve the beds, for example, you can permanently protect the bed from pests and weeds, or make the bed always
watered. There is a shop that sells improvements for the farm. There is also a sublocation in this location - a barn. In it, you can get ingredients such as milk, eggs, flour, and also it is here that you can recycle
waste into fertilizer. The last thing not mentioned was the car. All collected products will be stored in it. At any moment it can be sent to the city,
because of which all the products in it will move to the kitchen. This action has a cooldown, so it needs to be used wisely.

## Office
The last but very important location. The main thing in it is a computer through which you can buy new dishes, ingredients, kitchen appliances, cosmetics for locations
and improvements, such as expanding the cafe, speeding up the work of equipment, etc.

# Run source code on local machine
If for some reason you want to dig into the source code or even improve it, then this instruction is for you.

- Install [Unity Hub](https://unity3d.com/en/get-unity/download)
- Clone repository

```shell
git clone https://github.com/Nytrock/Madness_Cooking_EN.git
```
- Open Unity Hub, click on the `Add project` button, select the folder where the repository was cloned
- Click on the created project and start studying the code
