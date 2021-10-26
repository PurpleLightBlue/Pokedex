# TrueLayer Pokedex

The Truelayer Pokedex is a .net 5 based web API. It has an endpoint for use in getting basic Pokémon information from publicly available sources. It also has an endpoint to provide a fun translation for the Pokémon’s description. A 'legendary' or 'cave dwelling' Pokémon with receive a Yoda style translation, all others will receive a Shakespearean one. 

## Installation

Clone this repository to your local machine. Then you can either open and build the project using visual studio, or navigate to the <install location>\TrueLayerPokedex\TrueLayer.Pokedex.API\TrueLayer.Pokedex.API and run the following dotnet build command

```powershell
dotnet build --configuration Release
```
This should produce a number of outputs, including the following <install location>\Pokedex\TrueLayer.Pokedex.API\TrueLayer.Pokedex.API\bin\Release\net5.0\TrueLayer.Pokedex.API.dll

## Usage

To then start the API navigate to <install location>\Pokedex\TrueLayer.Pokedex.API\TrueLayer.Pokedex.API\bin\Release\net5.0\ and run the following dotnet command 

```powershell
dotnet TrueLayer.Pokedex.API.dll
```
The service should start up with output to the console, including the following lines:

```powershell
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
[2021-10-24 13:50:57.0416] [INFO] [Lifetime] Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
[2021-10-24 13:50:57.0416] [INFO] [Lifetime] Now listening on: https://localhost:5001
```

This means you should be able to start making API requests on the localhost, to get a basic Pokémon object run the following (as an example) in any browser

https://localhost:5001/Pokemon/onix

To get a fun translation use the following Url structure

https://localhost:5001/Pokemon/Translated/onix

One last item, when run locally the application will write a log to your local application data folder. This is something of a stop-gap measure as I wanted something that would seamlessly work without asking you to create a folder on C:/ or something like that. It will appear as:

* C:\Users\<Username>\AppData\Local\PokedexAPI\PokedexAPI\Pokedex-API.txt

See nlog config in the application for the setup of rolling, archiving etc. 

## Notes on design and implementation

In tackling the challenge, I wanted to see what the application would look like as approached from Domain Driven Design (DDD) point of view, or at least with a rich-ish domain model. However I was conscious that this might be overkill for a simple scenario. 

Certainly there were other approaches that could be taken, an anaemic domain model approach with a business logic layer represented in classes could have worked. As it happens I felt at times I was trying to make the domain approach work given there wasn’t a huge amount of complexity to ‘show it off’. Here are some thoughts that went into the approach taken:

* The API wrapper classes make use of Data Transfer Object (DTO) classes into which to map results from their respective sources. I felt these should be distinct to that API layer and only used there, the thinking being the spec of the API data that is returned could change. Any such changes would be limited to just that layer, indeed a new API source entirely of Pokémon data could be switched in. Taking this approach, any change should hopefully be limited to just this layer. 
* The API Wrappers return fully fledged domain objects, this means there is a common contract for other components to use. 
* The Pokémon domain class’s structure was guided by the required example response in the challenge paperwork, so is much simpler than the rich data returned from the public APIs. My reasoning being that sample response formed the requirements to satisfy therefore there was no need to include excess. If this were a product with a lifespan then the domain object could be changed and added to.
* The domain models have some basic guard clauses in the model to change the contents but otherwise there wasn’t much else to put in here such as business logic as would be seen in a truly ‘rich’ domain model. 
* I did make the translation aspect part of a domain service. I did 'umm and arr' about this part. It could have been a method on the domain model but then the domain model would need to know about the translation API wrapper as a dependency and I felt this wasn’t clean enough.
* The Controller classes effectively form the 'application layer' as sometimes termed in DDD. Although they receive domain models from the API wrappers I didn’t want them to expose those models to any consuming service, so again made view models that were distinct for this layer. Hopefully this means any changes needed by a consuming application would be limited to this layer. Also any modifications made to the domain objects would not impact the consuming applications. However I was conscious that, together with the DTOs used by the API wrapper classes, there are an awful lot of models now floating around. 
* I decided to use Lamar as an IOC container having worked with it recently. 
* Logging is taken care of via NLog, at the moment it just writes to a file but in production it is anticipated that some web based logging service could be used such as logzs.io
* I have included a basic docker file, in terms of putting this into production then I would anticipate using something like Kubernetes as this would provide scaling and healing as required. I did ponder the scenario of a service mesh if this were a service that was talked to by other micro services/had a requirement to talk to other services. If that’s not the case then this might be considered overkill as it can add a lot of overhead code for the benefits its brings. 

