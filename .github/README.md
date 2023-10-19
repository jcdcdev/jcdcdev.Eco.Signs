## jcdcdev.Eco.Signs

[![Latest version on Github](https://badgen.net/github/tag/jcdcdev/jcdcdev.Eco.Signs?label=latest)](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/latest) [![Commits](https://badgen.net/github/commits/jcdcdev/jcdcdev.Eco.Signs)](https://github.com/jcdcdev/jcdcdev.Eco.Signs/commits/main)

This mod extends the awesome Over9000SignPowerMod by adding patterns that I find useful on my server.

## Install Steps
- Download the latest version from https://mod.io/g/eco/m/jcdcdevecosigns
- Extract the zip
- Copy the extracted Mods folder into your Server's Mods folder
- Make sure you also install [Over9000SignPowerMod](https://mod.io/g/eco/m/over9000signpowermod)
- Start the server

### Instructions

- Place a sign
- Enter one of the patterns below
- Look at the sign
- Run `/sign register`
- Run `/sign stop` to make changes or to move sign

## Patterns

### Online Player Count

`</onlineCount>`

![onlineCount.png](https://github.com/jcdcdev/jcdcdev.Eco.Signs/blob/main/docs/screenshots/onlineCount.png?raw=true)

### Calorie Cost

Displays the average cost of 1000 calories across all food items currently for sale. Updates every 10 seconds.

`</calorieCost>`

![calorieCount.png](https://github.com/jcdcdev/jcdcdev.Eco.Signs/blob/main/docs/screenshots/calorieCount.png?raw=true)

#### Settings

These can be added/removed to your preference

- **oos** - includes out of stock items
- **id** - id of the store - only includes products for sale at that store

### Store Stock (Icons)

Displays items as icons for a given store!

![](https://image.modcdn.io/members/2215/26012295/profile/store.gif)

`</store id oos sell buy price stock>`

#### Settings

- **id** - required - id of the store

These can be added/removed to your preference

- **oos** - shows out of stock items
- **sell** - only display items for sale
- **buy** - only display items the store will buy
- **price** - display the price
- **stock** - display the stock level

### How to get store id

1. Look/aim at the store
2. Run `/sign CopyObjectId`
3. Paste where you see id in the examples above

## Feedback

Leave a comment if you have any suggestions or feature requests.
