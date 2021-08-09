create procedure InsertDataIntoERTable
(
@companyId int ,@employeName varchar(50),@phoneNumber bigint ,@address varchar(50),
@gender char ,@basePay int ,@deduction int ,@incomeTax int ,@departmentId int ,@isActive int,
@date date
)
as 
begin 
set xact_abort on;
begin try
begin transaction;
declare @out int;
INSERT INTO Employee VALUES (@companyId,@employeName,@phoneNumber,@address,@date,@gender,@isActive);
select @out = SCOPE_IDENTITY();
INSERT INTO EMPPayroll(EmployeeIdentity,BasicPay,Deductions,IncomeTax) values (@out,@basePay,@deduction,@incomeTax);
UPDATE EMPPayroll SET TaxablePay = BasicPay-Deductions;
UPDATE EMPPayroll SET NetPay=TaxablePay-IncomeTax;
INSERT INTO EmployeeDepartment VALUES (@departmentId,@out);
commit transaction;
end try
begin catch
if(xact_state())=-1
begin 
print N'The querys have some error!!'+'Rolling back transaction'
rollback transaction;
end;
if(xact_state())=1
begin
print N'The Transaction Is Done'+'Commiting Transaction'
commit transaction;
end;
end catch
end