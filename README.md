# Why
Project created in a two day sprint as a demonstration of my coding ability and style

# What (High level)

As a user, I want to create a formulation of feed ingredients measured in tons with pricing information, so that I can get an estimate of cost.

WPF user interface with an ASP.Net Core RESTful API for pricing information

# What (Requirements)

The user should be allowed to create formulas with the following ingredients: corn, soybeans, wheat, hay, straw. 

Each ingredient should be measured in tons. 

There should be a separate pricing REST API to get the prices for the ingredients. 

The service does not need to include any authentication or authorization. 

The service does not need to persist changes between runs

The price for the ingredients can be static in this proof-of-concept, but the idea is the production system will have a separate pricing system the formulation system will use. 

The number of ingredients will increase in the future.

We may need to include prices from more than one service in the future as well. 

Design the system so it is easy to add additional ingredients and calculate the price in more than one way.

Your program should execute and ask the user for the amount of each ingredient, calculate the price, and display the price to the user. 

# How

This project was created using Visual Studio 2017.  The WPF project targets .Net Framework 4.6.1 and the Web API targets .Net Core 2.1.  Those frameworks will need to be installed on the machine that will be compiling the application.

After downloading the Repository a few things will need to done to ensure the application will run correctly.
1.	Open the Properties page for the IngredientPricingAPI project.  Open the Debug tab and copy the port number from the App URL.
2.	Paste that port number into the App.Xaml.cs file in the WPF project in place of the current port.  This is the current line that has the port number: client.BaseAddress = new Uri("http://localhost:64370/");
3.	Set the IngredientPricingAPI project as startup and run the application.
4.	Stop the Debugging session.
5.	Set the WPF project as startup.  
6.	The application should now run.


# ETC

In addition to the nuget packages added by Visual Studio during project creation, this project implements the following nuget packages:
AutoFac
Microsoft.AspNet.WebApi.Client
Newtonsoft.JSON
Prism.Core
Moq
