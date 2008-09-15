CREATE SCHEMA [AdminTool] AUTHORIZATION Developer


Go
Create Procedure [AdminTool].GetAllProcedures
(@Schema varchar(500) = Null)
As
If @Schema Is Null 
Begin
Select 
	Distinct 
	procs.Specific_Name as [Name], 
	procs.Specific_Schema As [Schema], 
	Count(Parameter_name) ParameterCount 
FROM InformatioN_Schema.Routines procs
Left Outer Join INFORMATION_SCHEMA.PARAMETERS params
ON procs.Specific_Name = params.Specific_NAME and procs.Specific_Schema = params.Specific_Schema
Where Routine_Type = 'PROCEDURE'
Group By procs.Specific_Name, procs.Specific_Schema
End
Else
Begin
Select 
	Distinct 
	procs.Specific_Name as [Name], 
	procs.Specific_Schema As [Schema],
	Count(Parameter_name) ParameterCount 
FROM InformatioN_Schema.Routines procs
Left Outer Join INFORMATION_SCHEMA.PARAMETERS params
ON procs.Specific_Name = params.Specific_NAME and procs.Specific_Schema = params.Specific_Schema
Where procs.Routine_Type = 'PROCEDURE' And procs.Routine_Schema = @Schema
Group By procs.Specific_Name, procs.Specific_Schema
End
Go
Alter Procedure [AdminTool].GetAllParametersForProcedure 
(@ProcedureName varchar(500) = '')
As
SELECT 
	Specific_Catalog, 
	Specific_Schema, 
	Specific_name, 
	Parameter_Mode, 
	Parameter_name,
	Data_Type,
	Character_Maximum_Length
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE Specific_NAME=@ProcedureName
Order By Ordinal_position
gO
CREATE Procedure [AdminTool].GetExtendedPropertiesForProcedure
(@ProcedureName varchar(500), @PropertyName varchar(500) = Null)
As
If @PropertyName Is Null 
Begin
Select ep.name, ep.value FROM sys.extended_properties ep
	Inner Join sys.all_objects o on ep.major_ID = o.object_id
Where o.name = @ProcedureName
And ep.minor_id = 0
End 
Else
Begin
Select ep.name, ep.value FROM sys.extended_properties ep
	Inner Join sys.all_objects o on ep.major_ID = o.object_id
Where o.name = @ProcedureName and ep.name = @PropertyName
And ep.minor_id = 0
End

Go
Create Procedure [AdminTool].GetExtendedPropertiesForProcedureParameter
(@ProcedureName varchar(500), @ParameterName varchar(500), @PropertyName varchar(500) = Null)
As
If @PropertyName Is Null 
Begin
Select ep.name, ep.value
FROM sys.extended_properties ep
	Inner Join sys.all_objects o on ep.major_ID = o.object_id
	Inner Join Information_schema.parameters p on ep.minor_ID = p.ordinal_position and o.name = p.specific_name
Where o.name = @ProcedureName
And p.parameter_name = @ParameterName
--And ep.minor_id = 0
End 
Else
Begin
Select ep.name, ep.value
FROM sys.extended_properties ep
	Inner Join sys.all_objects o on ep.major_ID = o.object_id
	Inner Join Information_schema.parameters p on ep.minor_ID = p.ordinal_position and o.name = p.specific_name
Where o.name = @ProcedureName
And p.parameter_name = @ParameterName
and ep.name = @PropertyName
--And ep.minor_id = 0
End
Go
Alter Procedure [AdminTool].AddOrUpdateExtendedPropertyToProcedure
	(
	--@SchemaName varchar(500),
	@ProcedureName varchar(500),
	@PropertyName varchar(500),
	@PropertyValue varchar(2000)
	)
As
Declare @SchemaName varchar(500)
Select 
	@SchemaName = procs.Specific_Schema
FROM InformatioN_Schema.Routines procs
Where procs.Specific_Name = @ProcedureName
Print @SchemaName
Exec [AdminTool].GetExtendedPropertiesForProcedure @ProcedureName,@PropertyName

If @@rowcount = 0
Begin
Print 'Add'
Exec sp_addextendedproperty
	@name = @PropertyName, @value = @PropertyValue,
	@level0type = N'Schema', @level0name = @SchemaName,
	@level1type = N'Procedure',  @level1name = @ProcedureName
End
Else
Begin
Print 'Update'
Exec sp_updateextendedproperty
	@name = @PropertyName, @value = @PropertyValue,
	@level0type = N'Schema', @level0name = @SchemaName,
	@level1type = N'Procedure',  @level1name = @ProcedureName
End
Go
Create Procedure [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter
	(
	--@SchemaName varchar(500),
	@ProcedureName varchar(500),
	@ParameterName varchar(500),
	@PropertyName varchar(500),
	@PropertyValue varchar(2000)
	)
as
Declare @SchemaName varchar(500)
Select 
	@SchemaName = procs.Specific_Schema
FROM InformatioN_Schema.Routines procs
Where procs.Specific_Name = @ProcedureName

Exec [AdminTool].GetExtendedPropertiesForProcedureParameter @ProcedureName,@ParameterName,@PropertyName 
If @@rowcount = 0
Begin
Exec sp_addextendedproperty
	@name = @PropertyName, @value = @PropertyValue,
	@level0type = N'Schema', @level0name = @SchemaName,
	@level1type = N'Procedure',  @level1name = @ProcedureName,
	@level2type = N'PARAMETER', @level2name = @ParameterName
End
Else
Begin
Exec sp_updateextendedproperty
	@name = @PropertyName, @value = @PropertyValue,
	@level0type = N'Schema', @level0name = @SchemaName,
	@level1type = N'Procedure',  @level1name = @ProcedureName,
	@level2type = N'PARAMETER', @level2name = @ParameterName
End
Go
Create Procedure [AdminTool].GetTableInformation (@ObjectName varchar(500))
As
EXEC ('Select * FROM ' +  @ObjectName )
Go
Create Procedure [AdminTool].AddPropertiesToProcedure 
(
@ProcedureName varchar(500),
@FriendlyName varchar(500),
@Description varchar(2000)
)
As
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedure @ProcedureName, 'FriendlyName', @FriendlyName
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedure @ProcedureName, 'Description', @Description
Go
Alter Procedure [AdminTool].AddPropertiesToProcedureParameter
(
@ProcedureName varchar(500),
@ParameterName varchar(500),
@FriendlyName varchar(500),
@Description varchar(2000),
@Nullable bit
/*
@DataType varchar(500),
@BoundObject varchar(500),
@BoundID varchar(500),
@BoundName varchar(500)
*/
)
As
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'FriendlyName', @FriendlyName
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'Description', @Description
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'Nullable', @Nullable
/*
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'DataType', @DataType
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'BoundObject', @BoundObject
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'BoundID', @BoundID
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'BoundName', @BoundName
*/
Go
Create Procedure [AdminTool].[AddDropdownPropertiesToProcedureParameter]
(
@ProcedureName varchar(500),
@ParameterName varchar(500),
@BoundObject varchar(2000),
@BoundID varchar(2000),
@BoundName varchar(2000)
)
As
Declare @DataType varchar(50)
Set @DataType = 'dropdown'
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'DataType', @DataType
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'BoundObject', @BoundObject
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'BoundID', @BoundID
Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter @ProcedureName, @ParameterName, 'BoundName', @BoundName
Go
EXEC [AdminTool].[AddDropdownPropertiesToProcedureParameter]
	@ProcedureName = 'AddPropertiesToProcedureParameter',
	@ParameterName = '@ProcedureName',
	@BoundObject = 'GetAllProcedures',
	@BoundID = '[Name]',
	@BoundName = '[Name]'
Go
Exec [AdminTool].[GetExtendedPropertiesForProcedureParameter]
	@ProcedureName = 'AddPropertiesToProcedureParameter',
	@ParameterName = '@ProcedureName'
go


Exec [AdminTool].AddPropertiesToProcedure 'GetAllProcedures', 'Get all forms', 'Gets every form in the database'
Exec [AdminTool].AddPropertiesToProcedure 'GetAllProcedures', 'Get all forms', 'Gets every form in the database'
Exec [AdminTool].GetExtendedPropertiesForProcedure 'GetAllProcedures'
Exec [AdminTool].AddPropertiesToProcedureParameter 'TestDate', '@DateStamp', 'Date stamp', 'A date time', 'Date', 'Binding', '', ''
Exec [AdminTool].GetExtendedPropertiesForProcedureParameter 'TestDate', '@DateStamp'

/*
Exec [AdminTool].GetExtendedPropertiesForProcedureParameter
	@ProcedureName = 'AddOrUpdateExtendedPropertyToProcedure',
	@ParameterName = '@SchemaName',
	@PropertyName = 'Description'
Select @@Rowcount

Exec [AdminTool].AddOrUpdateExtendedPropertyToProcedureParameter 
	@SchemaName = 'AdminTool', 
	@ProcedureName = 'AddOrUpdateExtendedPropertyToProcedure',
	@ParameterName = '@SchemaName',
	@PropertyName = 'Description',
	@PropertyValue = 'The name of the schema where the procedure resides.'
*/
/*Ideas for ExtendedProperty Column Names

	BindingObjectName
	BindingObjectColumn
	BindingObjectIDColumn
	BindingObjectValueColumn
	BindingObjectCategoryColumn (for ajax dropdowns)
	Description
	VisibleToAdmin
	Default
*/


