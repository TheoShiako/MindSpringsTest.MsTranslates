CREATE PROCEDURE [dbo].[spTranslators_Insert]
	@Name nvarchar(50),
	@BaseUrl nvarchar(100),
	@ApiKey nvarchar(100)
AS

begin
	set nocount on;

	insert into Translators ([Name], BaseUrl, ApiKey)
	values (@Name, @BaseUrl, @ApiKey);

end
