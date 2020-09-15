![alt text](https://avatars1.githubusercontent.com/u/71271063?s=200&v=4 "Team Logo")

# [Eco](https://www.play.eco/) (v9) - Survey Mod (v1.0.0)

/!\ This project is still in development.

The **v1.0.0** is released. I've tested it on my own Eco 9.1 server (name: [FR/EN] Eco World - An Epic Journey)

This mod works within the chat of the game at the current state.

It is open-source : do whatever you want with it. If you start to develop mods for Eco, it might give
you some help on how you can work with the game's chat.

Developed using clean architecture, you will find 3 main parts :

- **Domain** : contains the domain definition of the application, this ensures the main features are designed in a way that the implementation (and by extension the framework you want to use) does not matter.
- **Implementation** : contains the implementation of the domain and some outbound targets (like Eco),
- **Presentation** : contains the presentation of the data.

There are currently 2 implementations, a first one that runs as a simple Console Application and a second one 
which is the EcoGame Chat implementation. Those depends of an onboarded app implementation that contains already some adapters, helpers and so on.

Feel free to re-implement it as you want by following the **Domain**. I might extract some implementations into specific **Use Cases** in a future version to
make it cleaner and less dependent of the implementation.

Move the folder **SurveyMod** from **Build** inside the Mods folder of your server if you want to test it in-game.

## v1.0.0

Following the list of released features :
- **/survey start** : *start a survey*
- **/survey stop** : *stop a survey*
- **/survey list** : *list all/active/ended surveys*
- **/survey vote** : *vote for a survey*

Everything is manageable using the game chat commands. All the commands are documented by appending "**help**".

## Upcoming features

This is my first mod so I'm trying to understand all the aspects of the game at the same time I'm learning C#. Modding a game is quite exciting, I'm gonna continue on extending this mod as soon as any idea will come up.

The next main goal is providing an in-game GUI to handle the surveys easier.

However, if you have any request, any bug report, remark or suggestion, you can create an issue here on github or join me on Discord: https://discord.gg/P9vGWPg