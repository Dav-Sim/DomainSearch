# DomainSearch
## C# WPF Whois domain search app

The application is used to search for whois information about Internet domains. Especially for an approximate determination of whether the domain is free to register.

![Image of app main window](https://github.com/Dave4626/DomainSearch/blob/main/app.jpg?raw=true "DomainSearch app main window")

### how to use
- In the top window, you can specify several "second-level domain (SLD) names" separated by a space for which you want to search for "whois" information.

- Under this window, select checkboxes with TLD (top level domain).

- And click start. Information from the whois server for all specified TLD and SLD combinations will begin to appear in the list.

This application uses the "WHOIS Lookup and Parsing Library" [github link](https://github.com/flipbit/whois) Thanks to its author/s.
