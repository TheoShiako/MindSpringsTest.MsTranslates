/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--Seed database with 'l33tsp34k' Translator
IF NOT EXISTS (SELECT 1 FROM Translators)
BEGIN
    exec spTranslators_Insert @Name = 'Leetspeak', @BaseUrl = 'https://api.funtranslations.com/translate/leetspeak.json', @ApiKey = null;
END
