CREATE PROCEDURE [dbo].[spTranslators_Update]
	@Name nvarchar(50),
	@BaseUrl nvarchar(100),
	@ApiKey nvarchar(100),
	@Id int
AS

begin
	set nocount on;

	update Translators
	set [Name] = @Name,
		BaseUrl = @BaseUrl,
		ApiKey = @ApiKey
	where Id = @Id;

end
