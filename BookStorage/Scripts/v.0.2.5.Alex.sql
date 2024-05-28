alter table [Book] add Price numeric(12,2) NULL
GO

ALTER PROCEDURE [dbo].[Book_Select] @bookId int = null, @title nvarchar(255) = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		b.BookId,
		b.Title,
		b.Description,
		b.AuthorId,
		b.CreatedAt,
		b.CoverStorageReference,
		b.Price,
		u.Username as AuthorName,
		u.AvatarStorageReference as AuthorAvatarStorageReference
	FROM Book b
		JOIN dbo.[User] u on u.UserId = b.AuthorId
	WHERE (BookId = @bookId OR @bookId is null) and
		(Title = @title OR @title is null)
	ORDER BY Title
END
GO

ALTER PROCEDURE [dbo].[Book_Upsert] 
	@bookId int = null, 
	@title nvarchar(255), 
	@description nvarchar(max), 
	@coverStorageReference nvarchar(255) = null,
	@authorId int,
	@price numeric(12,2)
AS
BEGIN
	SET NOCOUNT ON;

	IF @bookId IS NULL
		BEGIN
			INSERT INTO [dbo].[Book]
			   ([Title]
			   ,[Description]
			   ,[AuthorId]
			   ,[Price])
			 VALUES
				   (@title
				   ,@description
				   ,@authorId
				   ,@price)

			SELECT @bookId = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Book]
			SET [Title] = @title
				,[Description] = @description
				,[AuthorId] = @authorId
				,[Price] = @price
			WHERE BookId = @bookId
		END

	EXEC [Book_Select] @bookId
	RETURN @bookId
END
GO

alter table [User] add Balance numeric(12,2) not null default 0
go

ALTER PROCEDURE [dbo].[User_SelectById] @userId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.[UserId],
		u.[Username],
		u.[Email],
		u.[Password],
		u.Balance,
		u.[AvatarStorageReference],
		u.[CreatedAt]
	FROM [dbo].[User] u
	WHERE u.UserId = @userId
	ORDER BY u.Username
END
go

ALTER PROCEDURE [dbo].[User_SelectByEmail] @email nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.[UserId],
		u.[Username],
		u.[Email],
		u.Balance,
		u.[Password],
		u.[CreatedAt]
	FROM [dbo].[User] u
	WHERE u.Email = @email
	ORDER BY u.Username
END
go

create PROCEDURE [dbo].[User_SetBalance] 
	@userId int,
	@amount numeric(12,2)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[User]
	SET Balance = @amount
	WHERE UserId = @userId
END
go

CREATE TABLE [dbo].[UserBoughtBook](
	[UserBoughtBookId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] int NOT NULL,
	[BookId] int NOT NULL,
 CONSTRAINT [PK_UserBoughtBook] PRIMARY KEY CLUSTERED 
(
	[UserBoughtBookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 ) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserBoughtBook]  ADD  CONSTRAINT [FK_UserBook_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO

ALTER TABLE [dbo].[UserBoughtBook]  ADD  CONSTRAINT [FK_UserBook_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO

create PROCEDURE [dbo].[UserBoughtBook_Insert] 
	@bookId int, 
	@userId int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[UserBoughtBook]
           ([UserId]
           ,[BookId])
     VALUES
           (@userId
           ,@bookId)
END
go

ALTER PROCEDURE [dbo].[Book_Select] @bookId int = null, @title nvarchar(255) = null, @currentUserId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		b.BookId,
		b.Title,
		b.Description,
		b.AuthorId,
		b.CreatedAt,
		b.CoverStorageReference,
		b.Price,
		u.Username as AuthorName,
		u.AvatarStorageReference as AuthorAvatarStorageReference,
		cast(case when ubb.UserId is not null then 1 else 0 end as bit) as IsBought
	FROM Book b
		JOIN dbo.[User] u on u.UserId = b.AuthorId
		left join dbo.[User] cu on cu.UserId = @currentUserId 
		left join dbo.[UserBoughtBook] ubb on ubb.UserId = @currentUserId and ubb.BookId = b.BookId
	WHERE (b.BookId = @bookId OR @bookId is null) and
		(Title = @title OR @title is null)
	ORDER BY Title
END