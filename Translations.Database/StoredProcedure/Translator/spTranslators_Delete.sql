CREATE PROCEDURE [dbo].[spTranslators_Delete]
	@Id int
AS

begin
	set nocount on;

	delete from Translators where Id = @Id;

end
