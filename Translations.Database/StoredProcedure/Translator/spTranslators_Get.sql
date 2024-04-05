CREATE PROCEDURE [dbo].[spTranslators_Get]
	@Id int = null
AS

begin
	set nocount on;

	select * from Translators
	where Id = @Id or @Id is null;

end
