
-- ✅ Default Lookup Data
INSERT INTO TaskStatuses (StatusId, StatusName)
VALUES 
    (1, 'Open'),
    (2, 'In Progress'),
    (3, 'Done'),
    (4, 'Blocked');

INSERT INTO TaskPriorities (PriorityId, PriorityName)
VALUES 
    (1, 'Low'),
    (2, 'Medium'),
    (3, 'High'),
    (4, 'Critical');

INSERT INTO TaskTypes (TypeId, TypeName)
VALUES 
    (1, 'Bug'),
    (2, 'Feature'),
    (3, 'Task'),
    (4, 'Improvement');


INSERT INTO Roles (RoleId, RoleName, Description) VALUES
(1, 'Developer', 'Responsible for writing and committing code'),
(2, 'QA', 'Handles testing and quality assurance'),
(3, 'Scrum Master', 'Facilitates Agile processes and sprint meetings'),
(4, 'Product Owner', 'Defines features, prioritizes backlog, owns product vision'),
(5, 'Admin', 'Full system access, manages users, roles, configurations'),
(6, 'Client', 'Can view progress, raise bugs or feedback');
