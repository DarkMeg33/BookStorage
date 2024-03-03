ALTER PROCEDURE [dbo].[Book_Upsert] 
	@bookId int = null, 
	@title nvarchar(255), 
	@description nvarchar(max), 
	@coverStorageReference nvarchar(255) = null,
	@authorId int
AS
BEGIN
	SET NOCOUNT ON;

	IF @bookId IS NULL
		BEGIN
			INSERT INTO [dbo].[Book]
			   ([Title]
			   ,[Description]
			   ,[AuthorId])
			 VALUES
				   (@title
				   ,@description
				   ,@authorId)

			SELECT @bookId = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Book]
			SET [Title] = @title
				,[Description] = @description
				,[AuthorId] = @authorId
			WHERE BookId = @bookId
		END

	EXEC [Book_Select] @bookId
	RETURN @bookId
END
GO

ALTER PROCEDURE [dbo].[Comment_Upsert]
	@commentId int = null,
	@bookId int,
	@authorId int,
	@text nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Comment]
           ([Text]
           ,[AuthorId]
           ,[BookId]
           ,[CreatedAt])
     VALUES
           (@text
           ,@authorId
           ,@bookId
           ,GETUTCDATE())

	SELECT @commentId = SCOPE_IDENTITY()

	EXEC [Comment_Select] @commentId
END
GO