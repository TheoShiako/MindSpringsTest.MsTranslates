CREATE PROCEDURE [dbo].[spTranslations_Insert]
	@SearchText nvarchar(max),
	@TranslatedText nvarchar(max),
	@TranslatorId int,
	@Status tinyint,
	@Error nvarchar(1000)
AS

begin
	set nocount on;

	insert into Translations (SearchText,TranslatedText,TranslatorId,[Status],Error)
	values (@SearchText,@TranslatedText,@TranslatorId,@Status,@Error);
end
