-- EXEC sp_rename 'UserProjectRoles', 'UserRoles';

select * from Users

select * from Logs
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


 --  list all the tables in a SQL Server database:
 --SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
