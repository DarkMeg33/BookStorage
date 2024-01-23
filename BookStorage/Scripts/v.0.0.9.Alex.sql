CREATE TABLE [dbo].Comment(
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[AuthorId] [int] NOT NULL CONSTRAINT FK_Comment_AuthorId FOREIGN KEY (AuthorId) REFERENCES dbo.[User](UserId),
	[BookId] [int] NOT NULL CONSTRAINT FK_Comment_BookId FOREIGN KEY (BookId) REFERENCES Book(BookId),
	[CreatedAt] [datetime] NOT NULL,
	CONSTRAINT [PK_Comment_CommentId] PRIMARY KEY CLUSTERED 
	(
		[CommentId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	INDEX [IX_Comment_BookId] 
	(
		[BookId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].Comment ADD  CONSTRAINT [DF_Comment_Created]  DEFAULT (getdate()) FOR [CreatedAt]
GO


ALTER TABLE Book
ALTER COLUMN Author int NOT NULL

ALTER TABLE Book
ALTER COLUMN Description nvarchar(max) NOT NULL

GO

ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_BookId]
GO

ALTER TABLE [dbo].[Book] DROP CONSTRAINT [DF_Book_Created]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 12/12/2023 11:15:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Book]') AND type in (N'U'))
DROP TABLE [dbo].[Book]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 12/12/2023 11:15:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Author] [int] NOT NULL constraint FK_Book_Author foreign key (Author) references [User](UserId),
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Book_BookId] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Created]  DEFAULT (getdate()) FOR [CreatedAt]
GO


ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_BookId]
GO

ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_BookId]
GO

ALTER TABLE [dbo].[Book] DROP CONSTRAINT [DF_Book_Created]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 12/12/2023 11:15:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Book]') AND type in (N'U'))
DROP TABLE [dbo].[Book]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 12/12/2023 11:15:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[AuthorId] [int] NOT NULL constraint FK_Book_Author foreign key (AuthorId) references [User](UserId),
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Book_BookId] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Created]  DEFAULT (getdate()) FOR [CreatedAt]
GO


ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Book]
GO

CREATE PROCEDURE [dbo].[Comment_Select] @commentId int = null, @bookId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		c.CommentId,
		c.Text,
		c.BookId,
		c.AuthorId,
		c.CreatedAt,
		u.Username as AuthorName
	FROM Comment c
		JOIN dbo.[User] u on u.UserId = c.AuthorId
	WHERE (BookId = @bookId OR @bookId is null) and
		(CommentId = @commentId or @commentId is null)
	ORDER BY CreatedAt
END
GO

ALTER PROCEDURE [dbo].[Book_Select] @bookId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		b.BookId,
		b.Title,
		b.Description,
		b.AuthorId,
		b.CreatedAt,
		u.Username as AuthorName
	FROM Book b
		JOIN dbo.[User] u on u.UserId = b.AuthorId
	WHERE BookId = @bookId OR @bookId is null
	ORDER BY Title
END
GO

ALTER PROCEDURE [dbo].[Book_Upsert] 
	@bookId int = null, 
	@title nvarchar(255), 
	@description nvarchar(255), 
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