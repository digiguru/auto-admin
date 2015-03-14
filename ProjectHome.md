This tool will allow dynamic creation of web forms in ASP.NET for any stored procedure found in your SQL Server database.

It uses INFORMATION\_SCHEMA views to discover information about your stored procedures and the paramters it contains. (broken down by the databse schema they reside in). This then creates a form so that you can use the stored procedure.

EXTENDED\_PROPERTIES are utalised to assist the process of inputting information in the web forms, so that ID fields are selectable through dropdownlists.

The intention is that each input form is fully customizable using extended properties, but there is alwatys a simple form to fall back on if the developer has not customized the fields.