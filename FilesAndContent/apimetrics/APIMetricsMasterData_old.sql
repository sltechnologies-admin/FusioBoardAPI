INSERT INTO APIMatrix (
    Module, Classification, Feature, Purpose, StoredProcedure, APIEndpoint,
    AdditionalDetails, UIPageUsage, NavigationPath,
    ImplementationStatus, UI_API_IntegrationTestingStatus, BugsCount,
    AssignedTo, APITestedBy, UITestedBy, BlockingBugDescription, Priority, Phase
) VALUES
('User & Role Management', '✅ Core System Requirement', 'User Registration', 'Allow user to register', 'sp_fb_UpsertUser', 'POST /api/Auth/register', 'Includes password hashing and validation', 'Register Page', '/register', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),
('User & Role Management', '✅ Core System Requirement', 'User Login', 'Allow user to login', 'sp_fb_Validate_UserLogin', 'POST /api/Auth/login', 'Returns token or session', 'Login Page', '/login', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),
('User & Role Management', '✅ Core System Requirement', 'Get User by Id', 'View profile info', 'sp_fb_GetUserById', 'GET /api/Auth/user/{id}', 'Used in profile screen', 'User Profile', '/profile/:id', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),
('User & Role Management', '✅ Core System Requirement', 'Get All Users', 'Admin user management', 'sp_fb_GetAllUsers', 'GET /api/Auth/users', 'Optional filters (Phase 2)', 'User Management', '/admin/users', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),
('User & Role Management', '✅ Core System Requirement', 'Update User', 'Edit user profile', 'sp_fb_UpsertUser', 'PUT /api/Auth/user', 'Upsert used for create/update', 'User Profile Edit', '/profile/edit', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),
('User & Role Management', '✅ Core System Requirement', 'Delete User', 'Remove a user', 'sp_fb_DeleteUserById', 'DELETE /api/Auth/user/{id}', 'Soft delete optional', 'User Management', '/admin/users', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),
('User & Role Management', '✅ Core System Requirement', 'Email/OTP Verification (Optional)', 'Enhance security', 'TBD', 'TBD', 'May skip in Phase 1', 'N/A', 'N/A', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Low', 'Phase 1');

----------------------------------------
INSERT INTO APIMatrix (
    Module, Classification, Feature, Purpose, StoredProcedure, APIEndpoint,
    AdditionalDetails, UIPageUsage, NavigationPath,
    ImplementationStatus, UI_API_IntegrationTestingStatus, BugsCount,
    AssignedTo, APITestedBy, UITestedBy, BlockingBugDescription, Priority, Phase
) VALUES
('Role & Permission Management', '🔐 Security and Access Control', 'Get User Roles', 'Role visibility', 'sp_fb_GetUserRoles', 'GET /api/Role/user/{id}', 'Returns all roles for user', 'User Role View', '/admin/users/:id/roles', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),
('Role & Permission Management', '🔐 Security and Access Control', 'Assign Role to User', 'RBAC control', 'sp_fb_AssignUserRole', 'POST /api/Role/user', 'Support multiple roles', 'User Role Assignment', '/admin/users/:id/assign-role', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),
('Role & Permission Management', '🔐 Security and Access Control', 'Get User Permissions', 'RBAC details', 'sp_fb_GetUserPermissions', 'GET /api/Permission/user/{id}', 'For frontend routing guards', 'Permission View', '/admin/users/:id/permissions', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1');

---------------------------------------------------

INSERT INTO APIMatrix (
    Module, Classification, Feature, Purpose, StoredProcedure, APIEndpoint,
    AdditionalDetails, UIPageUsage, NavigationPath,
    ImplementationStatus, UI_API_IntegrationTestingStatus, BugsCount,
    AssignedTo, APITestedBy, UITestedBy, BlockingBugDescription, Priority, Phase
) VALUES
('Project Management', '🧱 Entry to workflows', 'Create Project', 'Add new project', 'sp_fb_UpsertProject', 'POST /api/Project', 'ProjectName, CreatedBy, Status', 'Project Create', '/projects/new', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Get Project By ID', 'View project', 'sp_fb_GetProjectById', 'GET /api/Project/{id}', 'Includes members optionally', 'Project View', '/projects/:id', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Update Project', 'Edit project', 'sp_fb_UpsertProject', 'PUT /api/Project', 'Same SP as Create', 'Project Edit', '/projects/:id/edit', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Get All Projects', 'List for admin/users', 'sp_fb_GetAllProjects', 'GET /api/Project', 'May support pagination', 'Project List', '/projects', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Archive Project', 'Mark as inactive', 'sp_fb_ArchiveProject', 'PUT /api/Project/archive/{id}', 'IsArchived = 1', 'Project List', '/projects', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Low', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Assign Roles to Project', 'Role binding', 'sp_fb_AssignProjectUserRole', 'POST /api/Project/{id}/roles', 'UserId, RoleId, ProjectId', 'Project Roles', '/projects/:id/roles', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Get Project Members', 'Show team', 'sp_fb_GetProjectMembers', 'GET /api/Project/{id}/members', 'Optional: include roles', 'Project Members', '/projects/:id/members', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Add Project Members', 'Add user(s) to project', 'sp_fb_AddProjectMember', 'POST /api/Project/{id}/members', 'Bulk support optional', 'Project Members', '/projects/:id/members/add', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Remove Member', 'Remove user', 'sp_fb_RemoveProjectMember', 'DELETE /api/Project/{id}/members/{userId}', 'Confirm before delete', 'Project Members', '/projects/:id/members', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Project Management', '🧱 Entry to workflows', 'Get Projects by User', 'Show user’s projects', 'sp_fb_GetProjectsByUserId', 'GET /api/Project/user/{userId}', 'Useful in dashboard view', 'User Dashboard', '/dashboard', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1');

---------------------------------

INSERT INTO APIMatrix (
    Module, Classification, Feature, Purpose, StoredProcedure, APIEndpoint,
    AdditionalDetails, UIPageUsage, NavigationPath,
    ImplementationStatus, UI_API_IntegrationTestingStatus, BugsCount,
    AssignedTo, APITestedBy, UITestedBy, BlockingBugDescription, Priority, Phase
) VALUES
('Sprint Management', '🏁 Sprint planning', 'Create Sprint', 'Start a sprint', 'sp_fb_UpsertSprint', 'POST /api/Sprint', 'Includes Start/End Dates', 'Sprint Create', '/sprints/new', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Sprint Management', '🏁 Sprint planning', 'Get Sprint by ID', 'View sprint', 'sp_fb_GetSprintById', 'GET /api/Sprint/{id}', 'Includes linked tasks (optional)', 'Sprint View', '/sprints/:id', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Sprint Management', '🏁 Sprint planning', 'Get All Sprints by Project', 'List sprints', 'sp_fb_GetSprintsByProjectId', 'GET /api/Sprint/project/{projectId}', 'Filter by status', 'Project Sprint Board', '/projects/:id/sprints', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Sprint Management', '🏁 Sprint planning', 'Update Sprint', 'Edit name, dates', 'sp_fb_UpsertSprint', 'PUT /api/Sprint', 'Common SP', 'Sprint Edit', '/sprints/:id/edit', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Sprint Management', '🏁 Sprint planning', 'Change Sprint Status', 'Start/Complete/Cancel', 'sp_fb_UpdateSprintStatus', 'PUT /api/Sprint/{id}/status', 'Use enum status', 'Sprint Status', '/sprints/:id/status', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Sprint Management', '🏁 Sprint planning', 'Link Tasks to Sprint', 'Task sprint binding', 'sp_fb_LinkTaskToSprint', 'POST /api/Sprint/{id}/tasks', 'List of TaskIds', 'Sprint Task Link', '/sprints/:id/tasks/link', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1');
------------------------------------------------
INSERT INTO APIMatrix (
    Module, Classification, Feature, Purpose, StoredProcedure, APIEndpoint,
    AdditionalDetails, UIPageUsage, NavigationPath,
    ImplementationStatus, UI_API_IntegrationTestingStatus, BugsCount,
    AssignedTo, APITestedBy, UITestedBy, BlockingBugDescription, Priority, Phase
) VALUES
('Requirement (PBI) Mgmt', '📌 Backlog planning', 'Create Requirement', 'Add Epic/Story', 'sp_fb_UpsertRequirement', 'POST /api/Requirement', 'Type: Epic, Story, etc.', 'Requirement Create', '/requirements/new', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Requirement (PBI) Mgmt', '📌 Backlog planning', 'Get Requirement By ID', 'View details', 'sp_fb_GetRequirementById', 'GET /api/Requirement/{id}', 'Include linked tasks', 'Requirement View', '/requirements/:id', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Requirement (PBI) Mgmt', '📌 Backlog planning', 'Update Requirement', 'Edit title, status', 'sp_fb_UpsertRequirement', 'PUT /api/Requirement', 'Status: Draft, Approved', 'Requirement Edit', '/requirements/:id/edit', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Requirement (PBI) Mgmt', '📌 Backlog planning', 'Link Tasks to Requirement', 'Task binding', 'sp_fb_LinkTasksToRequirement', 'POST /api/Requirement/{id}/tasks', 'Many-to-one', 'Requirement Task Link', '/requirements/:id/tasks/link', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Requirement (PBI) Mgmt', '📌 Backlog planning', 'Get All Requirements by Project', 'View backlog', 'sp_fb_GetRequirementsByProjectId', 'GET /api/Requirement/project/{projectId}', 'Optional filters', 'Project Backlog', '/projects/:id/requirements', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1');
----------------------------------- 
INSERT INTO APIMatrix (
    Module, Classification, Feature, Purpose, StoredProcedure, APIEndpoint,
    AdditionalDetails, UIPageUsage, NavigationPath,
    ImplementationStatus, UI_API_IntegrationTestingStatus, BugsCount,
    AssignedTo, APITestedBy, UITestedBy, BlockingBugDescription, Priority, Phase
) VALUES
('Task Management', '🔄 Core Agile Unit', 'Create Task', 'Add work unit', 'sp_fb_UpsertTask', 'POST /api/Task', 'Type, Estimation, Priority', 'Task Create', '/tasks/new', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Task Management', '🔄 Core Agile Unit', 'Get Task by ID', 'Task details', 'sp_fb_GetTaskById', 'GET /api/Task/{id}', 'Include subtasks', 'Task View', '/tasks/:id', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Task Management', '🔄 Core Agile Unit', 'Get Tasks by Sprint', 'Sprint planning', 'sp_fb_GetTasksBySprintId', 'GET /api/Task/sprint/{sprintId}', '', 'Sprint Board', '/sprints/:id/tasks', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Task Management', '🔄 Core Agile Unit', 'Update Task', 'Edit task', 'sp_fb_UpsertTask', 'PUT /api/Task', 'Status flow applied', 'Task Edit', '/tasks/:id/edit', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Task Management', '🔄 Core Agile Unit', 'Assign Task', 'Assign user', 'sp_fb_AssignTask', 'POST /api/Task/{id}/assign', 'AssigneeId, Estimation', 'Task Assignment', '/tasks/:id/assign', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Task Management', '🔄 Core Agile Unit', 'Add Subtasks / Checklist', 'Breakdown', 'sp_fb_AddSubtasks', 'POST /api/Task/{id}/subtasks', 'Optional in Phase 1', 'Subtasks Checklist', '/tasks/:id/subtasks', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Task Management', '🔄 Core Agile Unit', 'Task Comments', 'Team input', 'sp_fb_AddTaskComment', 'POST /api/Task/{id}/comments', 'UserId, message', 'Task Comments', '/tasks/:id/comments', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Task Management', '🔄 Core Agile Unit', 'Task History', 'Audit trail', 'sp_fb_GetTaskHistory', 'GET /api/Task/{id}/history', 'Based on change log', 'Task History', '/tasks/:id/history', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1');
-----------------------
INSERT INTO APIMatrix (
    Module, Classification, Feature, Purpose, StoredProcedure, APIEndpoint,
    AdditionalDetails, UIPageUsage, NavigationPath,
    ImplementationStatus, UI_API_IntegrationTestingStatus, BugsCount,
    AssignedTo, APITestedBy, UITestedBy, BlockingBugDescription, Priority, Phase
) VALUES
('Dashboard & Analytics', '📊 Visual overview', 'Role-Based Dashboard', 'Home for each user type', 'sp_fb_GetDashboardDataByRole', 'GET /api/Dashboard/role/{userId}', 'Return sections based on role', 'Dashboard', '/dashboard', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'High', 'Phase 1'),

('Dashboard & Analytics', '📊 Visual overview', 'Project Summary Widget', 'Show status breakdown', 'sp_fb_GetProjectSummary', 'GET /api/Dashboard/project/{projectId}/summary', 'Tasks by status, burndown metrics', 'Dashboard > Project Summary', '/dashboard/project/:id', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Dashboard & Analytics', '📊 Visual overview', 'Task Overview', 'All tasks by status, user', 'sp_fb_GetTaskSummary', 'GET /api/Dashboard/task/summary', 'Filter by sprint, project', 'Dashboard > Task View', '/dashboard/tasks', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1'),

('Dashboard & Analytics', '📊 Visual overview', 'Velocity Chart', 'Show velocity over sprints', 'sp_fb_GetVelocityChartData', 'GET /api/Dashboard/velocity/{projectId}', 'Optional in Phase 2', 'Velocity Chart', '/dashboard/velocity', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Low', 'Phase 1'),

('Dashboard & Analytics', '📊 Visual overview', 'Burndown Chart', 'Sprint progress tracking', 'sp_fb_GetBurndownData', 'GET /api/Dashboard/burndown/{sprintId}', 'Plot daily remaining work', 'Burndown', '/dashboard/burndown', 'Pending', 'Not Started', 0, NULL, NULL, NULL, NULL, 'Medium', 'Phase 1');
