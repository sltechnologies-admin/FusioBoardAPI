-- EXEC sp_rename 'UserProjectRoles', 'UserRoles';

--  list all the stored procedures in a SQL Server database:
SELECT 
    SCHEMA_NAME(p.schema_id) AS [Schema],
    p.name AS [ProcedureName],
    p.create_date,
    p.modify_date
FROM 
    sys.procedures p
ORDER BY 
    [Schema], [ProcedureName];
