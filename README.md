# OracleUDTCoreDapperConsoleApp

OracleUDTCoreDapperConsoleApp is a demo app or say POC for Oracle using UDT in .net core using ODP.NET Core driver(dll)

The solution is created as the POC to pass user defined type parameter from application to stored procedure as a input parameter in Oracle.

Select Initial_Commit to get the code

**Changes required in Oracle Database**

Execute below SP and UDT scripts in oracle database

DROP TYPE ODP_OBJ1_SAMPLE_PERSON_TABLE;

DROP TYPE ODP_OBJ1_SAMPLE_PERSON_TYPE;

DROP PROCEDURE odp_obj1_sample_upd_contacts;

CREATE OR REPLACE procedure odp_obj1_sample_upd_contacts(
 param2 IN     varchar2,
 param3 IN  odp_obj1_sample_person_table
) as
  begin
     insert into odp_obj1_sample_contacts values(null,param2);   
  end;


CREATE OR REPLACE type UFD_FSL.odp_obj1_sample_person_table as table of    odp_obj1_sample_person_type

CREATE OR REPLACE type UFD_FSL.odp_obj1_sample_person_type as object 
  (name varchar2(30), address varchar2(60), age number(3)) NOT FINAL;


**Changes required in Application**

Kindly add the Connection String and wallet value at line 30 and line 59 respectively in Project.cs

_**Note**_

1. If want to check the ado.net flow then uncomment method _CallingWithoutDapper_(line no. 32) and comment the method _CallingWithDapper_(line no 34) in Project.cs
2. If want to check the dapper flow then comment method _CallingWithoutDapper_(line no. 32) and uncomment the method _CallingWithDapper_(line no 34) in Project.cs

