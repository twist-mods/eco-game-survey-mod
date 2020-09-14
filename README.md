# [Eco](https://www.play.eco/) (v9) - Survey Mod (v1.0.0)

This mod works within the chat of the game at the current state.

It is open-source : do whatever you want with it. If you start to develop mods for Eco, it might give
you some help on how you can work with the game's chat.

Developed using some clean architecture, you will find 3 main parts :

- **Domain** : contains the domain definition of the application, this ensures the main features are designed in a way that the implementation (and by extension the framework you want to use) does not matter.
- **Implementation** : contains the implementation of the domain and some outbound targets (like Eco),
- **Presentation** : contains the presentation of the data.

There are currently 2 implementations, a first one that runs as a simple Console Application and a second one 
which is the EcoGame Chat implementation. Those depends of an onboarded app implementation that contains already some adapters, helpers and so on.

Feel free to re-implement it as you want by following the **Domain**. I might extract some implementations into specific **Use Cases** in a future version to
make it cleaner and less dependent of the implementation.

Move the folder **SurveyMod** inside **Build** in the Mods folder of your server if you want to test it in-game.

[]: https://www.play.eco/Eco