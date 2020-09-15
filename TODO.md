## Development TODO

*(because a code should never contain todo annotation!)*

- Parse the user input options in order to expose it into the Command, that way the Handlers does not have to parse their options (accessible once an handler is configured) and not have to check the availability themselves. This should not be their responsibility.