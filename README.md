# formulation-editor

Prototype of a formulation editor

WPF user interface with an ASP.Net Core Restful API for pricing information.

Project created in a two day sprint as a demonstration of my coding ability.

High level requirements:
•	The user should be allowed to create formulas with the following ingredients: corn, soybeans, wheat, hay, straw. 
•	Each ingredient should be measured in tons. 
•	There should be a separate pricing REST API to get the prices for the ingredients. 
•	The service does not need to include any authentication or authorization. 
•	The service does not need to persist changes between runs
•	The price for the ingredients can be static in this proof-of-concept, but the idea is the production system will have a separate pricing system the formulation system will use. 
•	The number of ingredients will increase in the future.
•	We may need to include prices from more than one service in the future as well. 
•	Design the system so it is easy to add additional ingredients and calculate the price in more than one way.
•	Your program should execute and ask the user for the amount of each ingredient, calculate the price, and display the price to the user. 


In addition to the nuget packages added by Visual Studio during project creation, this project implements the following nuget packages:
AutoFac
Microsoft.AspNet.WebApi.Client
Newtonsoft.JSON
Prism.Core
Moq
