CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [IX_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[Account_Insert] 
	@userId int = null, 
	@username nvarchar(255), 
	@email nvarchar(255), 
	@password nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[User]
           ([Username]
           ,[Email]
           ,[Password]
           ,[CreatedAt])
     VALUES
           (@username
           ,@email
           ,@password
           ,GETUTCDATE())
END
GO

CREATE PROCEDURE [dbo].[User_SelectByEmail] @email nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.[UserId],
		u.[Username],
		u.[Email],
		u.[Password],
		u.[CreatedAt]
	FROM [dbo].[User] u
	WHERE u.Email = @email
	ORDER BY u.Username
END
GO

CREATE PROCEDURE [dbo].[User_SelectById] @userId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.[UserId],
		u.[Username],
		u.[Email],
		u.[Password],
		u.[CreatedAt]
	FROM [dbo].[User] u
	WHERE u.UserId = @userId
	ORDER BY u.Username
END
GO

Create PROCEDURE [dbo].[UserPassword_Select] @email nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		u.Password
	FROM [User] u
	WHERE u.Email = @email

END
GO