--- Dashboard/Summary 
select Module,count(*) as APICount from APIMatrix group by module


-- Tables: 
select   * from Users
select   * from Projects
select   * from Sprints order by name
select   * from logs  order by CreatedAt desc
 
select top 3 * from Projectssp_fb_GetAllUsers
select top 3 * from UserProjectRoles

-- Only run this once during schema migration
EXEC sp_rename 'Logs.Message', 'UserMessage', 'COLUMN';
EXEC sp_rename 'Logs.Exception', 'TechnicalDetails', 'COLUMN';


-- where 
select * from Logs where CorrelationId='c4d9b0fa-2a5a-46fd-9def-e8428ff19ebd'
-- Login Users set Username = 'adminuser', PasswordHash = 'sltech@123' where UserId = 1


-- Stored Procedure:
  -- exec [dbo].[sp_fb_GetUserById] 1
  -- exec dbo.sp_fb_GetAllUsers
  exec [dbo].[sp_fb_GetProjectById] 1ProjectId
 
1
 
