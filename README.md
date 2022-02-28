# SignalR-Example
*Example : Build a Serverless Real-time App with Azure Functions, Azure SignalR Service and C# Console Client*

SignalR is a service which enables to send message to a client. With a persistent connection between the client and server, unlike a classic HTTP connection, the clients receive the message in real time and don’t need to poll the server for changes periodically. Full description here : https://www.nellysattari.com/real-time-applicationsazure-signalr-service/

Serverless functions are run in micro-containers that are started up on demand. They’re designed for fairly short-running processes, and so billing is set with this in mind. More information here : https://www.sitepoint.com/serverless-functions.

To create serverless functions, Azure Functions will be used. More information here : https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview

-----------------------------------------------------------------------------------------------------------------------------

Sign in to the Azure portal at https://portal.azure.com/ with your Azure account. Then select the New button found on the upper left-hand corner of the Azure portal. 
In the New screen, type SignalR Service in the search box and press enter.Select SignalR Service from the search results, then select Create.
Azure SignalR Service can be configured in different modes. In order to be able to use serverless, it needs to be configured in Serverless mode.
Then go to Settings/Connection Strings/and copy the connection strings in access key section.

The entire implementation of the tutorial is detailed here : https://charlescol.wordpress.com/2021/08/18/example-post-3/
