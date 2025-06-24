CREATE PROCEDURE [dbo].[sp_fb_User_Upsert]
    @UserId INT = 0, -- Optional: used for update
    @Username NVARCHAR(100),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255),
    @CreatedAt DATETIMEOFFSET = NULL,
    @UpdatedAt DATETIMEOFFSET = NULL,
    @IsActive BIT = 1,
    @IsEmailVerified BIT = 0,
    @ProfilePicture NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- If updating existing user by UserId
    IF EXISTS (SELECT 1 FROM Users WHERE UserId = @UserId AND @UserId > 0)
    BEGIN
        UPDATE Users
        SET 
            Username = @Username,
            Email = @Email,
            PasswordHash = @PasswordHash,
            UpdatedAt = ISNULL(@UpdatedAt, SYSDATETIMEOFFSET()),
            IsActive = @IsActive,
            IsEmailVerified = @IsEmailVerified,
            ProfilePicture = @ProfilePicture
        WHERE UserId = @UserId;
    END
    ELSE
    BEGIN
        -- Check for duplicate Username only
        IF EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
        BEGIN
                --11–16: User errors 
                -- 1: State (custom code) You could use 1 for duplicate username, 2 for duplicate email (if enforced), etc.
            RAISERROR ('Username already exists.', 16, 1); 
            RETURN;
        END

        -- Insert new user
        INSERT INTO Users (
            Username,
            Email,
            PasswordHash,
            CreatedAt,
            UpdatedAt,
            IsActive,
            IsEmailVerified,
            ProfilePicture
        )
        VALUES (
            @Username,
            @Email,
            @PasswordHash,
            ISNULL(@CreatedAt, SYSDATETIMEOFFSET()),
            ISNULL(@UpdatedAt, SYSDATETIMEOFFSET()),
            @IsActive,
            @IsEmailVerified,
            @ProfilePicture
        );
    END
END;