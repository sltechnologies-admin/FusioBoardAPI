CREATE PROCEDURE [dbo].[sp_fb_UpsertUser] @Username NVARCHAR(100)
	,@Email NVARCHAR(100)
	,@PasswordHash NVARCHAR(255)
	,@CreatedAt DATETIME = NULL
	,@UpdatedAt DATETIME = NULL
	,@IsActive BIT = 1
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
			SELECT 1
			FROM Users
			WHERE Username = @Username
			)
	BEGIN
		-- Update existing user
		UPDATE Users
		SET Email = @Email
			,PasswordHash = @PasswordHash
			,UpdatedAt = @UpdatedAt
			,IsActive = @IsActive
		WHERE Username = @Username;
	END
	ELSE
	BEGIN
		-- Insert new user
		INSERT INTO Users (
			Username
			,Email
			,PasswordHash
			,CreatedAt
			,UpdatedAt
			,IsActive
			)
		VALUES (
			@Username
			,@Email
			,@PasswordHash
			,@CreatedAt
			,@UpdatedAt
			,@IsActive
			);
	END
END;