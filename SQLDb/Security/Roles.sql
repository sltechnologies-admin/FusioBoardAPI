-- Create a role for project managers
CREATE ROLE db_project_manager;

-- Grant permissions to the role
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO db_project_manager;
GRANT EXECUTE ON SCHEMA::dbo TO db_project_manager;

-- Add a user to the role (if user exists)
-- CREATE USER [pm_user] FOR LOGIN [pm_user];
-- ALTER ROLE db_project_manager ADD MEMBER [pm_user];
