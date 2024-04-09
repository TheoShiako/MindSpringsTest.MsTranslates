CREATE TABLE [dbo].[Translations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SearchText] NVARCHAR(MAX) NOT NULL, 
    [TranslatedText] NVARCHAR(MAX) NULL, 
    [TranslatorId] INT NOT NULL, 
    [Status] TINYINT NOT NULL , 
    [Error] NVARCHAR(1000) NULL, 
    [Timestamp] DATETIME2 NOT NULL DEFAULT current_timestamp, 
    CONSTRAINT [FK_Translations_Translators] FOREIGN KEY ([TranslatorId]) REFERENCES [Translators]([Id])
)
