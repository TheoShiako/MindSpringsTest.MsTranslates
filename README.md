# MindSprings .Net Developer Test

An ASP.NET MVC application that takes a text string from a user and turns it into "l33tsp34k".

Authentication and authorization is implemented for administrator(s) of the application.

Administrators can add other translators when needed, view search history and add new administrator(s).

Where interactivity is required, Blazor Server components were used.

#
* Set connection string in appsettings.json.
* Database can be setup through Visual Studio with the 'MsTranslates.Database' project or by executing the attached 'MsTranslatesDb.Script'.
* Entity framework handles the authentication data access.
