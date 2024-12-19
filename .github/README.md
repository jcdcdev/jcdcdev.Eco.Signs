## jcdcdev.Eco.Signs

[![Latest version on Github](https://badgen.net/github/tag/jcdcdev/jcdcdev.Eco.Signs?color=3a93b4&label=Mod)](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/latest)

This mod extends the awesome Over9000SignPowerMod by adding patterns that I find useful on my server.

## Install Steps

- Download the latest version of this mod from [mod.io](https://mod.io/g/eco/m/jcdcdevecosigns)
- Download the latest version of `jcdcdev.Eco.Core` from [mod.io](https://mod.io/g/eco/m/jcdcdevecocore)
- Download the latest version of `Over9000SignPowerMod` from [mod.io](https://mod.io/g/eco/m/over9000signpowermod)
- Extract each zip
- Go to your Server's root folder
- Copy the extracted content to `./Mods/UserCode`
- Start the server

### Instructions

- Place a sign
- Enter one of the patterns below
- Look at the sign
- Run `/sign register`
- Run `/sign stop` to make changes or to move sign

## Available Patterns

### Online Player Count

`</onlineCount>`

![onlineCount.png](https://github.com/jcdcdev/jcdcdev.Eco.Signs/blob/main/docs/screenshots/onlineCount.png?raw=true)

### Bank Account Balance

Displays the bank account balance for the specified player.

`</bankAccount playerName currency i n>`

![bankAccount.png](https://github.com/jcdcdev/jcdcdev.Eco.Signs/blob/main/docs/screenshots/bankAccount.png?raw=true)

#### Settings

- **playerName** - required - name of the player
- **currency** - currency to display - required if player holds multiple currencies
- **i** - include an icon
- **n** - include the currency name

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

## Version Table
| Version | Core Version | Game Version |
|-----|---------| -----------|
| [11.1.6](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.1.6) | [11.1.6](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.1.6) | 11.1.6 |
| [11.1.5](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.1.5) | [11.1.5](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.1.5) | 11.1.5 |
| [11.1.4](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.1.4) | [11.1.4](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.1.4) | 11.1.4 |
| [11.1.3](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.1.3) | [11.1.3](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.1.3) | 11.1.3 |
| [11.1.2](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.1.2) | [11.1.2](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.1.2) | 11.1.2 |
| [11.1.1](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.1.1) | [11.1.1](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.1.1) | 11.1.1 |
| [11.1.0](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.1.0) | [11.1.0](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.1.0) | 11.1 |
| [11.0.8](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.8) | [11.0.7](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.7) | 11.0.6 |
| [11.0.7](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.7) | [11.0.6](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.6) | 11.0.5 |
| [11.0.6](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.6) | [11.0.5](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.5) | 11.0.4 |
| [11.0.5](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.5) | [11.0.4](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.4) | 11.0.3 |
| [11.0.4](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.4) | [11.0.3](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.3) | 11.0.3 |
| [11.0.3](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.3) | [11.0.2](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.2) | 11.0.2 |
| [11.0.2](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.2) | [11.0.1](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.1) | 11.0.1 |
| [11.0.1](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.1) | [11.0.1](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.1) | 11.0.1 |
| [11.0.0](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/11.0.0) | [11.0.0](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/11.0.0) | 11.0 |
| [0.5.2](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.5.2) | [0.5.2](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.5.2) | 10.2.3 |
| [0.5.1](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.5.1) | [0.5.1](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.5.1) | 10.2.2 |
| [0.5.0](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.5.0) | [0.5.0](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.5.0) | 10.2.1 |
| [0.4.1](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.4.1) | [0.4.1](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.4.1) | 10.1 |
| [0.4.0](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.4.0) | [0.4.0](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.4.0) | 10.0 |
| [0.3.0](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.3.0) | [0.3.0](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.3.0) | 9.7.10 |
| [0.2.2](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.2.2) | [0.2.0](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.2.0) | 9.7.10 |
| [0.2.1](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.2.1) | [0.1.5](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.1.5) | 9.7.10 |
| [0.2.0](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.2.0) | [0.1.4](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/0.1.4) | 9.7.10 |
| [0.1.1](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.1.1) | N/A | 9.7.10 |
| [0.1.0](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/0.1.0) | N/A | 9.7.10 |
| [](https://github.com/jcdcdev/jcdcdev.Eco.Signs/releases/tag/) | [](https://github.com/jcdcdev/jcdcdev.Eco.Core/releases/tag/) |  |
