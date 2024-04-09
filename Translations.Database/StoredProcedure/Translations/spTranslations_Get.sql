CREATE PROCEDURE [dbo].[spTranslations_Get]
	
AS

begin
	set nocount on;

	select *
	from Translations tn
	inner join Translators tr on tn.TranslatorId=tr.Id;
end