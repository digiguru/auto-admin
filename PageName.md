# Introduction #

You'll want to run the SQL file on your server under an admin login. Make sure you chaneg the Username at the top of the file.

# Details #

Here is a list of procedurtes the file creates....
  * **AdminTool**.**GetAllProcedures** (schema varchar(500) = _Null_) - Lists all the stored procedures in the database. If a schema is passed, it will list only procedures in that schema.
  * **AdminTool**.**GetAllParametersForProcedure** (@ProcedureName varchar(500)) - Lists the parameters for a given stored procedure
  * **AdminTool**.**GetExtendedPropertiesForProcedure** (@ProcedureName varchar(500), @PropertyName varchar(500) = Null) - Lists all the extended properties for a procedure. If a property name is included, then it will only list that values for that given property.
  * **AdminTool**.**GetExtendedPropertiesForProcedureParameter** (@ProcedureName varchar(500), @ParameterName varchar(500), @PropertyName varchar(500) = Null)- Lists all the extended properties for a procedure's parameter. If a property name is included, then it will only list that values for that given property.
  * **AdminTool**.**AddOrUpdateExtendedPropertyToProcedure** (@SchemaName varchar(500),@ProcedureName varchar(500),@PropertyName varchar(500),@PropertyValue varchar(2000)) - Adds or updates an extended property to a given stored procedure name.
  * **AdminTool**.**AddOrUpdateExtendedPropertyToProcedureParameter** (@SchemaName varchar(500),@ProcedureName varchar(500),@ParameterName varchar(500),@PropertyName varchar(500),@PropertyValue varchar(2000)) - Adds or updates an extended property to a given parameter within a stored procedure.
  * **AdminTool**.**GetTableInformation** (@ObjectName varchar(500)) - Dynamic SQL to get data from the object passed in through ObjectName